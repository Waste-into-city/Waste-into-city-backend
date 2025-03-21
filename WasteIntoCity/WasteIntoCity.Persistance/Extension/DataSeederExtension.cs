using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;

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

        private static void SeedEnumBasedEntities<TEnum, TEntity>(
            this DbContext context,
            DbSet<TEntity> dbSet,
            Func<TEnum, TEntity> entityCreator,
            string tableName
        ) where TEntity : class where TEnum : struct, Enum
        {
            HashSet<int> existingIds = dbSet
                .AsEnumerable()
                .Select(e => e.GetType().GetProperty("Id")?.GetValue(e) as int?)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToHashSet();

            IEnumerable<TEntity> requiredEntities = Enum.GetValues<TEnum>()
                .Cast<TEnum>()
                .Select(entityCreator);

            List<TEntity> missingEntities = requiredEntities
                .Where(entity => !existingIds.Contains(entity.GetType().GetProperty("Id")?.GetValue(entity) as int? ??
                throw new DbAddException(nameof(TEntity), "Init db error"))).ToList();

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
            context.SeedEnumBasedEntities<RoleType, RoleEntity>(
                context.Roles,
                role => new RoleEntity
                {
                    Id = (int)role,
                    Name = role.ToString()
                },
                RoleConfiguration.TABLE_NAME
            );
        }

        public static void SeedWorkReportComplaintStatusTypes(this MainDbContext context)
        {
            context.SeedEnumBasedEntities<WorkReportComplaintStatusType, WorkReportComplaintStatusTypeEntity>(
                context.WorkReportComplaintTypes,
                statusType => new WorkReportComplaintStatusTypeEntity
                {
                    Id = (int)statusType,
                    Name = statusType.ToString()
                },
                WorkReportComplaintStatusTypeConfiguration.TABLE_NAME
            );
        }
    }






}
