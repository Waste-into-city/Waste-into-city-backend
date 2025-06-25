using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class CoordinatesRepository : ICoordinatesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public CoordinatesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task CreateAsync(Coordinates coordinates)
        {
            CoordinatesEntity coordinatesEntity = new CoordinatesEntity
            {
                Id = coordinates.Id,
                Lat = coordinates.Lat,
                Lng = coordinates.Lng
            };

            await _mainDbContext.Coordinates.AddAsync(coordinatesEntity);
            await _mainDbContext.SaveChangesAsync();
        }
    }
}
