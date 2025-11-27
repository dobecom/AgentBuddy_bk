using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Server.Core.Entities.Records
{
    [Table("case_resolutions")]
    public record CaseResolution : BaseRecord
    {
        [Required]
        [Column("case_id")]
        public Guid CaseId { get; init; }

        [Column("content")]
        [JsonProperty("content")]
        public string? Content { get; set; }

        public Case? Case { get; init; }
    }
}
