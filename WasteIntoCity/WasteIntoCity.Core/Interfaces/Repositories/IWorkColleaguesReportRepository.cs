using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkColleaguesReportRepository
    {
        Task<List<WorkColleagueReport>> FindByFromParticipantIdAndWorksIdAsync(Guid fromParticipantId, Guid worksId);
    }
}
