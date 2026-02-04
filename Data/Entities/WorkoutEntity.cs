using System.ComponentModel.DataAnnotations;

namespace GitFitGym.Data.Entities;

public class WorkoutEntity
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    // Navigation
    public ICollection<WorkoutExerciseEntity> WorkoutExercises { get; set; } = [];
}