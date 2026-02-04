namespace GitFitGym.Domain.Models;

public class Workout
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public IReadOnlyList<WorkoutExercise> WorkoutExercises { get; set; } = [];
}