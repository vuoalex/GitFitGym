using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IWorkoutExerciseRepository
{
    Task<List<WorkoutExercise>> GetAllAsync();
    Task<WorkoutExercise?> GetByIdAsync(int id);
    Task<WorkoutExercise> CreateAsync(int workoutId, int exerciseId, int sets, int reps);
    Task DeleteAsync(int id);
}