using FlowAISystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowAISystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        // Define DbSet properties for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<TrainingData> TrainingData { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<AIModel> AIModels { get; set; }
        public DbSet<PredictionLog> PredictionLogs { get; set; } 
    }
}

