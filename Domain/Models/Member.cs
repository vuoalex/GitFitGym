namespace GitFitGym.Domain.Models;

public class Member
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    
    public int? TrainerId { get; set; }
    
    public Trainer? Trainer { get; set; }
    public IReadOnlyList<Membership> Memberships { get; set; } = [];
}