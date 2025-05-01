using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IImagesRepository
    {
        Task Create(Image image);

        Task<List<ImageName>> FindNamesFirstNotReferencedByUploadedTimeInterval(int imagesAmount, TimeSpan minImageIntervalAfterFinished);

        Task UploadWorksIdByNamesAsync(List<ImageName> imageNames, Guid? worksId);

        Task DeleteByNames(List<string> imageNames);
    }
}
