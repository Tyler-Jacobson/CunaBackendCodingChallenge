using CunaBackendCodingChallenge.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<ServiceReport>> CreateReport(int id, ServiceReportDto serviceReport)
        {
            var clientRequest = await _context.ClientRequests
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (clientRequest == null)
                return NotFound($"Client Request with id {id} not found");

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
        public async Task<ActionResult<ServiceReport>> UpdateReport(int id, ServiceReportDto serviceReport)
        {
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

            dbServiceReport.Status = serviceReport.Status;
            dbServiceReport.Detail = serviceReport.Detail;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
