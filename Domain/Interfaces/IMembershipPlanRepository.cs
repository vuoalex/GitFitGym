using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IMembershipPlanRepository
{
    Task<List<MembershipPlan>> GetAllAsync();
    Task<MembershipPlan?> GetByIdAsync(int id);
    Task<MembershipPlan> CreateAsync(string name, int durationDays, decimal price);
    Task DeleteAsync(int id);
}