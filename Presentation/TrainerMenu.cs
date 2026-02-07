using GitFitGym.Domain;

namespace GitFitGym.Presentation;

public class TrainerMenu(Gym gym)
{
    private readonly Gym _gym = gym;

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║        Trainers        ║");
            Console.WriteLine("╠════════════════════════╣");
            Console.WriteLine("║  1. View all trainers  ║");
            Console.WriteLine("║  2. View trainer       ║");
            Console.WriteLine("║  3. Add trainer        ║");
            Console.WriteLine("║  4. Edit trainer       ║");
            Console.WriteLine("║  5. Delete trainer     ║");
            Console.WriteLine("║  0. Back               ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.Write("\nSelect an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ViewAllTrainersAsync();
                    break;
                case "2":
                    await ViewTrainerAsync();
                    break;
                case "3":
                    await AddTrainerAsync();
                    break;
                case "4":
                    await EditTrainerAsync();
                    break;
                case "5":
                    await DeleteTrainerAsync();
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

    private async Task ViewAllTrainersAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Trainers ===\n");

        var trainers = await _gym.GetAllTrainersAsync();

        if (trainers.Count == 0)
        {
            Console.WriteLine("No trainers found.");
        }
        else
        {
            foreach (var t in trainers)
            {
                Console.WriteLine($"ID: {t.Id} | {t.FirstName} {t.LastName} | {t.Email} | Salary: {t.Salary:C}");
            }
        }

        MainMenu.Pause();
    }

    private async Task ViewTrainerAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Trainer ===\n");

        Console.Write("Enter trainer ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var trainer = await _gym.GetTrainerByIdAsync(id);

        if (trainer is null)
        {
            Console.WriteLine("Trainer not found.");
        }
        else
        {
            Console.WriteLine($"\nID: {trainer.Id}");
            Console.WriteLine($"Name: {trainer.FirstName} {trainer.LastName}");
            Console.WriteLine($"Email: {trainer.Email}");
            Console.WriteLine($"Salary: {trainer.Salary:C}");
            Console.WriteLine($"Joined: {trainer.JoinedAt:yyyy-MM-dd}");
        }

        MainMenu.Pause();
    }

    private async Task AddTrainerAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Add Trainer ===\n");

        Console.Write("First name: ");
        var firstName = Console.ReadLine() ?? "";

        Console.Write("Last name: ");
        var lastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";

        Console.Write("Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out var salary))
        {
            Console.WriteLine("Invalid salary.");
            MainMenu.Pause();
            return;
        }

        try
        {
            var trainer = await _gym.CreateTrainerAsync(firstName, lastName, email, salary);
            Console.WriteLine($"\nTrainer created with ID: {trainer.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task EditTrainerAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Edit Trainer ===\n");

        Console.Write("Enter trainer ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var trainer = await _gym.GetTrainerByIdAsync(id);

        if (trainer is null)
        {
            Console.WriteLine("Trainer not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine(
            $"\nCurrent: {trainer.FirstName} {trainer.LastName} ({trainer.Email}) - Salary: {trainer.Salary:C}");

        Console.Write($"First name ({trainer.FirstName}): ");
        var firstName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(firstName)) trainer.FirstName = firstName;

        Console.Write($"Last name ({trainer.LastName}): ");
        var lastName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lastName)) trainer.LastName = lastName;

        Console.Write($"Email ({trainer.Email}): ");
        var email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(email)) trainer.Email = email;

        Console.Write($"Salary ({trainer.Salary}): ");
        var salaryInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(salaryInput) && decimal.TryParse(salaryInput, out var salary))
        {
            trainer.Salary = salary;
        }

        try
        {
            await _gym.UpdateTrainerAsync(trainer);
            Console.WriteLine("\nTrainer updated!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task DeleteTrainerAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Trainer ===\n");

        Console.Write("Enter trainer ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var trainerToDelete = await _gym.GetTrainerByIdAsync(id);

        if (trainerToDelete is null)
        {
            Console.WriteLine("Trainer not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine(
            $"You are about to delete trainer: {trainerToDelete.FirstName} {trainerToDelete.LastName} | {trainerToDelete.Email}");

        Console.Write("Are you sure? (y/n): ");
        var confirm = Console.ReadLine()?.ToLower();

        if (confirm != "y")
        {
            Console.WriteLine("Cancelled.");
            MainMenu.Pause();
            return;
        }

        try
        {
            await _gym.DeleteTrainerAsync(id);
            Console.WriteLine("\nTrainer deleted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }
}