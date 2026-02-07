using GitFitGym.Domain.Interfaces;
using GitFitGym.Data.Entities;
using GitFitGym.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    public async Task<List<Exercise>> GetAllAsync()
    {
        await using var context = new AppDbContext();

        return await context.Exercises
            .AsNoTracking()
            .Select(x => new Exercise
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<Exercise?> GetByIdAsync(int id)
    {
        await using var context = new AppDbContext();

        return await context.Exercises
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new Exercise
            {
                Id = x.Id,
                Name = x.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Exercise> CreateAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Exercise name cannot be empty!");
        }

        await using var context = new AppDbContext();

        var existingExercise = await context.Exercises
            .FirstOrDefaultAsync(x => x.Name == name);

        if (existingExercise is not null)
        {
            throw new Exception("Exercise with this name already exists!");
        }

        var newExercise = new ExerciseEntity
        {
            Name = name.Trim()
        };

        context.Exercises.Add(newExercise);

        await context.SaveChangesAsync();

        return new Exercise
        {
            Id = newExercise.Id,
            Name = newExercise.Name
        };
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = new AppDbContext();

        var exerciseToDelete = await context.Exercises.FindAsync(id);

        if (exerciseToDelete is null)
        {
            throw new Exception("Exercise not found!");
        }

        context.Exercises.Remove(exerciseToDelete);

        await context.SaveChangesAsync();
    }
}