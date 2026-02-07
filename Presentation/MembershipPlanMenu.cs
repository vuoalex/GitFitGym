using GitFitGym.Domain;

namespace GitFitGym.Presentation;

public class MembershipPlanMenu(Gym gym)
{
    private readonly Gym _gym = gym;

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║   Membership Plans     ║");
            Console.WriteLine("╠════════════════════════╣");
            Console.WriteLine("║  1. View all plans     ║");
            Console.WriteLine("║  2. View plan          ║");
            Console.WriteLine("║  3. Create plan        ║");
            Console.WriteLine("║  4. Delete plan        ║");
            Console.WriteLine("║  0. Back               ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.Write("\nSelect an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ViewAllPlansAsync();
                    break;
                case "2":
                    await ViewPlanAsync();
                    break;
                case "3":
                    await CreatePlanAsync();
                    break;
                case "4":
                    await DeletePlanAsync();
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

    private async Task ViewAllPlansAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Membership Plans ===\n");

        var plans = await _gym.GetAllMembershipPlansAsync();

        if (plans.Count == 0)
        {
            Console.WriteLine("No plans found.");
        }
        else
        {
            foreach (var p in plans)
            {
                Console.WriteLine($"ID: {p.Id} | {p.Name} | {p.DurationDays} days | {p.Price:C}");
            }
        }

        MainMenu.Pause();
    }

    private async Task ViewPlanAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Plan ===\n");

        Console.Write("Enter plan ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var plan = await _gym.GetMembershipPlanByIdAsync(id);

        if (plan is null)
        {
            Console.WriteLine("Plan not found.");
        }
        else
        {
            Console.WriteLine($"\nID: {plan.Id}");
            Console.WriteLine($"Name: {plan.Name}");
            Console.WriteLine($"Duration: {plan.DurationDays} days");
            Console.WriteLine($"Price: {plan.Price:C}");
        }

        MainMenu.Pause();
    }

    private async Task CreatePlanAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Plan ===\n");

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("Duration (days): ");
        if (!int.TryParse(Console.ReadLine(), out var durationDays))
        {
            Console.WriteLine("Invalid duration.");
            MainMenu.Pause();
            return;
        }

        Console.Write("Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out var price))
        {
            Console.WriteLine("Invalid price.");
            MainMenu.Pause();
            return;
        }

        try
        {
            var plan = await _gym.CreateMembershipPlanAsync(name, durationDays, price);
            Console.WriteLine($"\nPlan created with ID: {plan.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }

    private async Task DeletePlanAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Plan ===\n");

        Console.Write("Enter plan ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            MainMenu.Pause();
            return;
        }

        var planToDelete = await _gym.GetMembershipPlanByIdAsync(id);

        if (planToDelete is null)
        {
            Console.WriteLine("Plan not found.");
            MainMenu.Pause();
            return;
        }

        Console.WriteLine(
            $"You are about to delete plan: {planToDelete.Name} | {planToDelete.DurationDays} days, {planToDelete.Price:C}");

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
            await _gym.DeleteMembershipPlanAsync(id);
            Console.WriteLine("\nPlan deleted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        MainMenu.Pause();
    }
}