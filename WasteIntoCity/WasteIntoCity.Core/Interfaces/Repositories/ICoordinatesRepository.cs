using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ICoordinatesRepository
    {
        Task CreateAsync(Coordinates coordinates);
    }
}
