using CunaBackendCodingChallenge.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CunaBackendCodingChallenge.MockAPI.Stub;

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
                .Where(c => c.Id == id)
                .Include(c => c.ServiceReport)
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
                Body = request.Body
            };

            _context.ClientRequests.Add(newClientRequest);
            await _context.SaveChangesAsync();

            var headers = new Dictionary<string, string>
            {
                { "body", newClientRequest.Body },
                { "callback", $"/callback/{newClientRequest.Id}" }
            };

            MockAPI.Stub stub = new MockAPI.Stub();

            // makes api call here to 3rd party service
            var response = await stub.MockPostAsync("http://example.com/request", headers);
            // some sort of backup will need to be in place to make a follow-up request in case the service is currently down

            // returns a url that can be used by the client to check the status of their request
            return Ok($"{this.Request.Scheme}://{this.Request.Host}/api/ClientRequest/{newClientRequest.Id}");
        }


    }
}
