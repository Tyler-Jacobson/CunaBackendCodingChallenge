using CunaBackendCodingChallenge.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CunaBackendCodingChallenge.Models.DTOs;
using CunaBackendCodingChallenge.Models;

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


        [HttpGet("{id}")]
        public async Task<ActionResult<ClientRequest>> Get(int id)
        {
            var clientRequest = await _context.ClientRequests
                .Where(clientReq => clientReq.Id == id)
                .Include(clientReq => clientReq.ServiceReport)
                .FirstOrDefaultAsync();

            if (clientRequest == null)
                return NotFound();

            return Ok(clientRequest);
        }

        [HttpPost]
        public async Task<ActionResult<ClientRequest>> CreateRequest(ClientRequestDto request)
        {
            var newClientRequest = new ClientRequest
            {
                Body = request.Body,
                CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow
            };

            if (newClientRequest.Body == string.Empty)
                return BadRequest("Requests must contain a valid 'body'");


            _context.ClientRequests.Add(newClientRequest);
            await _context.SaveChangesAsync();

            var headers = new Dictionary<string, string>
            {
                { "body", newClientRequest.Body },
                { "callback", $"/callback/{newClientRequest.Id}" }
            };

            MockAPI.Stub stub = new MockAPI.Stub();

            
            var response = await stub.MockPostAsync("http://example.com/request", headers);
            // would make api call here to 3rd party service
            // some sort of backup will need to be in place to make a future follow-up request in case the service is currently down

            // returns a url that can be used by the client to check the status of their request
            var returnValues = new Dictionary<string, string>
            {
                { "url", $"{this.Request.Scheme}://{this.Request.Host}/api/ClientRequest/{newClientRequest.Id}" },
                { "id", $"{newClientRequest.Id}" }
            };
            return Ok(returnValues);
        }
    }
}
