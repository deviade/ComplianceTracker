using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplianceTracker.Domain.Entities
{
    public class Contractor : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<ContractorDocument> Documents { get; set; } = new List<ContractorDocument>();
    }
}
