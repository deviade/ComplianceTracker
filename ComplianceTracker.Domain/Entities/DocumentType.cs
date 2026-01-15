using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplianceTracker.Domain.Entities
{
    public class DocumentType : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public int? ValidityInDays { get; set; }

        public virtual ICollection<ContractorDocument> ContractorDocuments { get; set; } = new List<ContractorDocument>();
    }
}
