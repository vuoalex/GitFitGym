using GitFitGym.Domain;

namespace GitFitGym.Presentation;

public class WorkoutMenu(Gym gym)
{
    private readonly Gym _gym = gym;

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║          Workouts            ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1. View all workouts        ║");
            Console.WriteLine("║  2. View workout             ║");
            Console.WriteLine("║  3. Create workout           ║");
            Console.WriteLine("║  4. Add exercise to workout  ║");
            Console.WriteLine("║  5. Remove exercise          ║");
            Console.WriteLine("║  6. Delete workout           ║");
            Console.WriteLine("║  0. Back                     ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("\nSelect an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ViewAllWorkoutsAsync();
                    break;
                case "2":
                    await ViewWorkoutAsync();
                    break;
                case "3":
                    await CreateWorkoutAsync();
                    break;
                case "4":
                    await AddExerciseToWorkoutAsync();
                    break;
                case "5":
                    await RemoveExerciseFromWorkoutAsync();
                    break;
                case "6":
                    await DeleteWorkoutAsync();
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

    private async Task ViewAllWorkoutsAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Workouts ===\n");

        var workouts = await _gym.GetAllWorkoutsAsync();

        if (workouts.Count == 0)
        {
            Console.WriteLine("No workouts found.");
        }
        else
        {
            foreach (var w in workouts)
            {
                Console.WriteLine($"ID: {w.Id} | {w.Name}");
            }
        }

        MainMenu.Pause();
    }

    private async Task ViewWorkoutAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Workout ===\n");

        Console.Write("Enter workout ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var workout = await _gym.GetWorkoutWithExercisesAsync(id);

        if (workout is null)
        {
            Console.WriteLine("Workout not found.");
        }
        else
        {
            Console.WriteLine($"\nID: {workout.Id}");
            Console.WriteLine($"Name: {workout.Name}");

            Console.WriteLine("\nExercises:");
            if (workout.WorkoutExercises.Count == 0)
            {
                Console.WriteLine("No exercises in this workout.");
            }
            else
            {
                foreach (var we in workout.WorkoutExercises)
                {
                    Console.WriteLine($"- {we.Exercise?.Name ?? "Unknown"} | {we.Sets} sets x {we.Reps} reps");
                }
            }
        }

        MainMenu.Pause();
    }

    private async Task CreateWorkoutAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Workout ===\n");

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";

        try
        {
            var workout = await _gym.CreateWorkoutAsync(name);
            Console.WriteLine($"\nWorkout created with ID: {workout.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task AddExerciseToWorkoutAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Add Exercise to Workout ===\n");

        var workouts = await _gym.GetAllWorkoutsAsync();
        Console.WriteLine("Available workouts:");
        foreach (var w in workouts)
        {
            Console.WriteLine($"  ID: {w.Id} | {w.Name}");
        }

        Console.Write("\nEnter workout ID: ");
        if (!int.TryParse(Console.ReadLine(), out var workoutId))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var exercises = await _gym.GetAllExercisesAsync();
        Console.WriteLine("\nAvailable exercises:");
        foreach (var e in exercises)
        {
            Console.WriteLine($"  ID: {e.Id} | {e.Name}");
        }

        Console.Write("\nEnter exercise ID: ");
        if (!int.TryParse(Console.ReadLine(), out var exerciseId))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        Console.Write("Sets: ");
        if (!int.TryParse(Console.ReadLine(), out var sets))
        {
            Console.WriteLine("Invalid sets.");
            MainMenu.Pause();
            return;
        }

        Console.Write("Reps: ");
        if (!int.TryParse(Console.ReadLine(), out var reps))
        {
            Console.WriteLine("Invalid reps.");
            MainMenu.Pause();
            return;
        }

        try
        {
            await _gym.AddExerciseToWorkoutAsync(workoutId, exerciseId, sets, reps);
            Console.WriteLine("\nExercise added to workout!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task RemoveExerciseFromWorkoutAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Remove Exercise from Workout ===\n");

        var workoutExercises = await _gym.GetAllWorkoutExercisesAsync();

        if (workoutExercises.Count == 0)
        {
            Console.WriteLine("No workout exercises found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine("Workout-Exercise links:");
        foreach (var we in workoutExercises)
        {
            Console.WriteLine(
                $"  ID: {we.Id} | Workout: {we.WorkoutId} | Exercise: {we.ExerciseId} | {we.Sets}x{we.Reps}");
        }

        Console.Write("\nEnter workout-exercise ID to remove: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

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
            await _gym.RemoveExerciseFromWorkoutAsync(id);
            Console.WriteLine("\nExercise removed from workout!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task DeleteWorkoutAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Workout ===\n");

        Console.Write("Enter workout ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var workoutToDelete = await _gym.GetWorkoutByIdAsync(id);

        if (workoutToDelete is null)
        {
            Console.WriteLine("Workout not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine($"You are about to delete workout: {workoutToDelete.Name}");

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
            await _gym.DeleteWorkoutAsync(id);
            Console.WriteLine("\nWorkout deleted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }
}