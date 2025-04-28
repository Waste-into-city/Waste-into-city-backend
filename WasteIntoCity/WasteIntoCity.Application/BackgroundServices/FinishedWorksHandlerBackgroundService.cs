
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

namespace WasteIntoCity.Application.BackgroundServices
{
    public class FinishedWorksHandlerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<FinishedWorksHandlerBackgroundServiceOptions> _options;

        private readonly List<ScoreSettingsEnum> _scoreSettingsIds = new List<ScoreSettingsEnum>
        {
            ScoreSettingsEnum.WorkApplicationRankingSubstracting,
            ScoreSettingsEnum.WorkApplicationNegativeAddingMultiplier,
            ScoreSettingsEnum.UserBanRankingAtLeast,
        };

        public FinishedWorksHandlerBackgroundService(IServiceScopeFactory scopeFactory, IOptions<FinishedWorksHandlerBackgroundServiceOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        private async Task CloseFinishedWorkAsync(Work work, Dictionary<ScoreSettingsEnum, int> scoreSettingsValues,
            IWorksRepository worksRepository, IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository,
            CancellationToken stoppingToken)
        {
            if (work.Participants == null)
            {
                throw new NullValueServerException(15, "participants", null);
            }

            if (work.WorkColleagueReports == null)
            {
                throw new NullValueServerException(16, "work colleague reports", null);
            }

            if (work.WorkComplexityType == null)
            {
                throw new NullValueServerException(17, "work complexity type", null);
            }

            if (work.WorkStatusType == null)
            {
                throw new NullValueServerException(18, "work status type", null);
            }

            int workParticipantsCount = work.Participants.Count;

            List<User> updatedParticipants = new List<User>();

            foreach (User participant in work.Participants)
            {
                int score = 0;

                List<WorkColleagueReport> workAboutParticipantColleagueReports = work.WorkColleagueReports.Where(w => w.AboutColleagueId == participant.Id).ToList();

                foreach (WorkColleagueReport workAboutParticipantColleagueReport in workAboutParticipantColleagueReports)
                {
                    if (workAboutParticipantColleagueReport.WorkMarkType == null)
                    {
                        throw new NullValueServerException(19, "work mark type", null);
                    }

                    score = score + workAboutParticipantColleagueReport.WorkMarkType.AdditionRanking;
                }

                score = score + scoreSettingsValues[ScoreSettingsEnum.WorkRankingDefaultReviewAdding] * (workParticipantsCount - work.WorkColleagueReports.Count);

                int rankingAddingScore = (int)Math.Round((double)score / workParticipantsCount, MidpointRounding.AwayFromZero);

                int negativeScore = 0;
                int ranking = 0;
                bool isBanned = false;

                if (rankingAddingScore > 0 && work.WorkStatusTypesId == WorkStatusEnum.FinishedSuccessfully)
                {
                    negativeScore = Math.Max(participant.NegativeScore - scoreSettingsValues[ScoreSettingsEnum.WorkNegativeSubstracting], 0);
                    ranking = participant.Ranking + rankingAddingScore * work.WorkComplexityType.MultiplierRanking + work.WorkStatusType.AddingRanking;
                }
                else if (rankingAddingScore < 0)
                {
                    if (participant.NegativeScore == 0)
                    {
                        ranking = participant.Ranking + rankingAddingScore + work.WorkStatusType.AddingRanking - scoreSettingsValues
                            [ScoreSettingsEnum.WorkApplicationRankingSubstracting];
                        negativeScore = 1;
                    }
                    else
                    {
                        ranking = participant.Ranking + rankingAddingScore + work.WorkStatusType.AddingRanking - scoreSettingsValues
                            [ScoreSettingsEnum.WorkApplicationRankingSubstracting] * negativeScore;
                        negativeScore = participant.NegativeScore * scoreSettingsValues[ScoreSettingsEnum.WorkNegativeAddingMultiplier];
                    }

                    if (ranking <= scoreSettingsValues[ScoreSettingsEnum.UserBanRankingAtLeast])
                    {
                        isBanned = true;

                        await refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, participant.Id);
                    }
                }

                updatedParticipants.Add(User.Create(participant.Id, participant.Nickname, participant.Email, participant.Password, ranking,
                    participant.Roles, negativeScore, isBanned, null, null));
            }

            await usersRepository.UpdateAllByIdAsync(updatedParticipants);
            await worksRepository.UpdateStatusIdByIdAsync(work.Id, WorkStatusEnum.Closed);
        }

        private async Task CloseFinishedWorksAsync(IWorksRepository worksRepository, IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository,
            IScoreSettingsTypeRepository scoreSettingsTypeRepository, CancellationToken stoppingToken)
        {
            List<Work> works = await worksRepository.FindFirstFinishedWithParticipantsAndMultiplierRankingAndWorkColleagueReportsByFinishedTimeAndClientStatuses(
                _options.Value.WorksAtTimeAmount,
                _options.Value.MinWorkIntervalAfterFinished
            );

            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIds);

            int i = 0;
            while (i < works.Count && !stoppingToken.IsCancellationRequested)
            {
                await CloseFinishedWorkAsync(works[i], scoreSettingsValues, worksRepository, usersRepository, refreshTokensRepository, stoppingToken);

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
                IUsersRepository usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();
                RefreshTokensRepository refreshTokensRepository = scope.ServiceProvider.GetRequiredService<RefreshTokensRepository>();
                IScoreSettingsTypeRepository scoreSettingsTypeRepository = scope.ServiceProvider.GetRequiredService<IScoreSettingsTypeRepository>();

                await CloseFinishedWorksAsync(worksRepository, usersRepository, refreshTokensRepository, scoreSettingsTypeRepository, stoppingToken);

                await Task.Delay(_options.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
