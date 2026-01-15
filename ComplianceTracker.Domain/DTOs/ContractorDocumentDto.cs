using ComplianceTracker.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ComplianceTracker.Domain.DTOs
{
    public class ContractorDocumentDto
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime UploadedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DocumentStatus Status { get; set; }
    }

    public class CreateDocumentDto
    {
        [Required]
        public int DocumentTypeId { get; set; }

        public DateTime? UploadedOn { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
    }

    public class ExpiringDocumentDto
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; } = string.Empty;
        public int DocumentTypeId { get; set; }
        public DateTime UploadedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DocumentStatus Status { get; set; }
        public int DaysUntilExpiry { get; set; }
    }
}
