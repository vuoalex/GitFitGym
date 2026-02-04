using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface ITrainerRepository
{
    Task<List<Trainer>> GetAllAsync();
    Task<Trainer?> GetByIdAsync(int id);
    Task<Trainer> CreateAsync(string firstName, string lastName, string email, decimal salary);
    Task<Trainer> UpdateAsync(Trainer trainer);
    Task DeleteAsync(int id);
}