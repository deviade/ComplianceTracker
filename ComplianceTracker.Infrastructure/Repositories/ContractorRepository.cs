using Microsoft.EntityFrameworkCore;
using ComplianceTracker.Domain.Entities;
using ComplianceTracker.Domain.Interfaces;
using ComplianceTracker.Infrastructure.Data;

namespace ComplianceTracker.Infrastructure.Repositories
{
    public class ContractorRepository : GenericRepository<Contractor>, IContractorRepository
    {
        public ContractorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Contractor?> GetContractorWithDocumentsAsync(int id)
        {
            return await _context.Contractors
                .Include(c => c.Documents)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> HasDocumentsAsync(int id)
        {
            return await _context.ContractorDocuments
                .AnyAsync(d => d.ContractorId == id);
        }
    }
}