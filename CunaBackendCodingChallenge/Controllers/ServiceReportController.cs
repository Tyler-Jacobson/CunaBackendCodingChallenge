using CunaBackendCodingChallenge.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CunaBackendCodingChallenge.DTOs;

namespace CunaBackendCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceReportController : ControllerBase
    {
        private readonly DataContext _context;
        public ServiceReportController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("callback/{id}")]
        public async Task<ActionResult<ServiceReport>> CreateReport(int id, CreateServiceReportDto serviceReport)
        {
            if (serviceReport.Body != "STARTED")
                return BadRequest("Requests must have a body containing the text 'STARTED'");

            var clientRequest = await _context.ClientRequests
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (clientRequest == null)
                return NotFound($"Client Request with id {id} not found");

            clientRequest.ModifiedDateTime = DateTime.UtcNow;

            var report = new ServiceReport
            {
                Body = serviceReport.Body,
                ClientRequestId = id
            };

            _context.ServiceReports.Add(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("callback/{id}")]
        public async Task<ActionResult<ServiceReport>> UpdateReport(int id, UpdateServiceReportDto serviceReport)
        {
            if (serviceReport.Status != "PROCESSED" && (serviceReport.Status != "COMPLETED") && (serviceReport.Status != "ERROR"))
                return BadRequest("Requests must have 'status' containing one of: 'PROCESSED' | 'COMPLETED' | 'ERROR'");

            var clientRequest = await _context.ClientRequests
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (clientRequest == null)
                return NotFound($"Client Request with id {id} not found");

            var dbServiceReport = await _context.ServiceReports
                .Where(c => c.ClientRequestId == id)
                .FirstOrDefaultAsync();
            if (dbServiceReport == null)
                return NotFound($"Client Request with id {id} does not have an active Service Report");

            clientRequest.ModifiedDateTime = DateTime.UtcNow;

            dbServiceReport.Status = serviceReport.Status;
            dbServiceReport.Detail = serviceReport.Detail;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
