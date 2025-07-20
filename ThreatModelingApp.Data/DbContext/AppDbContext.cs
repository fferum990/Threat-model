using Microsoft.EntityFrameworkCore;
using ThreatModelingApp.Core.Models;
using ThreatModelingApp.Core.Enums;

namespace ThreatModelingApp.Data.DbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Таблица угроз
        public DbSet<Threat> Threats { get; set; }

        // Таблица вопросов
        public DbSet<Question> Questions { get; set; }

        // Таблица ответов
        public DbSet<Answer> Answers { get; set; }

        // Таблица шаблонов документов
        public DbSet<DocumentTemplate> DocumentTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация модели Threat
            modelBuilder.Entity<Threat>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(t => t.Category)
                    .HasConversion<string>();
                
                entity.Property(t => t.Level)
                    .HasConversion<string>();

                // Связь один-ко-многим с рекомендациями
                entity.HasMany(t => t.Recommendations)
                    .WithOne()
                    .HasForeignKey(r => r.ThreatId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Связь с условиями угроз
                entity.OwnsMany(t => t.Conditions, c =>
                {
                    c.WithOwner().HasForeignKey("ThreatId");
                    c.Property<int>("Id");
                    c.HasKey("Id");
                });
            });

            // Конфигурация модели Question
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Text)
                    .IsRequired()
                    .HasMaxLength(500);
                
                entity.Property(q => q.Type)
                    .HasConversion<string>();
                
                entity.Property(q => q.Section)
                    .HasConversion<string>();

                // Связь один-ко-многим с вариантами ответов
                entity.HasMany(q => q.PossibleAnswers)
                    .WithOne()
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация модели Answer
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(a => a.Id);
                
                // Связь с вопросом
                entity.HasOne<Question>()
                    .WithMany()
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация модели DocumentTemplate
            modelBuilder.Entity<DocumentTemplate>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(t => t.FilePath)
                    .IsRequired();
            });

            // Начальные данные (seed data)
            modelBuilder.Entity<Threat>().HasData(SeedData.GetThreats());
            modelBuilder.Entity<Question>().HasData(SeedData.GetQuestions());
            modelBuilder.Entity<DocumentTemplate>().HasData(SeedData.GetTemplates());
        }
    }
}