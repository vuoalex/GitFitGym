namespace GitFitGym.Domain.Models;

public class MembershipPlan
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
}