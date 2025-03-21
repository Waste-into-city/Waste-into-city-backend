using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Persistance.Configurations;

namespace WasteIntoCity.Persistance.Extension
{
    public static class EnumSeederExtensions
    {
        private static void CustomExecuteSqlRow(this DbContext context, string sqlQuery)
        {
#pragma warning disable EF1002
            context.Database.ExecuteSqlRaw(sqlQuery);
#pragma warning restore EF1002
        }

        private static void SeedEntityBasedEntities<TEntity>(
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
                        throw new DbAddException(nameof(TEntity), "Init db error")))
                .ToList();

            if (missingEntities.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    context.CustomExecuteSqlRow($"SET IDENTITY_INSERT dbo.{tableName} ON");

                    dbSet.AddRange(missingEntities);
                    context.SaveChanges();

                    context.CustomExecuteSqlRow($"SET IDENTITY_INSERT dbo.{tableName} OFF");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error seeding {tableName}: {ex.Message}", ex);
                }
            }
        }


        public static void SeedRoles(this MainDbContext context)
        {
            context.SeedEntityBasedEntities(
                context.Roles,
                DefaultInitTypes.roleEntities,
                RoleConfiguration.TABLE_NAME
            );
        }

        public static void SeedWorkReportComplaintStatusTypes(this MainDbContext context)
        {
            context.SeedEntityBasedEntities(
                context.WorkReportComplaintTypes,
                DefaultInitTypes.workReportComplaintStatusTypeEntities,
                WorkReportComplaintStatusTypeConfiguration.TABLE_NAME
            );
        }

    }






}
