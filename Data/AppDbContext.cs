using GitFitGym.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data;

public class AppDbContext : DbContext
{
    // Core entities
    public DbSet<MemberEntity> Members { get; set; }
    public DbSet<TrainerEntity> Trainers { get; set; }

    // Memberships
    public DbSet<MembershipPlanEntity> MembershipPlans { get; set; }
    public DbSet<MembershipEntity> Memberships { get; set; }

    // Workouts
    public DbSet<WorkoutEntity> Workouts { get; set; }
    public DbSet<ExerciseEntity> Exercises { get; set; }
    public DbSet<WorkoutExerciseEntity> WorkoutExercises { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;" +
            "Database=gitfitgym_db;" +
            "Username=postgres;" +
            "Password=postgres"
        );

        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Member
        modelBuilder.Entity<MemberEntity>(x =>
        {
            x.ToTable("members");

            x.HasKey(e => e.Id);

            x.Property(e => e.FirstName).IsRequired();
            x.Property(e => e.LastName).IsRequired();

            x.Property(e => e.Email).IsRequired();
            x.HasIndex(e => e.Email).IsUnique();

            x.Property(e => e.JoinedAt)
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAdd();
            
            x.HasOne(e => e.Trainer)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.TrainerId)
                .OnDelete(DeleteBehavior.SetNull); // Trainer deleted -> member keeps existing
        });

        // Trainer
        modelBuilder.Entity<TrainerEntity>(x =>
        {
            x.ToTable("trainers");

            x.HasKey(e => e.Id);

            x.Property(e => e.FirstName).IsRequired();
            x.Property(e => e.LastName).IsRequired();

            x.Property(e => e.Email).IsRequired();
            x.HasIndex(e => e.Email).IsUnique();

            x.Property(e => e.Salary).HasPrecision(10, 2);

            x.Property(e => e.JoinedAt)
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAdd();
        });

        // MembershipPlan
        modelBuilder.Entity<MembershipPlanEntity>(x =>
        {
            x.ToTable("membership_plans");

            x.HasKey(e => e.Id);

            x.Property(e => e.Name).IsRequired();
            x.HasIndex(e => e.Name).IsUnique();

            x.Property(e => e.Price).HasPrecision(10, 2);
        });

        // Exercise
        modelBuilder.Entity<ExerciseEntity>(x =>
        {
            x.ToTable("exercises");
            
            x.HasKey(e => e.Id);

            x.Property(e => e.Name).IsRequired();
            x.HasIndex(e => e.Name).IsUnique();
        });

        // Workout
        modelBuilder.Entity<WorkoutEntity>(x =>
        {
            x.ToTable("workouts");

            x.HasKey(e => e.Id);
            
            x.Property(e => e.Name).IsRequired();
            x.HasIndex(e => e.Name).IsUnique();
        });

        // Junction table, Membership
        modelBuilder.Entity<MembershipEntity>(x =>
        {
            x.ToTable("memberships");

            x.HasKey(e => e.Id);
            
            // Convert enum to string for readability
            x.Property(e => e.Status).HasConversion<string>();
            
            x.HasOne(e => e.Member)
                .WithMany(e => e.Memberships)
                .HasForeignKey(e => e.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // Member deleted -> memberships deleted
            
            x.HasOne(e => e.MembershipPlan)
                .WithMany(e => e.Memberships)
                .HasForeignKey(e => e.MembershipPlanId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent plan deletion if member is using it
        });

        // Junction table, WorkoutExercise
        modelBuilder.Entity<WorkoutExerciseEntity>(x =>
        {
            x.ToTable("workout_exercises");

            x.HasKey(e => e.Id);

            // Prevent exercise more than once in the same workout
            x.HasIndex(e => new
            {
                e.WorkoutId,
                e.ExerciseId
            }).IsUnique();
            
            x.HasOne(e => e.Workout)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(e => e.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a workout removes its exercises
            
            x.HasOne(e => e.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(e => e.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting an exercise removes it from all workouts
        });
    }
}