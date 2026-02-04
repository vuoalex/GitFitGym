using System.ComponentModel.DataAnnotations;

namespace GitFitGym.Data.Entities;

public class TrainerEntity
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    public decimal Salary { get; set; }

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<MemberEntity> Members { get; set; } = [];
}