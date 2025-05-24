using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public ImagesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task Create(Image image)
        {
            ImageEntity imageEntity = new ImageEntity
            {
                Id = image.Id,
                Name = image.Name.Value,
                UploadedTime = image.UploadedTime,
            };

            await _mainDbContext.Images.AddAsync(imageEntity);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task DeleteByNames(List<string> imageNames)
        {
            IQueryable<ImageEntity> images = _mainDbContext.Images.Where(img => imageNames.Contains(img.Name));
            _mainDbContext.Images.RemoveRange(images);

            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<List<ImageName>> FindNamesFirstNotReferencedByUploadedTimeInterval(int imagesAmount, TimeSpan minImageIntervalAfterUpdated)
        {
            DateTime minAppropriateUploadedWorkTime = DateTime.UtcNow.Subtract(minImageIntervalAfterUpdated);

            List<ImageEntity> imageEntities = await _mainDbContext.Images
                .Where(w => w.UploadedTime <= minAppropriateUploadedWorkTime && w.WorkApplicationsId == null && w.WorkReportComplaintsId == null
                    && w.WorkReportResultsId == null)
                .Take(imagesAmount)
                .ToListAsync();

            List<ImageName> imageNames = imageEntities.Select(w =>
                ImageName.Create(
                    w.Name
                )
            ).ToList();

            return imageNames;
        }

        public async Task UpdateWorksIdByNamesAsync(List<ImageName> imageNames, Guid? worksId)
        {
            List<string> imageNamesLines = imageNames.Select(i => i.Value).ToList();

            int updatedRows = await _mainDbContext.Images
                .Where(w => imageNamesLines.Contains(w.Name))
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.WorksId, worksId)
                );
        }

        public async Task UpdateWorkApplicationsIdByNamesAsync(List<ImageName> imageNames, Guid? workApplicationsId)
        {
            List<string> imageNamesLines = imageNames.Select(i => i.Value).ToList();

            int updatedRows = await _mainDbContext.Images
                .Where(w => imageNamesLines.Contains(w.Name))
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.WorkApplicationsId, workApplicationsId)
                );
        }

        public async Task UpdateWorkReportComplaintsIdByNamesAsync(List<ImageName> imageNames, Guid? workReportComplaintsId)
        {
            List<string> imageNamesLines = imageNames.Select(i => i.Value).ToList();

            int updatedRows = await _mainDbContext.Images
                .Where(w => imageNamesLines.Contains(w.Name))
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.WorkReportComplaintsId, workReportComplaintsId)
                );
        }

        public async Task UpdateUserIdByNameAsync(ImageName imageName, Guid? usersId)
        {
            int updatedRows = await _mainDbContext.Images
                .Where(w => imageName.Value == w.Name)
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.UsersId, usersId)
                );
        }
    }
}
