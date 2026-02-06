using GitFitGym.Domain;

namespace GitFitGym.Presentation;

public class MainMenu(Gym gym)
{
    private readonly Gym _gym = gym;

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║       GitFitGym        ║");
            Console.WriteLine("╠════════════════════════╣");
            Console.WriteLine("║  1. Members            ║");
            Console.WriteLine("║  2. Trainers           ║");
            Console.WriteLine("║  3. Membership Plans   ║");
            Console.WriteLine("║  4. Workouts           ║");
            Console.WriteLine("║  5. Exercises          ║");
            Console.WriteLine("║  0. Exit               ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.Write("\nSelect an option: ");
            
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await new MemberMenu(_gym).ShowAsync();
                    break;
                case "2":
                    await new TrainerMenu(_gym).ShowAsync();
                    break;
                case "3":
                    await new MembershipPlanMenu(_gym).ShowAsync();
                    break;
                case "4":
                    await new WorkoutMenu(_gym).ShowAsync();
                    break;
                case "5":
                    await new ExerciseMenu(_gym).ShowAsync();
                    break;
                case "6":
                    await new StatisticsMenu(_gym).ShowAsync();
                    break;
                case "0":
                    Console.WriteLine("\nGoodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option, try again. ");
                    Pause();
                    break;
            }
        }
    }

    // TODO: Move to helper class
    public static void Pause()
    {
        Console.WriteLine("\nPress any key to continue. . .");
        Console.ReadKey();
    }
}