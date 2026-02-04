using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IExerciseRepository
{
    Task<List<Exercise>> GetAllAsync();
    Task<Exercise?> GetByIdAsync(int id);
    Task<Exercise> CreateAsync(string name);
    Task DeleteAsync(int id);
}