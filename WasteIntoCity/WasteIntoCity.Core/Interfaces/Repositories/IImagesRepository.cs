using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IImagesRepository
    {
        Task Create(Image image);

        Task<List<ImageName>> FindNamesFirstNotReferencedByUploadedTimeInterval(int imagesAmount, TimeSpan minImageIntervalAfterFinished);

        Task UpdateWorksIdByNamesAsync(List<ImageName> imageNames, Guid? worksId);

        Task UpdateWorkApplicationsIdByNamesAsync(List<ImageName> imageNames, Guid? workApplicationsId);

        Task DeleteByNames(List<string> imageNames);
    }
}
