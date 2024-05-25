using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PIMS.Domain;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация сущности ProjectTask 
    /// </summary>
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        /// <summary>
        /// Конфигурирует сущность ProjectTask.
        /// </summary>
        /// <param name="builder">Построитель сущности.</param>
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            // Устанавливаем таблицу для сущности ProjectTask.
            builder.ToTable("ProjectTasks");

            // Определяем первичный ключ для сущности.
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID).ValueGeneratedOnAdd();

            // Определяем свойство Name с максимальной длиной 255 и обязательностью.
            builder.Property(t => t.Name).HasMaxLength(255).IsRequired();

            // Определяем отношение "один к одному" между ProjectTask и PreviousTask.
            builder.HasOne(t => t.PreviousTask)
                .WithOne()
                .HasForeignKey<ProjectTask>(t => t.PreviousTaskID)
                .OnDelete(DeleteBehavior.Restrict);// При удалении задачи ограничиваем удаление связанной предыдущей задачи.

            // Определяем отношение "один ко многим" между ProjectTask и Document.
            builder.HasOne(t => t.Document)
                .WithMany(d => d.Tasks)
                .HasForeignKey(t => t.DocumentID)
                .OnDelete(DeleteBehavior.Cascade); // При удалении задачи удаляем все связанные задачи.
        }
    }
}