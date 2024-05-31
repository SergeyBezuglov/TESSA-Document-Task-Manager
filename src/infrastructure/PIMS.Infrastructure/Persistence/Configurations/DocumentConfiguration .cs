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
        /// <summary>
        /// Конфигурация сущности Document 
        /// </summary>
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            /// <summary>
            /// Конфигурирует сущность Document.
            /// </summary>
            /// <param name="builder">Построитель сущности.</param>
            builder.ToTable("Documents");

            //Определяем первичный ключ для сущности.
            builder.HasKey(d => d.ID);
            builder.Property(d => d.ID).ValueGeneratedOnAdd();

            // Определяем свойство Status с максимальной длиной 50 и обязательностью
            builder.Property(d => d.Status).HasMaxLength(50).IsRequired();

            // Определяем отношение "один ко многим" между Document и ProjectTask.
            builder
                .HasMany(d => d.Tasks)
                .WithOne(t => t.Document)
                .HasForeignKey(t => t.DocumentID)
                .OnDelete(DeleteBehavior.Cascade);

            // Определяем отношение "один к одному" между Document и ActiveTask.
            builder.HasOne(d => d.ActiveTask)
                .WithOne()
                .HasForeignKey<Document>(d => d.ActiveTaskID)
                .OnDelete(DeleteBehavior.Restrict);  // При удалении документа ограничиваем удаление связанной активной задачи.
        }
    }
}