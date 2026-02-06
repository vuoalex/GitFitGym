using GitFitGym.Domain;

namespace GitFitGym.Presentation;

public class StatisticsMenu(Gym gym)
{
    private readonly Gym _gym = gym;

    public async Task ShowAsync()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════╗");
        Console.WriteLine("║       Statistics       ║");
        Console.WriteLine("╚════════════════════════╝\n");

        // Count
        var memberCount = await _gym.GetMemberCountAsync();
        var trainerCount = await _gym.GetTrainerCountAsync();
        Console.WriteLine($"Total members: {memberCount}");
        Console.WriteLine($"Total trainers: {trainerCount}");

        // Sum
        var totalSalary = await _gym.GetTotalTrainerSalaryAsync();
        Console.WriteLine($"\nTotal trainer salaries: {totalSalary:C}");

        // Average
        var avgSalary = await _gym.GetAverageTrainerSalaryAsync();
        Console.WriteLine($"Average trainer salary: {avgSalary:C}");

        // GroupBy
        var membersPerTrainer = await _gym.GetMembersPerTrainerAsync();
        Console.WriteLine("\nMembers per trainer:");
        foreach (var entry in membersPerTrainer)
        {
            Console.WriteLine($"  {entry.Key}: {entry.Value} members");
        }

        MainMenu.Pause();
    }
}