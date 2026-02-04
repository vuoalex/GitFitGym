using GitFitGym.Domain.Models;

namespace GitFitGym.Domain.Interfaces;

public interface IMemberRepository
{
    Task<List<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member> CreateAsync(string firstName, string lastName, string email, int? trainerId);
    Task<Member> UpdateAsync(Member member);
    Task DeleteAsync(int id);
}