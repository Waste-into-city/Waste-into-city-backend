using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IImagesRepository
    {
        Task Create(Image image);

        Task<List<ImageName>> FindNamesFirstNotReferencedByFinished(int imagesAmount, TimeSpan minImageIntervalAfterFinished);

        Task DeleteByNames(List<string> imageNames);
    }
}
