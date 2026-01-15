using ComplianceTracker.Domain.Entities;
using ComplianceTracker.Domain.Interfaces;

namespace ComplianceTracker.Domain.Interfaces
{
    public interface IContractorRepository : IRepository<Contractor>
    {
        Task<Contractor?> GetContractorWithDocumentsAsync(int id);
        Task<bool> HasDocumentsAsync(int id);
    }
}
