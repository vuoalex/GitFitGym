using GitFitGym.Data.Entities;
using GitFitGym.Domain.Interfaces;
using GitFitGym.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data.Repositories;

public class MembershipRepository : IMembershipRepository
{
    public async Task<List<Membership>> GetAllAsync()
    {
        await using var context = new AppDbContext();

        return await context.Memberships
            .AsNoTracking()
            .Select(x => new Membership
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                MemberId = x.MemberId,
                MembershipPlanId = x.MembershipPlanId
            })
            .ToListAsync();
    }

    public async Task<Membership?> GetByIdAsync(int id)
    {
        await using var context = new AppDbContext();

        return await context.Memberships
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new Membership
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                MemberId = x.MemberId,
                MembershipPlanId = x.MembershipPlanId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Membership> CreateAsync(int memberId, int membershipPlanId)
    {
        await using var context = new AppDbContext();

        var existingMember = await context.Members.FindAsync(memberId);

        if (existingMember is null)
        {
            throw new Exception("Member not found!");
        }

        var existingPlan = await context.MembershipPlans.FindAsync(membershipPlanId);

        if (existingPlan is null)
        {
            throw new Exception("Membership plan not found!");
        }

        var startDate = DateTime.UtcNow;
        var endDate = existingPlan.DurationDays > 0
            ? startDate.AddDays(existingPlan.DurationDays)
            : (DateTime?)null; // Lifetime membership

        var newMembership = new MembershipEntity
        {
            StartDate = startDate,
            EndDate = endDate,
            Status = MembershipStatus.Active,
            MemberId = memberId,
            MembershipPlanId = membershipPlanId
        };

        context.Memberships.Add(newMembership);

        await context.SaveChangesAsync();

        return new Membership
        {
            Id = newMembership.Id,
            StartDate = newMembership.StartDate,
            EndDate = newMembership.EndDate,
            Status = newMembership.Status,
            MemberId = newMembership.MemberId,
            MembershipPlanId = newMembership.MembershipPlanId
        };
    }

    public async Task<Membership> UpdateAsync(Membership membership)
    {
        await using var context = new AppDbContext();

        var membershipToUpdate = await context.Memberships.FindAsync(membership.Id);

        if (membershipToUpdate is null)
        {
            throw new Exception("Membership not found!");
        }

        membershipToUpdate.Status = membership.Status;
        membershipToUpdate.EndDate = membership.EndDate;

        await context.SaveChangesAsync();

        return new Membership
        {
            Id = membershipToUpdate.Id,
            StartDate = membershipToUpdate.StartDate,
            EndDate = membershipToUpdate.EndDate,
            Status = membershipToUpdate.Status,
            MemberId = membershipToUpdate.MemberId,
            MembershipPlanId = membershipToUpdate.MembershipPlanId
        };
    }
}