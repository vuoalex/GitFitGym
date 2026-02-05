using GitFitGym.Domain;
using GitFitGym.Presentation;

namespace GitFitGym;

class Program
{
    static async Task Main(string[] args)
    {
        var gym = new Gym();
        
        await new MainMenu(gym).ShowAsync();
    }
}