using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IWorkoutRepository
{
    Task<List<Workout>> GetAllAsync();
    Task<Workout?> GetByIdAsync(int id);
    Task CreateAsync(Workout workout);
    Task DeleteAsync(int id);
}