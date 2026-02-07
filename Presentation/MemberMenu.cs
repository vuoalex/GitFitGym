using GitFitGym.Domain;

namespace GitFitGym.Presentation;

public class MemberMenu(Gym gym)
{
    private readonly Gym _gym = gym;

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║          Members             ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1. View all members         ║");
            Console.WriteLine("║  2. View member              ║");
            Console.WriteLine("║  3. Add member               ║");
            Console.WriteLine("║  4. Register + purchase plan ║");
            Console.WriteLine("║  5. Edit member              ║");
            Console.WriteLine("║  6. Delete member            ║");
            Console.WriteLine("║  7. Assign trainer           ║");
            Console.WriteLine("║  0. Back                     ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("\nSelect an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ViewAllMembersAsync();
                    break;
                case "2":
                    await ViewMemberAsync();
                    break;
                case "3":
                    await AddMemberAsync();
                    break;
                case "4":
                    await RegisterMemberWithPurchaseAsync();
                    break;
                case "5":
                    await EditMemberAsync();
                    break;
                case "6":
                    await DeleteMemberAsync();
                    break;
                case "7":
                    await AssignTrainerAsync();
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

    private async Task ViewAllMembersAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Members ===\n");

        var members = await _gym.GetAllMembersAsync();

        if (members.Count == 0)
        {
            Console.WriteLine("No members found.");
        }
        else
        {
            foreach (var m in members)
            {
                Console.WriteLine($"ID: {m.Id} | {m.FirstName} {m.LastName} | {m.Email}");
            }
        }

        MainMenu.Pause();
    }

    private async Task ViewMemberAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Member ===\n");

        Console.Write("Enter member ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var member = await _gym.GetMemberByIdAsync(id);

        if (member is null)
        {
            Console.WriteLine("Member not found.");
        }
        else
        {
            Console.WriteLine($"\nID: {member.Id}");
            Console.WriteLine($"Name: {member.FirstName} {member.LastName}");
            Console.WriteLine($"Email: {member.Email}");
            Console.WriteLine($"Joined: {member.JoinedAt:yyyy-MM-dd}");
            Console.WriteLine($"Trainer ID: {member.TrainerId?.ToString() ?? "None"}");
        }

        MainMenu.Pause();
    }

    private async Task AddMemberAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Add Member ===\n");

        Console.Write("First name: ");
        var firstName = Console.ReadLine() ?? "";

        Console.Write("Last name: ");
        var lastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";

        try
        {
            var member = await _gym.CreateMemberAsync(firstName, lastName, email);
            Console.WriteLine($"\nMember created with ID: {member.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task RegisterMemberWithPurchaseAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Register + Purchase Plan ===\n");

        Console.Write("First name: ");
        var firstName = Console.ReadLine() ?? "";

        Console.Write("Last name: ");
        var lastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";

        var plans = await _gym.GetAllMembershipPlansAsync();

        Console.WriteLine("\nAvailable membership plans:");
        foreach (var p in plans)
        {
            Console.WriteLine($"  ID: {p.Id} | {p.Name} | {p.DurationDays} days | {p.Price:C}");
        }

        Console.Write("\nEnter plan ID: ");
        if (!int.TryParse(Console.ReadLine(), out var planId))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        try
        {
            var member = await _gym.RegisterMemberWithPurchaseAsync(firstName, lastName, email, planId);
            Console.WriteLine($"\nMember registered with ID: {member.Id} and membership activated!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task EditMemberAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Edit Member ===\n");

        Console.Write("Enter member ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var member = await _gym.GetMemberByIdAsync(id);

        if (member is null)
        {
            Console.WriteLine("Member not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine($"\nCurrent: {member.FirstName} {member.LastName} ({member.Email})");

        Console.Write($"First name ({member.FirstName}): ");
        var firstName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(firstName)) member.FirstName = firstName;

        Console.Write($"Last name ({member.LastName}): ");
        var lastName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lastName)) member.LastName = lastName;

        Console.Write($"Email ({member.Email}): ");
        var email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(email)) member.Email = email;

        try
        {
            await _gym.UpdateMemberAsync(member);
            Console.WriteLine("\nMember updated!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task DeleteMemberAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Member ===\n");

        Console.Write("Enter member ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var memberToDelete = await _gym.GetMemberByIdAsync(id);

        if (memberToDelete is null)
        {
            Console.WriteLine("Member not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine($"You are about to delete member: " +
                          $"{memberToDelete.FirstName} " +
                          $"{memberToDelete.LastName} " +
                          $"| {memberToDelete.Email}");

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
            await _gym.DeleteMemberAsync(id);
            Console.WriteLine("\nMember deleted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task AssignTrainerAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Assign Trainer ===\n");

        Console.Write("Enter member ID: ");
        if (!int.TryParse(Console.ReadLine(), out var memberId))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var member = await _gym.GetMemberByIdAsync(memberId);

        if (member is null)
        {
            Console.WriteLine("Member not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine($"Assigning trainer to: {member.FirstName} {member.LastName} | ({member.Email})");
        Console.WriteLine($"Current trainer ID: {member.TrainerId?.ToString() ?? "None"}\n");

        var trainers = await _gym.GetAllTrainersAsync();

        Console.WriteLine("\nAvailable trainers:");
        foreach (var trainer in trainers)
        {
            Console.WriteLine($"  ID: {trainer.Id} | {trainer.FirstName} {trainer.LastName}");
        }

        Console.Write("\nEnter trainer ID (0 to remove): ");
        if (!int.TryParse(Console.ReadLine(), out var trainerId))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        member.TrainerId = trainerId == 0 ? null : trainerId;

        try
        {
            await _gym.UpdateMemberAsync(member);
            Console.WriteLine("\nTrainer assigned!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }
}