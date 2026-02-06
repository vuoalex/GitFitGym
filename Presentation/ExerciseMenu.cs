using GitFitGym.Domain;

namespace GitFitGym.Presentation;

public class ExerciseMenu(Gym gym)
{
    private readonly Gym _gym = gym;

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║       Exercises        ║");
            Console.WriteLine("╠════════════════════════╣");
            Console.WriteLine("║  1. View all exercises ║");
            Console.WriteLine("║  2. View exercise      ║");
            Console.WriteLine("║  3. Add exercise       ║");
            Console.WriteLine("║  4. Delete exercise    ║");
            Console.WriteLine("║  0. Back               ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.Write("\nSelect an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ViewAllExercisesAsync();
                    break;
                case "2":
                    await ViewExerciseAsync();
                    break;
                case "3":
                    await AddExerciseAsync();
                    break;
                case "4":
                    await DeleteExerciseAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    MainMenu.Pause();
                    break;
            }
        }
    }

    private async Task ViewAllExercisesAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Exercises ===\n");
        
        var exercises = await _gym.GetAllExercisesAsync();

        if (exercises.Count == 0)
        {
            Console.WriteLine("No exercises found.");
            MainMenu.Pause();
            return;
        }

        foreach (var e in exercises)
        {
            Console.WriteLine($"ID: {e.Id} | {e.Name}");
        }

        MainMenu.Pause();
    }

    private async Task ViewExerciseAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Exercise ===\n");

        Console.Write("Enter exercise ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var exercise = await _gym.GetExerciseByIdAsync(id);

        if (exercise is null)
        {
            Console.WriteLine("Exercise not found.");
        }
        else
        {
            Console.WriteLine($"\nID: {exercise.Id}");
            Console.WriteLine($"Name: {exercise.Name}");
        }

        MainMenu.Pause();
    }

    private async Task AddExerciseAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Add Exercise ===\n");

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";

        try
        {
            var exercise = await _gym.CreateExerciseAsync(name);
            Console.WriteLine($"\nExercise created with ID: {exercise.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task DeleteExerciseAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Exercise ===\n");

        Console.Write("Enter exercise ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var exercise = await _gym.GetExerciseByIdAsync(id);

        if (exercise is null)
        {
            Console.WriteLine("Exercise not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine($"You are about to delete exercise: {exercise.Name}.");

        Console.Write("Are you sure? (y/n): ");
        var confirm = Console.ReadLine()?.ToLower();

        if (confirm == "n")
        {
            Console.WriteLine("Cancelled.");
            MainMenu.Pause();
            return;
        }

        try
        {
            await _gym.DeleteExerciseAsync(id);
            Console.WriteLine("Exercise deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }
}