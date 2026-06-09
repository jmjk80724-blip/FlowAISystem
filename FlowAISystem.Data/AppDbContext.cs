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

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student -> Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);
            
            // Subject -> Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                 .HasOne( e => e.Subject)
                 .WithMany( s => s.Enrollments)
                 .HasForeignKey(e => e.SubjectId);

                // Enrollment -> Score (One-to-One)
            modelBuilder.Entity<Score>()
               .HasOne(s => s.Enrollment)
               .WithMany(e => e.Scores)
               .HasForeignKey(s => s.EnrollmentId); 

            // Enrollment -> Prediction
            modelBuilder.Entity<Prediction>()   
                .HasOne( p => p.Enrollment)    
                .WithMany( e => e.Predictions)  
                .HasForeignKey( p => p.EnrollmentId);
            //AIModel - Prediction (one to many)
            modelBuilder.Entity<Prediction>()
                 .HasOne( p => p.AIModel )
                 .WithMany( a => a.Predictions)
                 .HasForeignKey( p => p.AIModelId);

            // Feedback - Prediction (one to one )
            modelBuilder.Entity<Feedback>()
                .HasOne( f => f.Prediction)
                .WithOne(p => p.Feedback)
                .HasForeignKey<Feedback>( f => f.PredictionId);

            // Prediction - PredictionLog (one to many)
            modelBuilder.Entity<PredictionLog>()
                .HasOne(pl => pl.Prediction)
                .WithMany( p => p.PredictionLogs)
                .HasForeignKey( pl => pl.PredictionId);
            
            // AIModel - PredicitonLog 
            modelBuilder.Entity<PredictionLog>()
                 .HasOne (pl => pl.AIModel)
                 .WithMany( a => a.PredictionLogs)
                 .HasForeignKey( pl => pl.AIModelId);

             // AIModel → TrainingData (One-to-Many)
            modelBuilder.Entity<TrainingData>()
                .HasOne(t => t.AIModel)
                .WithMany(m => m.TrainingDataList)
                .HasForeignKey(t => t.AIModelId);

            // Role → Users (One-to-Many)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
   
}

