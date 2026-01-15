using ComplianceTracker.Domain.DTOs;
using ComplianceTracker.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentsController> _logger;

        public DocumentsController(
            IDocumentService documentService,
            ILogger<DocumentsController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        // GET: api/documents/expiring
        [HttpGet("expiring")]
        public async Task<ActionResult<IEnumerable<ExpiringDocumentDto>>> GetExpiringDocuments()
        {
            try
            {
                var documents = await _documentService.GetExpiringDocumentsAsync();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting expiring documents");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
