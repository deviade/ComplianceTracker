using ComplianceTracker.Domain.DTOs;
using ComplianceTracker.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {
        private readonly IContractorService _contractorService;
        private readonly ILogger<ContractorsController> _logger;

        public ContractorsController(
            IContractorService contractorService,
            ILogger<ContractorsController> logger)
        {
            _contractorService = contractorService;
            _logger = logger;
        }

        // GET: api/contractors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractorDto>>> GetContractors()
        {
            try
            {
                var contractors = await _contractorService.GetAllContractorsAsync();
                return Ok(contractors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contractors");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/contractors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractorDetailsDto>> GetContractor(int id)
        {
            try
            {
                var contractor = await _contractorService.GetContractorByIdAsync(id);

                if (contractor == null)
                    return NotFound($"Contractor with ID {id} not found");

                return Ok(contractor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contractor with ID {Id}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST: api/contractors
        [HttpPost]
        public async Task<ActionResult<ContractorDto>> PostContractor(CreateContractorDto createContractorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var contractor = await _contractorService.CreateContractorAsync(createContractorDto);
                return CreatedAtAction(nameof(GetContractor), new { id = contractor.Id }, contractor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating contractor");
                return StatusCode(500, "An error occurred while creating the contractor.");
            }
        }

        // PUT: api/contractors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractor(int id, UpdateContractorDto updateContractorDto)
        {
            try
            {
                if (id != updateContractorDto.Id)
                    return BadRequest("ID mismatch");

                await _contractorService.UpdateContractorAsync(id, updateContractorDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Contractor with ID {id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating contractor with ID {Id}", id);
                return StatusCode(500, "An error occurred while updating the contractor.");
            }
        }

        // DELETE: api/contractors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractor(int id)
        {
            try
            {
                await _contractorService.DeleteContractorAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Contractor with ID {id} not found");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting contractor with ID {Id}", id);
                return StatusCode(500, "An error occurred while deleting the contractor.");
            }
        }
    }
}
