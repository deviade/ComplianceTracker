
using ComplianceTracker.Domain.Entities;
using ComplianceTracker.Domain.Enum;
using ComplianceTracker.Domain.Interfaces;
using ComplianceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComplianceTracker.Infrastructure.Repositories
{
    public class ContractorDocumentRepository : GenericRepository<ContractorDocument>, IContractorDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public ContractorDocumentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContractorDocument>> GetDocumentsByContractorIdAsync(int contractorId)
        {
            return await _context.ContractorDocuments
                .Where(d => d.ContractorId == contractorId && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<ContractorDocument>> GetExpiringDocumentsAsync()
        {
            var today = DateTime.Today;
            var thirtyDaysFromNow = today.AddDays(30);

            return await _context.ContractorDocuments
                .Include(d => d.Contractor)
                .Where(d => !d.IsDeleted &&
                           !d.Contractor.IsDeleted &&
                           (d.Status == DocumentStatus.ExpiringSoon || d.Status == DocumentStatus.Expired))
                .OrderBy(d => d.ExpiryDate)
                .ToListAsync();
        }

        public async Task<bool> HasActiveDocumentsByContractorAsync(int contractorId)
        {
            return await _context.ContractorDocuments
                .AnyAsync(d => d.ContractorId == contractorId && !d.IsDeleted);
        }
    }
}