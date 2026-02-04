namespace GitFitGym.Presentation;

public class MainMenu
{
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
                    // Member menu
                    break;
                case "2":
                    // Trainer menu
                    break;
                case "3":
                    // Membership Plan menu
                    break;
                case "4":
                    // Workout menu
                    break;
                case "5":
                    // Exercise menu
                    break;
                case "0":
                    Console.WriteLine("\nGoodbye!");
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Press any key . . .");
                    break;
            }
        }
    }
}