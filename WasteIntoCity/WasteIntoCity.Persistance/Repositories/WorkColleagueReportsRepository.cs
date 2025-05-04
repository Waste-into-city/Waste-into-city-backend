using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkColleagueReportsRepository : IWorkColleaguesReportRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkColleagueReportsRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddMarksAsync(List<WorkColleagueReport> workColleagueReports)
        {
            List<WorkColleagueReportEntity> workColleagueReportEntities = workColleagueReports.Select(r => new WorkColleagueReportEntity
            {
                Id = r.Id,
                FromParticipantId = r.FromParticipantId,
                AboutColleagueId = r.AboutColleagueId,
                WorksId = r.WorksId,
                WorkMarkTypesId = (int)r.WorkMarkTypesId
            }).ToList();

            await _mainDbContext.WorkColleagueReports.AddRangeAsync(workColleagueReportEntities);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<List<WorkColleagueReport>> FindAllWithAboutColleagueByWorksIdAsync(Guid worksId)
        {
            List<WorkColleagueReportEntity> workApplicationEntities = await _mainDbContext.WorkColleagueReports.AsNoTracking()
                .Where(r => r.WorksId == worksId).Include(r => r.UserAboutColleague).ToListAsync()
                ?? throw new DbIsNotFoundException(nameof(WorkColleagueReport), 17, null);

            return workApplicationEntities.Select(a => WorkColleagueReport.Create(a.Id, a.FromParticipantId, a.AboutColleagueId, a.WorksId,
                (WorkMarkEnum)a.WorkMarkTypesId, null,
                User.Create(a.UserAboutColleague!.Id, Nickname.Create(a.UserAboutColleague.Nickname), Email.Create(a.UserAboutColleague.Email),
                Password.Create(a.UserAboutColleague.Password), a.UserAboutColleague.Ranking, null, a.UserAboutColleague.NegativeScore,
                a.UserAboutColleague.IsBanned, null, null))).ToList();
        }
    }
}
