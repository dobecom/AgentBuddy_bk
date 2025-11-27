using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Core.Entities.Records
{
    [Table("case_statements")]
    public record CaseStatement : BaseRecord
    {
        [Required]
        [Column("case_id")]
        public Guid CaseId { get; init; }

        [Required]
        [MaxLength(1000)]
        [Column("symptom", TypeName = "varchar(1000)")]
        public required string Symptom { get; init; }

        [Required]
        [MaxLength(1000)]
        [Column("needs", TypeName = "varchar(1000)")]
        public required string Needs { get; init; }

        [Column("environments")]
        [JsonProperty("environments")]
        public string? Environments { get; init; }

        public Case? Case { get; init; }
    }
}
