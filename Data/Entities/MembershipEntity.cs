using GitFitGym.Domain.Models;

namespace GitFitGym.Data.Entities;

// Junction table (Member <--> MembershipPlan)
public class MembershipEntity
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } // Null = lifetime membership
    public MembershipStatus Status { get; set; } = MembershipStatus.Active;

    // FK
    public int MemberId { get; set; }
    public int MembershipPlanId { get; set; }

    // Navigation
    public MemberEntity Member { get; set; } = null!;
    public MembershipPlanEntity MembershipPlan { get; set; } = null!;
}