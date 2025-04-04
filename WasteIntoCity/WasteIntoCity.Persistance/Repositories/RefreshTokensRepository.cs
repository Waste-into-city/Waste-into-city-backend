using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions;
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

        public async Task AddAsync(RefreshTokenEntity refreshTokenEntity)
        {
            await _mainDbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            int addedRows = await _mainDbContext.SaveChangesAsync();

            if (addedRows == 0)
            {
                throw new DbAddException(nameof(RefreshTokenEntity), null);
            }
        }

        public async Task UpdateAsync(RefreshTokenEntity refreshTokenEntity)
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

        public async Task UpdateUsedByUserIdTokensAsync(bool used, Guid userId)
        {
            int updatedRows = await _mainDbContext.RefreshTokens
                .Where(r => r.UserId == userId)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(r => r.Used, used)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(RefreshTokenEntity), null);
            }
        }

        public async Task UpdateInvalidatedByUserIdTokensAsync(bool isInvalidated, Guid userId)
        {
            int updatedRows = await _mainDbContext.RefreshTokens
                .Where(r => r.UserId == userId)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(r => r.Invalidated, isInvalidated)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(RefreshTokenEntity), null);
            }
        }

        public async Task DeleteUsedAndInvalid(int recordsAtTimeAmount)
        {
            var tokensToDelete = _mainDbContext.RefreshTokens
                .Where(t => t.Invalidated || t.Used)
                .Take(recordsAtTimeAmount)
                .ToList();

            _mainDbContext.RefreshTokens.RemoveRange(tokensToDelete);
            await _mainDbContext.SaveChangesAsync();
        }
    }
}
