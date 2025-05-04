using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkColleaguesReportRepository
    {
        Task AddMarksAsync(List<WorkColleagueReport> workColleagueReports);

        Task<List<WorkColleagueReport>> FindAllWithAboutColleagueByWorksIdAsync(Guid worksId);
    }
}
