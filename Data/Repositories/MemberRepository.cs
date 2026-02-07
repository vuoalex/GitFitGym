using GitFitGym.Data.Entities;
using GitFitGym.Domain.Interfaces;
using GitFitGym.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data.Repositories;

public class MemberRepository : IMemberRepository
{
    public async Task<List<Member>> GetAllAsync()
    {
        await using var context = new AppDbContext();

        return await context.Members
            .AsNoTracking()
            .Select(x => new Member
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                JoinedAt = x.JoinedAt,
                TrainerId = x.TrainerId
            })
            .ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        await using var context = new AppDbContext();

        return await context.Members
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new Member
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                JoinedAt = x.JoinedAt,
                TrainerId = x.TrainerId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Member> CreateAsync(string firstName, string lastName, string email, int? trainerId)
    {
        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("First name, Last name and Email cannot be empty!");
        }

        await using var context = new AppDbContext();

        var existingAccount = await context.Members
            .FirstOrDefaultAsync(x => x.Email == email);

        if (existingAccount is not null)
        {
            throw new Exception("Email already exists!");
        }

        var newMember = new MemberEntity
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = email,
            TrainerId = trainerId
        };

        context.Members.Add(newMember);

        await context.SaveChangesAsync();

        return new Member
        {
            Id = newMember.Id,
            FirstName = newMember.FirstName,
            LastName = newMember.LastName,
            Email = newMember.Email,
            JoinedAt = newMember.JoinedAt,
            TrainerId = newMember.TrainerId
        };
    }

    public async Task<Member> UpdateAsync(Member member)
    {
        await using var context = new AppDbContext();

        var memberToUpdate = await context.Members.FindAsync(member.Id);

        if (memberToUpdate is null)
        {
            throw new Exception("Member not found!");
        }

        memberToUpdate.FirstName = member.FirstName;
        memberToUpdate.LastName = member.LastName;
        memberToUpdate.Email = member.Email;
        memberToUpdate.TrainerId = member.TrainerId;

        await context.SaveChangesAsync();

        return new Member
        {
            Id = memberToUpdate.Id,
            FirstName = memberToUpdate.FirstName,
            LastName = memberToUpdate.LastName,
            Email = memberToUpdate.Email,
            JoinedAt = memberToUpdate.JoinedAt,
            TrainerId = memberToUpdate.TrainerId
        };
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = new AppDbContext();

        var memberToRemove = await context.Members.FindAsync(id);

        if (memberToRemove is null)
        {
            throw new Exception("Member not found!");
        }

        context.Members.Remove(memberToRemove);

        await context.SaveChangesAsync();
    }
}