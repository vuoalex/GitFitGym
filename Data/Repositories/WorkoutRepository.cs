using GitFitGym.Data.Entities;
using GitFitGym.Domain.Interfaces;
using GitFitGym.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data.Repositories;

public class WorkoutRepository : IWorkoutRepository
{
    public async Task<List<Workout>> GetAllAsync()
    {
        await using var context = new AppDbContext();

        return await context.Workouts
            .AsNoTracking()
            .Select(x => new Workout
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<Workout?> GetByIdAsync(int id)
    {
        await using var context = new AppDbContext();

        return await context.Workouts
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new Workout
            {
                Id = x.Id,
                Name = x.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Workout> CreateAsync(string name)
    {
        await using var context = new AppDbContext();

        var existingWorkout = await context.Workouts
            .FirstOrDefaultAsync(x => x.Name == name);

        if (existingWorkout is not null)
        {
            throw new Exception("Workout with this name already exists!");
        }

        var newWorkout = new WorkoutEntity
        {
            Name = name.Trim()
        };

        context.Workouts.Add(newWorkout);

        await context.SaveChangesAsync();

        return new Workout
        {
            Id = newWorkout.Id,
            Name = newWorkout.Name
        };
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = new AppDbContext();

        var workoutToDelete = await context.Workouts.FindAsync(id);

        if (workoutToDelete is null)
        {
            throw new Exception("Workout not found!");
        }

        context.Workouts.Remove(workoutToDelete);

        await context.SaveChangesAsync();
    }
}