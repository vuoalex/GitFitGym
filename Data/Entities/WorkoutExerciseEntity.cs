namespace GitFitGym.Data.Entities;

// Junction table (Workout <--> Exercise)
public class WorkoutExerciseEntity
{
    public int Id { get; set; }

    public int Sets { get; set; }
    public int Reps { get; set; }

    // FK
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }

    // Navigation
    public WorkoutEntity Workout { get; set; } = null!;
    public ExerciseEntity Exercise { get; set; } = null!;
}