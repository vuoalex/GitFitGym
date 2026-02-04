using System.ComponentModel.DataAnnotations;

namespace GitFitGym.Data.Entities;

public class MemberEntity
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    // Personal trainer
    public int? TrainerId { get; set; }
    public TrainerEntity? Trainer { get; set; }

    // Navigation
    public ICollection<MembershipEntity> Memberships { get; set; } = [];
}