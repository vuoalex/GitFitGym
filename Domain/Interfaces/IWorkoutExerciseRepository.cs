using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IWorkoutExerciseRepository
{
    Task<List<WorkoutExercise>> GetAllAsync();
    Task<WorkoutExercise?> GetByIdAsync(int id);
    Task CreateAsync(WorkoutExercise workoutExercise);
    Task DeleteAsync(int id);
}