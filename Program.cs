using GitFitGym.Data.Repositories;
using GitFitGym.Presentation;

namespace GitFitGym;

class Program
{
    static async Task Main(string[] args)
    {
        await new MainMenu().ShowAsync();
    }
}