using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IMembershipRepository
{
    Task<List<Membership>> GetAllAsync();
    Task<Membership?> GetByIdAsync(int id);
    Task<Membership> CreateAsync(int memberId, int membershipPlanId);
    Task<Membership> UpdateAsync(Membership membership);
}