using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Core.Entities.Records
{
    public enum CaseStatus
    {
        OPEN,
        PROCESSING,
        PROCESSED,
        CLOSED
    }

    [Table("cases")]
    public record Case : BaseRecord
    {
        [Required]
        [MaxLength(50)]
        [Column("number", TypeName = "varchar(50)")]
        public required string Number { get; init; }

        [Required]
        [MaxLength(100)]
        [Column("product_family", TypeName = "varchar(100)")]
        public required string ProductFamily { get; init; }

        [Required]
        [MaxLength(100)]
        [Column("product_name", TypeName = "varchar(100)")]
        public required string ProductName { get; init; }

        [MaxLength(50)]
        [Column("product_version", TypeName = "varchar(50)")]
        public string? ProductVersion { get; init; }

        [MaxLength(100)]
        [Column("category", TypeName = "varchar(100)")]
        public string? Category { get; init; }

        [MaxLength(100)]
        [Column("sub_category", TypeName = "varchar(100)")]
        public string? SubCategory { get; init; }

        [Required]
        [MaxLength(255)]
        [Column("title", TypeName = "varchar(255)")]
        public required string Title { get; init; }

        [Required]
        [Column("status", TypeName = "varchar(20)")]
        public CaseStatus Status { get; init; }

        public ICollection<CaseAttachment> Attachments { get; init; } = new List<CaseAttachment>();
        public ICollection<CaseResolution> Resolutions { get; init; } = new List<CaseResolution>();
        public ICollection<CaseStatement> Statements { get; init; } = new List<CaseStatement>();
    }
}
