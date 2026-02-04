using GitFitGym.Data.Repositories;

namespace GitFitGym;

class Program
{
    static async Task Main(string[] args)
    {
        var memberRepository = new MemberRepository();

        // Testa GetAllAsync
        Console.WriteLine("=== All Members ===");
        var members = await memberRepository.GetAllAsync();
        foreach (var member in members)
        {
            Console.WriteLine($"ID: {member.Id} | {member.FirstName} {member.LastName} | {member.Email}");
        }

        // Testa GetByIdAsync
        Console.WriteLine("\n=== Member by ID ===");
        var singleMember = await memberRepository.GetByIdAsync(1);
        if (singleMember != null)
        {
            Console.WriteLine($"Found: {singleMember.FirstName} {singleMember.LastName}");
        }

        // Testa CreateAsync
        Console.WriteLine("\n=== Create Member ===");
        var newMember = await memberRepository.CreateAsync("Test", "Testsson", "test@email.com", null);
        Console.WriteLine($"Created: ID {newMember.Id} | {newMember.FirstName} {newMember.LastName}");

        // Testa UpdateAsync
        Console.WriteLine("\n=== Update Member ===");
        newMember.FirstName = "Updated";
        var updatedMember = await memberRepository.UpdateAsync(newMember);
        Console.WriteLine($"Updated: {updatedMember.FirstName} {updatedMember.LastName}");

        // Testa DeleteAsync
        Console.WriteLine("\n=== Delete Member ===");
        await memberRepository.DeleteAsync(newMember.Id);
        Console.WriteLine($"Deleted member with ID {newMember.Id}");

        // Verifiera borttagning
        Console.WriteLine("\n=== All Members (after delete) ===");
        var membersAfterDelete = await memberRepository.GetAllAsync();
        foreach (var member in membersAfterDelete)
        {
            Console.WriteLine($"ID: {member.Id} | {member.FirstName} {member.LastName}");
        }
    }
}