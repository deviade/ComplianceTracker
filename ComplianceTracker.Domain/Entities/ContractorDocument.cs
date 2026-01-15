using ComplianceTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplianceTracker.Domain.Entities
{
    public class ContractorDocument : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public int ContractorId { get; set; }

        [Required]
        public int DocumentTypeId { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public DocumentStatus Status { get; set; }

        // Navigation properties
        public virtual Contractor Contractor { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
