using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Errors;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class RefreshTokensRepository
    {
        private readonly MainDbContext _mainDbContext;

        public RefreshTokensRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<RefreshTokenEntity> FindByValueAsync(string value)
        {
            RefreshTokenEntity refreshTokenEntity = await _mainDbContext.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(r => r.Value.ToString() == value)
                ?? throw new DbIsNotFoundException(nameof(RefreshTokenEntity), null);

            return refreshTokenEntity;
        }

        public async Task<RefreshTokenEntity> FindByJwtIdAsync(string jwtId)
        {
            RefreshTokenEntity refreshTokenEntity = await _mainDbContext.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(r => r.JwtId == jwtId)
                ?? throw new DbIsNotFoundException(nameof(RefreshTokenEntity), null);

            return refreshTokenEntity;
        }

        public async Task Add(RefreshTokenEntity refreshTokenEntity)
        {
            await _mainDbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            int addedRows = await _mainDbContext.SaveChangesAsync();

            if (addedRows == 0)
            {
                throw new DbAddException(nameof(RefreshTokenEntity), null);
            }
        }

        public async Task Update(RefreshTokenEntity refreshTokenEntity)
        {
            int updatedRows = await _mainDbContext.RefreshTokens
                .Where(r => r.Value == refreshTokenEntity.Value)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(r => r.JwtId, refreshTokenEntity.JwtId)
                    .SetProperty(r => r.CreationTimestamp, refreshTokenEntity.CreationTimestamp)
                    .SetProperty(r => r.ExpirationTimestamp, refreshTokenEntity.ExpirationTimestamp)
                    .SetProperty(r => r.Used, refreshTokenEntity.Used)
                    .SetProperty(r => r.Invalidated, refreshTokenEntity.Invalidated)
                    .SetProperty(r => r.UserId, refreshTokenEntity.UserId)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(RefreshTokenEntity), null);
            }
        }

        public async Task UpdateUsedByUserIdTokens(bool used, string userId)
        {
            int updatedRows = await _mainDbContext.RefreshTokens
                .Where(r => r.UserId.ToString() == userId)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(r => r.Used, used)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(RefreshTokenEntity), null);
            }
        }
    }
}
