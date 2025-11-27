using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Server.Core.Entities.DTO
{
    public class CaseStatementDTO : BaseDTO
    {
        public Guid CaseId { get; set; }
        public string Symptom { get; set; } = string.Empty;
        public string Needs { get; set; } = string.Empty;

        [JsonProperty("environments")]
        public string? Environments { get; set; }
    }

    public class CaseStatementCreateDTO
    {
        [Required(ErrorMessage = "Symptom is required.")]
        [StringLength(1000, ErrorMessage = "Symptom cannot exceed 1000 characters.")]
        public string Symptom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Needs are required.")]
        [StringLength(1000, ErrorMessage = "Needs cannot exceed 1000 characters.")]
        public string Needs { get; set; } = string.Empty;

        [JsonProperty("environments")]
        public string? Environments { get; set; }
    }

    public class CaseStatementUpdateDTO
    {
        [StringLength(1000, ErrorMessage = "Symptom cannot exceed 1000 characters.")]
        public string? Symptom { get; set; }

        [StringLength(1000, ErrorMessage = "Needs cannot exceed 1000 characters.")]
        public string? Needs { get; set; }

        [JsonProperty("environments")]
        public string? Environments { get; set; }
    }
}