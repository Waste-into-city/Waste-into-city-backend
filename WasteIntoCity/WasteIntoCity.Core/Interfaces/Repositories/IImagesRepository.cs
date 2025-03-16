using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IImagesRepository
    {
        Task Create(Image image);

        Task Delete(Guid id);

        Task<List<Image>> Get();

        Task<Image> GetById(Guid id);

        Task Update(Image image);
    }
}
