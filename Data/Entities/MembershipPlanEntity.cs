using System.ComponentModel.DataAnnotations;

namespace GitFitGym.Data.Entities;

public class MembershipPlanEntity
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    public int DurationDays { get; set; }
    public decimal Price { get; set; }

    // Navigation
    public ICollection<MembershipEntity> Memberships { get; set; } = [];
}