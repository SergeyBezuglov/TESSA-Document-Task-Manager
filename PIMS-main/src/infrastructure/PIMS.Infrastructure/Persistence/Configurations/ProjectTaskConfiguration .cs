using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PIMS.Domain;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable("ProjectTasks");

            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID).ValueGeneratedOnAdd();

            builder.Property(t => t.Name).HasMaxLength(255).IsRequired();

            builder.HasOne(t => t.PreviousTask)
                .WithOne()
                .HasForeignKey<ProjectTask>(t => t.PreviousTaskID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Document)
                .WithMany(d => d.Tasks)
                .HasForeignKey(t => t.DocumentID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}