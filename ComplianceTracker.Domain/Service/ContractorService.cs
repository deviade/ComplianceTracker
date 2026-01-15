using ComplianceTracker.Domain.DTOs;
using ComplianceTracker.Domain.Entities;
using ComplianceTracker.Domain.Interfaces;
using ComplianceTracker.Domain.Service;
using Microsoft.Extensions.Logging;

namespace ComplianceTracker.Domain.Services
{
    public class ContractorService : IContractorService
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly ILogger<ContractorService> _logger;

        public ContractorService(
            IContractorRepository contractorRepository,
            ILogger<ContractorService> logger)
        {
            _contractorRepository = contractorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ContractorDto>> GetAllContractorsAsync()
        {
            try
            {
                var contractors = await _contractorRepository.GetAllAsync();
                return contractors.Select(c => new ContractorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all contractors");
                throw;
            }
        }

        public async Task<ContractorDetailsDto?> GetContractorByIdAsync(int id)
        {
            try
            {
                var contractor = await _contractorRepository.GetContractorWithDocumentsAsync(id);

                if (contractor == null)
                    return null;

                return new ContractorDetailsDto
                {
                    Id = contractor.Id,
                    Name = contractor.Name,
                    Email = contractor.Email,
                    Documents = contractor.Documents.Select(d => new ContractorDocumentDto
                    {
                        Id = d.Id,
                        DocumentTypeId = d.DocumentTypeId,
                        UploadedOn = d.UploadedOn,
                        ExpiryDate = d.ExpiryDate,
                        Status = d.Status
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contractor with ID {Id}", id);
                throw;
            }
        }

        public async Task<ContractorDto> CreateContractorAsync(CreateContractorDto dto)
        {
            try
            {
                var contractor = new Contractor
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    CreatedAt = DateTime.UtcNow
                };

                var createdContractor = await _contractorRepository.AddAsync(contractor);

                return new ContractorDto
                {
                    Id = createdContractor.Id,
                    Name = createdContractor.Name,
                    Email = createdContractor.Email
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating contractor");
                throw;
            }
        }

        public async Task UpdateContractorAsync(int id, UpdateContractorDto dto)
        {
            try
            {
                var contractor = await _contractorRepository.GetByIdAsync(id);
                if (contractor == null)
                    throw new KeyNotFoundException($"Contractor with ID {id} not found");

                contractor.Name = dto.Name;
                contractor.Email = dto.Email;
                contractor.UpdatedAt = DateTime.UtcNow;

                await _contractorRepository.UpdateAsync(contractor);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating contractor with ID {Id}", id);
                throw;
            }
        }

        public async Task DeleteContractorAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting contractor with ID {Id}", id);

                var contractor = await _contractorRepository.GetByIdAsync(id);
                if (contractor == null)
                    throw new KeyNotFoundException($"Contractor with ID {id} not found");

                // Soft delete - just set IsDeleted flag
                contractor.IsDeleted = true;

                await _contractorRepository.UpdateAsync(contractor);

                _logger.LogInformation("Soft deleted contractor with ID {Id}", id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting contractor with ID {Id}", id);
                throw new ApplicationException($"An error occurred while deleting contractor with ID {id}.", ex);
            }
        }
    }
}