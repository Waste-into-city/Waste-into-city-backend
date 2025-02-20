using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkColleagueReport
    {
        Task Create(WorkColleagueReport workColleagueReport);

        Task Delete(Guid id);

        Task<List<WorkColleagueReport>> Get();

        Task<WorkColleagueReport> GetById(Guid id);

        Task Update(WorkColleagueReport workColleagueReport);
    }
}
