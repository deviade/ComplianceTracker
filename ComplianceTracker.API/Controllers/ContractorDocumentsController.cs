using ComplianceTracker.Domain.DTOs;
using ComplianceTracker.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceTracker.API.Controllers
{
    [Route("api/contractors/{contractorId}/[controller]")]
    [ApiController]
    public class ContractorDocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<ContractorDocumentsController> _logger;

        public ContractorDocumentsController(
            IDocumentService documentService,
            ILogger<ContractorDocumentsController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        // GET: api/contractors/5/documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractorDocumentDto>>> GetContractorDocuments(int contractorId)
        {
            try
            {
                var documents = await _documentService.GetDocumentsByContractorAsync(contractorId);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting documents for contractor {ContractorId}", contractorId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST: api/contractors/5/documents
        [HttpPost]
        public async Task<ActionResult<ContractorDocumentDto>> PostContractorDocument(
            int contractorId,
            CreateDocumentDto createDocumentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var document = await _documentService.CreateDocumentAsync(contractorId, createDocumentDto);
                return CreatedAtAction(
                    nameof(GetContractorDocuments),
                    new { contractorId = contractorId },
                    document);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating document for contractor {ContractorId}", contractorId);
                return StatusCode(500, "An error occurred while creating the document.");
            }
        }
    }
}
