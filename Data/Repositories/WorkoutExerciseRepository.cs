using GitFitGym.Data.Entities;
using GitFitGym.Domain.Interfaces;
using GitFitGym.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data.Repositories;

public class WorkoutExerciseRepository : IWorkoutExerciseRepository
{
    public async Task<List<WorkoutExercise>> GetAllAsync()
    {
        await using var context = new AppDbContext();

        return await context.WorkoutExercises
            .AsNoTracking()
            .Select(x => new WorkoutExercise
            {
                Id = x.Id,
                WorkoutId = x.WorkoutId,
                ExerciseId = x.ExerciseId,
                Sets = x.Sets,
                Reps = x.Reps
            })
            .ToListAsync();
    }

    public async Task<WorkoutExercise?> GetByIdAsync(int id)
    {
        await using var context = new AppDbContext();

        return await context.WorkoutExercises
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new WorkoutExercise
            {
                Id = x.Id,
                WorkoutId = x.WorkoutId,
                ExerciseId = x.ExerciseId,
                Sets = x.Sets,
                Reps = x.Reps
            })
            .FirstOrDefaultAsync();
    }

    public async Task<WorkoutExercise> CreateAsync(int workoutId, int exerciseId, int sets, int reps)
    {
        await using var context = new AppDbContext();

        var existingWorkoutExercise = await context.WorkoutExercises
            .FirstOrDefaultAsync(x => x.WorkoutId == workoutId && x.ExerciseId == exerciseId);

        if (existingWorkoutExercise is not null)
        {
            throw new Exception("Workout exercise already exists!");
        }

        var newWorkoutExercise = new WorkoutExerciseEntity
        {
            WorkoutId = workoutId,
            ExerciseId = exerciseId,
            Sets = sets < 1 ? 1 : sets,
            Reps = reps < 1 ? 1 : reps
        };

        context.WorkoutExercises.Add(newWorkoutExercise);

        await context.SaveChangesAsync();

        return new WorkoutExercise
        {
            Id = newWorkoutExercise.Id,
            WorkoutId = newWorkoutExercise.WorkoutId,
            ExerciseId = newWorkoutExercise.ExerciseId,
            Sets = newWorkoutExercise.Sets,
            Reps = newWorkoutExercise.Reps
        };
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = new AppDbContext();

        var existingWorkoutExercise = await context.WorkoutExercises.FindAsync(id);

        if (existingWorkoutExercise is null)
        {
            throw new Exception("Workout exercise not found!");
        }

        context.WorkoutExercises.Remove(existingWorkoutExercise);

        await context.SaveChangesAsync();
    }
}