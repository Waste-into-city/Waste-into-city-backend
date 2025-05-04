using WasteIntoCity.Application.Structs;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkColleagueReportsService
    {
        Task CreateMarksAsync(Guid WorksId, Guid FromParticipantId, List<MarkColleaguePairStruct> workColleaguePairStructs);

        Task<List<WorkColleagueReport>> GetAllByWorksIdAsync(Guid worksId);
    }
}
