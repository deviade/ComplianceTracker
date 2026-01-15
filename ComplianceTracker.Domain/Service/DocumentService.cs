using ComplianceTracker.Domain.DTOs;
using ComplianceTracker.Domain.Entities;
using ComplianceTracker.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ComplianceTracker.Domain.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IContractorDocumentRepository _documentRepository;
        private readonly IContractorRepository _contractorRepository;
        private readonly DocumentStatusService _statusService;
        private readonly ILogger<DocumentService> _logger;


        public DocumentService(
            IContractorDocumentRepository documentRepository,
            IContractorRepository contractorRepository,
            DocumentStatusService statusService,
            ILogger<DocumentService> logger)
        {
            _documentRepository = documentRepository;
            _contractorRepository = contractorRepository;
            _statusService = statusService;
            _logger = logger;
        }

        public async Task<IEnumerable<ContractorDocumentDto>> GetDocumentsByContractorAsync(int contractorId)
        {
            try
            {
                var documents = await _documentRepository.GetDocumentsByContractorIdAsync(contractorId);
                return documents.Select(d => new ContractorDocumentDto
                {
                    Id = d.Id,
                    DocumentTypeId = d.DocumentTypeId,
                    UploadedOn = d.UploadedOn,
                    ExpiryDate = d.ExpiryDate,
                    Status = d.Status
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting documents for contractor {ContractorId}", contractorId);
                throw;
            }
        }

        public async Task<ContractorDocumentDto> CreateDocumentAsync(int contractorId, CreateDocumentDto dto)
        {
            try
            {
                // Validate contractor exists
                var contractorExists = await _contractorRepository.ExistsAsync(contractorId);
                if (!contractorExists)
                    throw new KeyNotFoundException($"Contractor with ID {contractorId} not found");

                var document = new ContractorDocument
                {
                    ContractorId = contractorId,
                    DocumentTypeId = dto.DocumentTypeId,
                    UploadedOn = dto.UploadedOn ?? DateTime.UtcNow,
                    ExpiryDate = dto.ExpiryDate
                };

                // Calculate status based on business rules
                document.Status = _statusService.CalculateStatus(document.ExpiryDate);

                var createdDocument = await _documentRepository.AddAsync(document);

                return new ContractorDocumentDto
                {
                    Id = createdDocument.Id,
                    DocumentTypeId = createdDocument.DocumentTypeId,
                    UploadedOn = createdDocument.UploadedOn,
                    ExpiryDate = createdDocument.ExpiryDate,
                    Status = createdDocument.Status
                };
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating document for contractor {ContractorId}", contractorId);
                throw;
            }
        }

        public async Task<IEnumerable<ExpiringDocumentDto>> GetExpiringDocumentsAsync()
        {
            try
            {
                var documents = await _documentRepository.GetExpiringDocumentsAsync();

                return documents.Select(d => new ExpiringDocumentDto
                {
                    Id = d.Id,
                    ContractorId = d.ContractorId,
                    ContractorName = d.Contractor?.Name ?? "Unknown",
                    DocumentTypeId = d.DocumentTypeId,
                    UploadedOn = d.UploadedOn,
                    ExpiryDate = d.ExpiryDate,
                    Status = d.Status,
                    DaysUntilExpiry = (d.ExpiryDate - DateTime.Today).Days
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting expiring documents");
                throw;
            }
        }
    }
}
