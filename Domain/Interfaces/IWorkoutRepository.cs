using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IWorkoutRepository
{
    Task<List<Workout>> GetAllAsync();
    Task<Workout?> GetByIdAsync(int id);
    Task<Workout> CreateAsync(string name);
    Task DeleteAsync(int id);
}