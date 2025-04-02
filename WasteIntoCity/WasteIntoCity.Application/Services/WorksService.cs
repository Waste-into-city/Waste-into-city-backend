using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.Services
{
    public class WorksService : IWorksService
    {
        private readonly IWorksRepository _worksRepository;

        public WorksService(IWorksRepository worksRepository)
        {
            _worksRepository = worksRepository;
        }

        public async Task<List<Work>> GetAll()
        {
            return await _worksRepository.FindAllAsync();
        }

        public async Task<List<Work>> GetAllOwnTakePartIn(Guid userId)
        {
            return await _worksRepository.FindAllByParticipantIdAsync(userId);
        }

        public async Task<Work> GetById(Guid id)
        {
            return await _worksRepository.FindByIdAsync(id);
        }

        public async Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, int workComplexityTypesId,
            int workStatusesId, Guid coordinatesId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkComplexityEnum>(workComplexityTypesId, "work complexity");
            EnumOperationsExtension.CheckEnumIntValue<WorkStatusEnum>(workStatusesId, "work status");

            Work work = Work.Create(id, Title.Create(title), Description.Create(description), startedDatetime, finishDatetime,
                (WorkComplexityEnum)workComplexityTypesId, (WorkStatusEnum)workStatusesId, coordinatesId, null, null);

            await _worksRepository.UpdateAsync(work);
        }

        public async Task UpdateWorkStatusAsync(Guid id, int workStatusesId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkStatusEnum>(workStatusesId, "work status");

            await _worksRepository.UpdateStatusesIdByIdAsync(id, (WorkStatusEnum)workStatusesId);
        }

        public async Task AddPointsForUserAsync(Guid workId, User currentUser, Dictionary<WorkMarkEnum, int> workMarkTypesDict, int participantsCount,
            IWorkColleaguesReportRepository workColleaguesReportRepository, IUsersRepository usersRepository)
        {
            const int DEFAULT_ADDITION_RANKING = 1;
            const int NEGATIVE_SCORE_DECREASING = 1;

            List<WorkColleagueReport> workColleagueReports = await workColleaguesReportRepository.
                FindByFromParticipantIdAndWorksIdAsync(currentUser.Id, workId);

            int score = 0;

            foreach (WorkColleagueReport workColleagueReport in workColleagueReports)
            {
                score = score + workMarkTypesDict.GetValueOrDefault(workColleagueReport.WorkMarkTypesId);
            }

            score = score + DEFAULT_ADDITION_RANKING * (participantsCount - workColleagueReports.Count);
            int ranking = currentUser.Ranking + score / participantsCount;
            int negativeScore = currentUser.NegativeScore - NEGATIVE_SCORE_DECREASING;

            User updatedUser = User.Create(currentUser.Id, currentUser.Nickname, currentUser.Email, currentUser.Password, ranking,
                currentUser.Roles, negativeScore, currentUser.IsBanned);

            await usersRepository.UpdateAsync(updatedUser);
        }


        public async Task AddPointsAsync(IWorksRepository worksRepository, IUsersRepository usersRepository,
            IWorkColleaguesReportRepository workColleaguesReportRepository, IWorkMarkTypesRepository workMarkTypesRepository)
        {
            Work work;

            try
            {
                work = await worksRepository.FindFirstFilteredWithParticipantsByTimestampFinishedWork();
            }
            catch
            {
                return;
            }

            Dictionary<WorkMarkEnum, int> workMarkTypesDict = await workMarkTypesRepository.TakeDictionaryAllWithKeyIdAndValueAdditionRanking();

            int participantsCount = work.Participants!.Count;

            for (int i = 0; i < participantsCount; i++)
            {
                await AddPointsForUserAsync(work.Id, work.Participants[i], workMarkTypesDict, participantsCount, workColleaguesReportRepository, usersRepository);
            }

            await worksRepository.UpdateStatusesIdByIdAsync(work.Id, WorkStatusEnum.Closed);
        }
    }
}
