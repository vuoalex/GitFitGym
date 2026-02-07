using GitFitGym.Data.Entities;
using GitFitGym.Domain.Interfaces;
using GitFitGym.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data.Repositories;

public class MembershipPlanRepository : IMembershipPlanRepository
{
    public async Task<List<MembershipPlan>> GetAllAsync()
    {
        await using var context = new AppDbContext();

        return await context.MembershipPlans
            .AsNoTracking()
            .Select(x => new MembershipPlan
            {
                Id = x.Id,
                Name = x.Name,
                DurationDays = x.DurationDays,
                Price = x.Price
            })
            .ToListAsync();
    }

    public async Task<MembershipPlan?> GetByIdAsync(int id)
    {
        await using var context = new AppDbContext();

        return await context.MembershipPlans
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new MembershipPlan
            {
                Id = x.Id,
                Name = x.Name,
                DurationDays = x.DurationDays,
                Price = x.Price
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MembershipPlan> CreateAsync(string name, int durationDays, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Plan name cannot be empty!");
        }

        await using var context = new AppDbContext();

        var existingPlan = await context.MembershipPlans
            .FirstOrDefaultAsync(x => x.Name == name && x.DurationDays == durationDays);

        if (existingPlan is not null)
        {
            throw new Exception("That plan already exists!");
        }

        var newPlan = new MembershipPlanEntity
        {
            Name = name.Trim(),
            DurationDays = durationDays < 1 ? 1 : durationDays,
            Price = price < 0m ? 0m : price
        };

        context.MembershipPlans.Add(newPlan);

        await context.SaveChangesAsync();

        return new MembershipPlan
        {
            Id = newPlan.Id,
            Name = newPlan.Name,
            DurationDays = newPlan.DurationDays,
            Price = newPlan.Price
        };
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = new AppDbContext();

        var planToDelete = await context.MembershipPlans.FindAsync(id);

        if (planToDelete is null)
        {
            throw new Exception("That plan does not exist!");
        }

        context.MembershipPlans.Remove(planToDelete);

        await context.SaveChangesAsync();
    }
}