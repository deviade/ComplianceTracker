// Services/ContractorService.cs
using ContractorManagement.Components.Model;
using static ContractorManagement.Components.Model.ContractorDetails;

namespace ContractorManagement.Blazor.Services
{
    public interface IContractorService
    {
        Task<List<Contractor>> GetContractorsAsync();
        Task<ContractorDetails?> GetContractorByIdAsync(int id);
        Task<Contractor> CreateContractorAsync(CreateContractorDto dto);
        Task UpdateContractorAsync(UpdateContractorDto dto);
        Task DeleteContractorAsync(int id);
    }

    public class ContractorService : IContractorService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ContractorService> _logger;

        public ContractorService(HttpClient httpClient, ILogger<ContractorService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Contractor>> GetContractorsAsync()
        {
            var response = await _httpClient.GetAsync("api/contractors");

            if (!response.IsSuccessStatusCode)
                throw await CreateApiException(response);

            return await response.Content.ReadFromJsonAsync<List<Contractor>>()
                   ?? new List<Contractor>();
        }

        public async Task<ContractorDetails?> GetContractorByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/contractors/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                throw await CreateApiException(response);

            return await response.Content.ReadFromJsonAsync<ContractorDetails>();
        }

        public async Task<Contractor> CreateContractorAsync(CreateContractorDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/contractors", dto);

            if (!response.IsSuccessStatusCode)
                throw await CreateApiException(response);

            return (await response.Content.ReadFromJsonAsync<Contractor>())!;
        }

        public async Task UpdateContractorAsync(UpdateContractorDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/contractors/{dto.Id}", dto);

            if (!response.IsSuccessStatusCode)
                throw await CreateApiException(response);
        }

        public async Task DeleteContractorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/contractors/{id}");

            if (!response.IsSuccessStatusCode)
                throw await CreateApiException(response);
        }

        private async Task<Exception> CreateApiException(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            _logger.LogError("API Error {Status}: {Content}",
                response.StatusCode, content);

            return new ApplicationException(
                $"API error ({(int)response.StatusCode}): {content}");
        }
    }

}