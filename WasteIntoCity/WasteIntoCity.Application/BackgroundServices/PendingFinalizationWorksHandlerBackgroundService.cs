
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Application.BackgroundServices
{
    public class PendingFinalizationWorksHandlerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<PendingFinalizationWorksHandlerBackgroundServiceOptions> _options;

        private readonly List<ScoreSettingsEnum> _scoreSettingsIds = new List<ScoreSettingsEnum>
        {
            ScoreSettingsEnum.PendingFinalizationWorkRankingSubstracting,
            ScoreSettingsEnum.PendingFinalizationWorkNegativeAdding,
            ScoreSettingsEnum.UserBanRankingAtLeast,
        };

        public PendingFinalizationWorksHandlerBackgroundService(IServiceScopeFactory scopeFactory, IOptions<PendingFinalizationWorksHandlerBackgroundServiceOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        private async Task ClosePendingFinalizationWorkAsync(Work work, Dictionary<ScoreSettingsEnum, int> scoreSettingsValues, IWorksRepository worksRepository,
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
                    ranking = participant.Ranking - scoreSettingsValues[ScoreSettingsEnum.PendingFinalizationWorkRankingSubstracting];
                    negativeScore = 1;
                }
                else
                {
                    ranking = participant.Ranking - scoreSettingsValues[ScoreSettingsEnum.PendingFinalizationWorkRankingSubstracting] * negativeScore;
                    negativeScore = participant.NegativeScore + scoreSettingsValues[ScoreSettingsEnum.PendingFinalizationWorkNegativeAdding];
                }

                bool isBanned = false;

                if (ranking <= scoreSettingsValues[ScoreSettingsEnum.UserBanRankingAtLeast])
                {
                    isBanned = true;

                    await refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, participant.Id);
                }

                updatedParticipants.Add(User.Create(participant.Id, participant.Nickname, participant.Email, participant.Password, ranking,
                    participant.Roles, negativeScore, isBanned, null, null));
            }

            await usersRepository.UpdateAllByIdAsync(updatedParticipants);

            await worksRepository.UpdateStatusIdByIdAsync(work.Id, WorkStatusEnum.Closed);
        }

        private async Task ClosePendingFinalizationWorksAsync(IWorksRepository worksRepository, IScoreSettingsTypesRepository scoreSettingsTypeRepository,
            IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository, CancellationToken stoppingToken)
        {
            List<Work> works = await worksRepository.FindFirstPendingFinalizationWorksWithParticipantsByFinishedTimeAndClientStatuses(
                _options.Value.WorksAtTimeAmount,
                _options.Value.MinWorkIntervalAfterFinished
            );

            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIds);

            int i = 0;
            while (i < works.Count && !stoppingToken.IsCancellationRequested)
            {
                await ClosePendingFinalizationWorkAsync(works[i], scoreSettingsValues, worksRepository, usersRepository, refreshTokensRepository, stoppingToken);

                i++;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(_options.Value.StartServiceWaitingTime, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();

                IWorksRepository worksRepository = scope.ServiceProvider.GetRequiredService<IWorksRepository>();
                IScoreSettingsTypesRepository scoreSettingsTypeRepository = scope.ServiceProvider.GetRequiredService<IScoreSettingsTypesRepository>();
                IUsersRepository usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();
                RefreshTokensRepository refreshTokensRepository = scope.ServiceProvider.GetRequiredService<RefreshTokensRepository>();

                await ClosePendingFinalizationWorksAsync(worksRepository, scoreSettingsTypeRepository, usersRepository, refreshTokensRepository, stoppingToken);

                await Task.Delay(_options.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
