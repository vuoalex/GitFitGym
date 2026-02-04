namespace GitFitGym.Domain.Models;

public class Trainer
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
    public DateTime JoinedAt { get; set; }

    public IReadOnlyList<Member> Members { get; set; } = [];
}