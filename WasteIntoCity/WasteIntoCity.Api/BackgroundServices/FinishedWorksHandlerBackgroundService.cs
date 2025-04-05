
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Api.BackgroundServices
{
    public class FinishedWorksHandlerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<FinishedWorksHandlerBackgroundServiceOptions> _options;

        private const int WORK_RANKING_DEFAULT_REVIEW_ADDING = 1;
        private const int WORK_NEGATIVE_ADDING_MULTIPLIER = 2;
        private const int WORK_NEGATIVE_SUBSTRACTING = 1;

        private const int USER_BAN_RANKING_AT_LEAST = -30;

        public FinishedWorksHandlerBackgroundService(IServiceScopeFactory scopeFactory, IOptions<FinishedWorksHandlerBackgroundServiceOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        private async Task HandleFinishedWork(Work work, IWorksRepository worksRepository, IUsersRepository usersRepository,
            RefreshTokensRepository refreshTokensRepository, CancellationToken stoppingToken)
        {
            if (work.Participants == null)
            {
                throw new NullValueServerException("participants", null);
            }

            if (work.WorkColleagueReports == null)
            {
                throw new NullValueServerException("work colleague reports", null);
            }

            if (work.WorkComplexityType == null)
            {
                throw new NullValueServerException("work complexity type", null);
            }

            if (work.WorkStatusType == null)
            {
                throw new NullValueServerException("work status type", null);
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
                        throw new NullValueServerException("work mark type", null);
                    }

                    score = score + workAboutParticipantColleagueReport.WorkMarkType.AdditionRanking;
                }

                score = score + WORK_RANKING_DEFAULT_REVIEW_ADDING * (workParticipantsCount - work.WorkColleagueReports.Count);

                int rankingAddingScore = (int)Math.Round((double)score / workParticipantsCount, MidpointRounding.AwayFromZero);

                int negativeScore = 0;
                int ranking = 0;
                bool isBanned = false;

                if (rankingAddingScore > 0 && work.WorkStatusTypesId == WorkStatusEnum.FinishedSuccessfully)
                {
                    negativeScore = Math.Max(participant.NegativeScore - WORK_NEGATIVE_SUBSTRACTING, 0);
                    ranking = participant.Ranking + rankingAddingScore * work.WorkComplexityType.MultiplierRanking + work.WorkStatusType.AddingRanking;
                }
                else if (rankingAddingScore < 0)
                {
                    if (participant.NegativeScore == 0)
                    {
                        ranking = participant.Ranking + rankingAddingScore + work.WorkStatusType.AddingRanking;
                        negativeScore = 1;
                    }
                    else
                    {
                        ranking = participant.Ranking + (rankingAddingScore + work.WorkStatusType.AddingRanking) * negativeScore;
                        negativeScore = participant.NegativeScore * WORK_NEGATIVE_ADDING_MULTIPLIER;
                    }

                    if (ranking <= USER_BAN_RANKING_AT_LEAST)
                    {
                        isBanned = true;

                        await refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, participant.Id);
                    }
                }

                updatedParticipants.Add(User.Create(participant.Id, participant.Nickname, participant.Email, participant.Password, ranking,
                    participant.Roles, negativeScore, isBanned));
            }

            await usersRepository.UpdateAllByIdAsync(updatedParticipants);
            await worksRepository.UpdateStatusesIdByIdAsync(work.Id, WorkStatusEnum.Closed);
        }

        private async Task HandleFinishedWorks(IWorksRepository worksRepository, IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository,
            CancellationToken stoppingToken)
        {
            List<Work> works = await worksRepository.TakeFirstWorksByFinishedTimeWithParticipantsAndMultiplierRankingAndWorkColleagueReportsAndWorkStatus(
                _options.Value.WorksAtTimeAmount,
                _options.Value.MinWorkIntervalAfterFinished
            );

            int i = 0;
            while (i < works.Count && !stoppingToken.IsCancellationRequested)
            {
                await HandleFinishedWork(works[i], worksRepository, usersRepository, refreshTokensRepository, stoppingToken);

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
                IUsersRepository usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();
                RefreshTokensRepository refreshTokensRepository = scope.ServiceProvider.GetRequiredService<RefreshTokensRepository>();

                await HandleFinishedWorks(worksRepository, usersRepository, refreshTokensRepository, stoppingToken);

                await Task.Delay(_options.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
