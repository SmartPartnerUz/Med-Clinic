using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
