using Newtonsoft.Json;
using System.Text.Json;

namespace Server.Core.Entities.DTO
{
    public class CaseResolutionDTO : BaseDTO
    {
        public Guid CaseId { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }
    }

    public class CaseResolutionCreateDTO
    {
        [JsonProperty("content")]
        public string? Content { get; set; }
    }

    public class CaseResolutionUpdateDTO
    {
        [JsonProperty("content")]
        public string? Content { get; set; }
    }
}