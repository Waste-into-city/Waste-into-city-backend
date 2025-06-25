using WasteIntoCity.Application.Structs;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Application.Services
{
    public class WorkColleagueReportsService : IWorkColleagueReportsService
    {
        private readonly IWorkColleaguesReportRepository _workColleagueReportsRepository;
        private readonly IWorkReportResultsRepository _workReportResultsRepository;

        public WorkColleagueReportsService(IWorkColleaguesReportRepository workColleagueReportsRepository,
            IWorkReportResultsRepository workReportResultsRepository)
        {
            _workColleagueReportsRepository = workColleagueReportsRepository;
            _workReportResultsRepository = workReportResultsRepository;
        }

        public async Task CreateMarksAsync(Guid worksId, Guid fromParticipantId, List<MarkColleaguePairStruct> workColleaguePairStructs)
        {
            try
            {
                await _workReportResultsRepository.FindByWorksIdAsync(worksId);
            }
            catch
            {
                throw new NotAllowedStatusException(78, "Report result about the work hasn't been created.");
            }

            List<WorkColleagueReport> workColleagueReports = workColleaguePairStructs.Select(w => WorkColleagueReport.Create(
                Guid.NewGuid(), fromParticipantId, w.AboutColleagueId, worksId, w.WorkMarkTypesId, null, null)).ToList();

            await _workColleagueReportsRepository.AddMarksAsync(workColleagueReports);
        }

        public async Task<List<WorkColleagueReport>> GetAllByWorksIdAndUserIdAsync(Guid usersId, Guid worksId)
        {
            return await _workColleagueReportsRepository.FindAllWithAboutColleagueWithAvatarImageNameByWorksIdAndUserIdAsync(
                usersId, worksId);
        }
    }
}

