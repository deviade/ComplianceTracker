using ComplianceTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplianceTracker.Shared.DTOs
{
    public class ContractorDocumentDto
    {
        public int Id { get; set; }

        [Required]
        public int ContractorId { get; set; }
        public string ContractorName { get; set; } = string.Empty;

        [Required]
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; } = string.Empty;

        [Required]
        public DateTime UploadedOn { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public DocumentStatus Status { get; set; }
        public int DaysUntilExpiry { get; set; }
        public bool IsExpiringSoon => Status == DocumentStatus.ExpiringSoon;
        public bool IsExpired => Status == DocumentStatus.Expired;

        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public long? FileSize { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateDocumentDto
    {
        [Required(ErrorMessage = "Contractor ID is required")]
        public int ContractorId { get; set; }

        [Required(ErrorMessage = "Document type ID is required")]
        public int DocumentTypeId { get; set; }

        public DateTime? UploadedOn { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [StringLength(100, ErrorMessage = "File name cannot exceed 100 characters")]
        public string? FileName { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }

    public class UpdateDocumentDto
    {
        [Required]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UploadedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [StringLength(100, ErrorMessage = "File name cannot exceed 100 characters")]
        public string? FileName { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }

    public class DocumentStatusSummaryDto
    {
        public DocumentStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }
    public class ExpiringDocumentDto : ContractorDocumentDto
    {
        public string ContractorEmail { get; set; } = string.Empty;
        public string? ContractorPhone { get; set; }
        public int DaysThreshold { get; set; }
        public bool NotificationsSent { get; set; }
    }

    public class DocumentUploadDto
    {
        [Required]
        public int ContractorId { get; set; }

        [Required]
        public int DocumentTypeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public IFormFile File { get; set; } = null!;

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
