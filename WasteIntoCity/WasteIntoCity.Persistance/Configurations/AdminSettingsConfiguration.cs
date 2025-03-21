using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class AdminSettingsConfiguration : IEntityTypeConfiguration<AdminSettingsEntity>
    {
        public const string TABLE_NAME = "admin_settings";

        public void Configure(EntityTypeBuilder<AdminSettingsEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(a => a.Id).HasColumnName("id");

            builder.Property(a => a.TrueComplaintToAdditionRanking).IsRequired().HasColumnName("true_complaint_to_addition_ranking");

            builder.Property(a => a.FalseComplaintFromAdditionRanking).IsRequired().HasColumnName("false_complaint_from_addition_ranking");

            builder.Property(a => a.TrueComplaintFromAdditionRanking).IsRequired().HasColumnName("true_complaint_from_addition_ranking");

            builder.Property(a => a.AcceptableDifferenceReportTrashcanOccupancy).IsRequired().HasColumnName("acceptable_difference_report_trashcan_occupancy");

            builder.Property(a => a.FalseReportTrashcansOccupancyAdditionRanking).IsRequired().HasColumnName("false_report_trashcans_occupancy_addition_ranking");

            builder.HasKey(a => a.Id);
        }
    }
}
