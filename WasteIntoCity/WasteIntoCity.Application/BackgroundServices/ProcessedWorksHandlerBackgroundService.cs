
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Api.BackgroundServices
{
    public class ProcessedWorksHandlerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<ProcessedWorksHandlerBackgroundServiceOptions> _options;

        private readonly List<ScoreSettingsEnum> _scoreSettingsIds = new List<ScoreSettingsEnum>
        {
            ScoreSettingsEnum.ProcessedWorkRankingSubstracting,
            ScoreSettingsEnum.ProcessedWorkNegativeAdding,
            ScoreSettingsEnum.UserBanRankingAtLeast,
        };

        public ProcessedWorksHandlerBackgroundService(IServiceScopeFactory scopeFactory, IOptions<ProcessedWorksHandlerBackgroundServiceOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        private async Task HandlePendingFinalizationWorkAsync(Work work, Dictionary<ScoreSettingsEnum, int> scoreSettingsValues, IWorksRepository worksRepository,
            IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository, CancellationToken stoppingToken)
        {
            if (work.Participants == null)
            {
                throw new NullValueServerException(20, "participants", null);
            }

            List<User> updatedParticipants = new List<User>();

            foreach (User participant in work.Participants)
            {
                int ranking = 0;
                int negativeScore = 0;

                if (participant.NegativeScore == 0)
                {
                    ranking = participant.Ranking - scoreSettingsValues[ScoreSettingsEnum.ProcessedWorkRankingSubstracting];
                    negativeScore = 1;
                }
                else
                {
                    ranking = participant.Ranking - scoreSettingsValues[ScoreSettingsEnum.ProcessedWorkRankingSubstracting] * negativeScore;
                    negativeScore = participant.NegativeScore + scoreSettingsValues[ScoreSettingsEnum.ProcessedWorkNegativeAdding];
                }

                bool isBanned = false;

                if (ranking <= scoreSettingsValues[ScoreSettingsEnum.UserBanRankingAtLeast])
                {
                    isBanned = true;

                    await refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, participant.Id);
                }

                updatedParticipants.Add(User.Create(participant.Id, participant.Nickname, participant.Email, participant.Password, ranking,
                    participant.Roles, negativeScore, isBanned));
            }

            await usersRepository.UpdateAllByIdAsync(updatedParticipants);

            await worksRepository.UpdateStatusesIdByIdAsync(work.Id, WorkStatusEnum.Closed);
        }

        private async Task HandlePendingFinalizationWorksAsync(IWorksRepository worksRepository, IScoreSettingsTypeRepository scoreSettingsTypeRepository,
            IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository, CancellationToken stoppingToken)
        {
            List<Work> works = await worksRepository.FindFirstByFinishedTimeAndStatusesWithParticipants(
                _options.Value.WorksAtTimeAmount,
                _options.Value.MinWorkIntervalAfterFinished,
                [WorkStatusForClientEnum.PendingFinalization]
            );

            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIds);

            int i = 0;
            while (i < works.Count && !stoppingToken.IsCancellationRequested)
            {
                await HandlePendingFinalizationWorkAsync(works[i], scoreSettingsValues, worksRepository, usersRepository, refreshTokensRepository, stoppingToken);

                i++;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(_options.Value.IntervalTime, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();

                IWorksRepository worksRepository = scope.ServiceProvider.GetRequiredService<IWorksRepository>();
                IScoreSettingsTypeRepository scoreSettingsTypeRepository = scope.ServiceProvider.GetRequiredService<IScoreSettingsTypeRepository>();
                IUsersRepository usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();
                RefreshTokensRepository refreshTokensRepository = scope.ServiceProvider.GetRequiredService<RefreshTokensRepository>();

                await HandleInProgressWorksAsync(worksRepository, scoreSettingsTypeRepository, usersRepository, refreshTokensRepository, stoppingToken);

                await Task.Delay(_options.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
