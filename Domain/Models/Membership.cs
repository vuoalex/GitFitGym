namespace GitFitGym.Domain.Models;

public enum MembershipStatus
{
    Active,
    Expired,
    Cancelled
}

public class Membership
{
    public int Id { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public MembershipStatus Status { get; set; } = MembershipStatus.Active;
    
    public int MemberId { get; set; }
    public Member? Member { get; set; }
    
    public int MembershipPlanId { get; set; }
    public MembershipPlan? MembershipPlan { get; set; }
}