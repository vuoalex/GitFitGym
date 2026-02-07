using GitFitGym.Data.Entities;
using GitFitGym.Domain.Interfaces;
using GitFitGym.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GitFitGym.Data.Repositories;

public class TrainerRepository : ITrainerRepository
{
    public async Task<List<Trainer>> GetAllAsync()
    {
        await using var context = new AppDbContext();

        return await context.Trainers
            .AsNoTracking()
            .Select(x => new Trainer
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Salary = x.Salary,
                JoinedAt = x.JoinedAt
            })
            .ToListAsync();
    }

    public async Task<Trainer?> GetByIdAsync(int id)
    {
        await using var context = new AppDbContext();

        return await context.Trainers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new Trainer
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Salary = x.Salary,
                JoinedAt = x.JoinedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Trainer> CreateAsync(string firstName, string lastName, string email, decimal salary)
    {
        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("First name, Last name and Email cannot be empty!");
        }

        await using var context = new AppDbContext();

        var existingTrainer = await context.Trainers
            .FirstOrDefaultAsync(x => x.Email == email);

        if (existingTrainer is not null)
        {
            throw new Exception("Email already exists!");
        }

        var newTrainer = new TrainerEntity
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = email,
            Salary = salary < 0m ? 0m : salary
        };

        context.Trainers.Add(newTrainer);

        await context.SaveChangesAsync();

        return new Trainer
        {
            Id = newTrainer.Id,
            FirstName = newTrainer.FirstName,
            LastName = newTrainer.LastName,
            Email = newTrainer.Email,
            Salary = newTrainer.Salary,
            JoinedAt = newTrainer.JoinedAt
        };
    }

    public async Task<Trainer> UpdateAsync(Trainer trainer)
    {
        await using var context = new AppDbContext();

        var trainerToUpdate = await context.Trainers.FindAsync(trainer.Id);

        if (trainerToUpdate is null)
        {
            throw new Exception("Trainer not found!");
        }

        trainerToUpdate.FirstName = trainer.FirstName;
        trainerToUpdate.LastName = trainer.LastName;
        trainerToUpdate.Email = trainer.Email;
        trainerToUpdate.Salary = trainer.Salary < 0m ? 0m : trainer.Salary;

        await context.SaveChangesAsync();

        return new Trainer
        {
            Id = trainerToUpdate.Id,
            FirstName = trainerToUpdate.FirstName,
            LastName = trainerToUpdate.LastName,
            Email = trainerToUpdate.Email,
            Salary = trainerToUpdate.Salary,
            JoinedAt = trainerToUpdate.JoinedAt
        };
    }

    public async Task DeleteAsync(int id)
    {
        await using var context = new AppDbContext();

        var trainerToDelete = await context.Trainers.FindAsync(id);

        if (trainerToDelete is null)
        {
            throw new Exception("Trainer not found!");
        }

        context.Trainers.Remove(trainerToDelete);

        await context.SaveChangesAsync();
    }
}