using System.ComponentModel.DataAnnotations;

namespace ContractorManagement.Components.Model
{
    public class Contractor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    public class ContractorDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<ContractorDocument> Documents { get; set; } = new();

        public class ContractorDocument
        {
            public int Id { get; set; }
            public int DocumentTypeId { get; set; }
            public DateTime UploadedOn { get; set; }
            public DateTime ExpiryDate { get; set; }
            public DocumentStatus Status { get; set; }
        }

        public enum DocumentStatus
        {
            Valid,
            ExpiringSoon,
            Expired
        }
        public class CreateContractorDto
        {
            [Required]
            [StringLength(200)]
            public string Name { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; } = string.Empty;
        }

        public class UpdateContractorDto
        {
            [Required]
            public int Id { get; set; }

            [Required]
            [StringLength(200)]
            public string Name { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; } = string.Empty;
        }
    }
}
