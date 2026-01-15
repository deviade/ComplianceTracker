using System.ComponentModel.DataAnnotations;

namespace ComplianceTracker.Shared.DTOs
{
    public class DocumentTypeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public int? ValidityInDays { get; set; }
        public bool IsRequired { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalDocuments { get; set; }
    }

    public class CreateDocumentTypeDto
    {
        [Required(ErrorMessage = "Document type name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Range(1, 3650, ErrorMessage = "Validity must be between 1 and 3650 days")]
        public int? ValidityInDays { get; set; }

        public bool IsRequired { get; set; }
    }

    public class UpdateDocumentTypeDto
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Range(1, 3650, ErrorMessage = "Validity must be between 1 and 3650 days")]
        public int? ValidityInDays { get; set; }

        public bool? IsRequired { get; set; }
    }
}
