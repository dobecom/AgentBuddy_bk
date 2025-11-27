using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Core.Entities.Records
{
    public abstract record BaseRecord
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; init; }

        [Required]
        [Column("created_at", TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        [MaxLength(255)]
        [Column("created_by", TypeName = "varchar(255)")]
        public string? CreatedBy { get; init; }

        [Required]
        [Column("updated_at", TypeName = "timestamp with time zone")]
        public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

        [MaxLength(255)]
        [Column("updated_by", TypeName = "varchar(255)")]
        public string? UpdatedBy { get; init; }
    }
}
