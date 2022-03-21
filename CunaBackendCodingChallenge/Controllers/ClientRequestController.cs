using CunaBackendCodingChallenge.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CunaBackendCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientRequestController : ControllerBase
    {
        private readonly DataContext _context;
        public ClientRequestController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<ClientRequest>> Get(int requestId)
        {
            var clientRequest = await _context.ClientRequests
                .Where(c => c.Id == requestId)
                .Include(c => c.ServiceReport)
                .FirstOrDefaultAsync();

            if (clientRequest == null)
                return NotFound();

            return Ok(clientRequest);
        }
    }
}
