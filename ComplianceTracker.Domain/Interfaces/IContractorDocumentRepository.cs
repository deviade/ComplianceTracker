using ComplianceTracker.Domain.Entities;

namespace ComplianceTracker.Domain.Interfaces
{
    public interface IContractorDocumentRepository : IRepository<ContractorDocument>
    {
        Task<IEnumerable<ContractorDocument>> GetDocumentsByContractorIdAsync(int contractorId);
        Task<IEnumerable<ContractorDocument>> GetExpiringDocumentsAsync();
        Task<bool> HasActiveDocumentsByContractorAsync(int contractorId);
    }
}
