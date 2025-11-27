using System.ComponentModel.DataAnnotations;

namespace Server.Core.Entities.DTO
{
    public class CaseAttachmentDTO : BaseDTO
    {
        public Guid CaseId { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string OriginalName { get; set; } = string.Empty;
        public string? Memo { get; set; }
    }

    public class CaseAttachmentCreateDTO
    {
        [Required(ErrorMessage = "File path is required.")]
        [StringLength(255, ErrorMessage = "File path cannot exceed 255 characters.")]
        public string Path { get; set; } = string.Empty;

        [Required(ErrorMessage = "File name is required.")]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Original file name is required.")]
        [StringLength(255, ErrorMessage = "Original file name cannot exceed 255 characters.")]
        public string OriginalName { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Memo cannot exceed 1000 characters.")]
        public string? Memo { get; set; }
    }

    public class CaseAttachmentUpdateDTO
    {
        [StringLength(255, ErrorMessage = "File path cannot exceed 255 characters.")]
        public string? Path { get; set; }

        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "Original file name cannot exceed 255 characters.")]
        public string? OriginalName { get; set; }

        [StringLength(1000, ErrorMessage = "Memo cannot exceed 1000 characters.")]
        public string? Memo { get; set; }
    }
}
