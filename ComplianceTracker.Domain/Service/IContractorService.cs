using ComplianceTracker.Domain.DTOs;

namespace ComplianceTracker.Domain.Service
{
    public interface IContractorService
    {
        Task<IEnumerable<ContractorDto>> GetAllContractorsAsync();
        Task<ContractorDetailsDto?> GetContractorByIdAsync(int id);
        Task<ContractorDto> CreateContractorAsync(CreateContractorDto dto);
        Task UpdateContractorAsync(int id, UpdateContractorDto dto);
        Task DeleteContractorAsync(int id);
    }
}
