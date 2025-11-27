using Server.Core.Entities.Records;
using System.ComponentModel.DataAnnotations;

namespace Server.Core.Entities.DTO
{
    public class CaseDTO : BaseDTO
    {
        public string Number { get; set; } = string.Empty;
        public string ProductFamily { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string? ProductVersion { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string Title { get; set; } = string.Empty;
        public CaseStatus Status { get; set; } = CaseStatus.OPEN;

        public ICollection<CaseAttachmentDTO> Attachments { get; set; } = new List<CaseAttachmentDTO>();
        public ICollection<CaseResolutionDTO> Resolutions { get; set; } = new List<CaseResolutionDTO>();
        public ICollection<CaseStatementDTO> Statements { get; set; } = new List<CaseStatementDTO>();
    }

    public class CaseCreateDTO
    {
        [Required(ErrorMessage = "Case number is required.")]
        [StringLength(50, ErrorMessage = "Case number cannot exceed 50 characters.")]
        public string Number { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product family is required.")]
        [StringLength(100, ErrorMessage = "Product family cannot exceed 100 characters.")]
        public string ProductFamily { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Product version cannot exceed 50 characters.")]
        public string? ProductVersion { get; set; }

        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string? Category { get; set; }

        [StringLength(100, ErrorMessage = "Sub-category cannot exceed 100 characters.")]
        public string? SubCategory { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(CaseStatus), ErrorMessage = "Invalid status value.")]
        public string Status { get; set; } = string.Empty;

        public ICollection<CaseAttachmentCreateDTO> Attachments { get; set; } = new List<CaseAttachmentCreateDTO>();
        public ICollection<CaseResolutionCreateDTO> Resolutions { get; set; } = new List<CaseResolutionCreateDTO>();
        public ICollection<CaseStatementCreateDTO> Statements { get; set; } = new List<CaseStatementCreateDTO>();
    }


    public class CaseUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Case number cannot exceed 50 characters.")]
        public string? Number { get; set; }

        [StringLength(100, ErrorMessage = "Product family cannot exceed 100 characters.")]
        public string? ProductFamily { get; set; }

        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string? ProductName { get; set; }

        [StringLength(50, ErrorMessage = "Product version cannot exceed 50 characters.")]
        public string? ProductVersion { get; set; }

        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string? Category { get; set; }

        [StringLength(100, ErrorMessage = "Sub-category cannot exceed 100 characters.")]
        public string? SubCategory { get; set; }

        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string? Title { get; set; }

        [EnumDataType(typeof(CaseStatus), ErrorMessage = "Invalid status value.")]
        public string? Status { get; set; }

        public ICollection<CaseAttachmentUpdateDTO>? Attachments { get; set; }
        public ICollection<CaseResolutionUpdateDTO>? Resolutions { get; set; }
        public ICollection<CaseStatementUpdateDTO>? Statements { get; set; }
    }

    public class CaseListDTO : BaseDTO
    {
        public string Number { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ProductFamily { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
    }
}