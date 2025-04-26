using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;

namespace WasteIntoCity.Persistance.Extensions
{
    public static class TypesBasedDbExtension
    {
        private static async Task CustomExecuteSqlRowAsync(this DbContext context, string sqlQuery)
        {
#pragma warning disable EF1002
            await context.Database.ExecuteSqlRawAsync(sqlQuery);
#pragma warning restore EF1002
        }

        public static async Task AddToDbTypesBasedEntitiesIfEachNotExistById<TEntity>(
            this DbContext context,
            DbSet<TEntity> dbSet,
            IEnumerable<TEntity> entities,
            string tableName
        ) where TEntity : class
        {
            HashSet<int> existingIds = dbSet
                .AsEnumerable()
                .Select(e => e.GetType().GetProperty("Id")?.GetValue(e) as int?)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToHashSet();

            List<TEntity> missingEntities = entities
                .Where(entity => !existingIds.Contains(entity.GetType().GetProperty("Id")?.GetValue(entity) as int? ??
                        throw new DbAddException(nameof(TEntity), 5, "Init db error")))
                .ToList();

            if (missingEntities.Any())
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    await context.CustomExecuteSqlRowAsync($"SET IDENTITY_INSERT dbo.{tableName} ON");

                    await dbSet.AddRangeAsync(missingEntities);
                    await context.SaveChangesAsync();

                    await context.CustomExecuteSqlRowAsync($"SET IDENTITY_INSERT dbo.{tableName} OFF");

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw new Exception($"Error seeding {tableName}: {ex.Message}", ex);
                }
            }
        }
    }
}
