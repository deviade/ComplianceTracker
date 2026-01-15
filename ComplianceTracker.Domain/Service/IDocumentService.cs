using ComplianceTracker.Domain.DTOs;
using ComplianceTracker.Domain.Entities;

namespace ComplianceTracker.Domain.Service
{
    public interface IDocumentService
    {
        Task<IEnumerable<ContractorDocumentDto>> GetDocumentsByContractorAsync(int contractorId);
        Task<ContractorDocumentDto> CreateDocumentAsync(int contractorId, CreateDocumentDto dto);
        Task<IEnumerable<ExpiringDocumentDto>> GetExpiringDocumentsAsync();
    }
}
