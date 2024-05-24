using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMS.Domain;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.ID);
            builder.Property(d => d.ID).ValueGeneratedOnAdd();

            builder.Property(d => d.Status).HasMaxLength(50).IsRequired();

            builder
                .HasMany(d => d.Tasks)
                .WithOne(t => t.Document)
                .HasForeignKey(t => t.DocumentID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.ActiveTask)
                .WithOne()
                .HasForeignKey<Document>(d => d.ActiveTaskID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}