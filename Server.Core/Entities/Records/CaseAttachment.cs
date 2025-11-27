using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Core.Entities.Records
{
    [Table("case_attachments")]
    public record CaseAttachment : BaseRecord
    {
        [Required]
        [Column("case_id")]
        public Guid CaseId { get; init; }

        [Required]
        [MaxLength(255)]
        [Column("path", TypeName = "varchar(255)")]
        public required string Path { get; init; }

        [Required]
        [MaxLength(255)]
        [Column("name", TypeName = "varchar(255)")]
        public required string Name { get; init; }

        [Required]
        [MaxLength(255)]
        [Column("original_name", TypeName = "varchar(255)")]
        public required string OriginalName { get; init; }

        [MaxLength(1000)]
        [Column("memo", TypeName = "text")]
        public string? Memo { get; init; }

        public Case? Case { get; init; }
    }
}
