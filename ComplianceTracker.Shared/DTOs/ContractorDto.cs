using System.ComponentModel.DataAnnotations;

namespace ComplianceTracker.Shared.DTOs
{
    public class ContractorDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int TotalDocuments { get; set; }
        public int ValidDocuments { get; set; }
        public int ExpiringSoonDocuments { get; set; }
        public int ExpiredDocuments { get; set; }

        public IEnumerable<ContractorDocumentDto>? Documents { get; set; }
    }

    public class CreateContractorDto
    {
        [Required(ErrorMessage = "Contractor name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;
    }

    public class UpdateContractorDto
    {
        [Required(ErrorMessage = "Contractor ID is required")]
        public int Id { get; set; }

        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string? Email { get; set; }
    }

    public class ContractorSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int DocumentCount { get; set; }
        public DateTime? LastDocumentUpload { get; set; }
        public DateTime? NextExpiryDate { get; set; }
    }
}
