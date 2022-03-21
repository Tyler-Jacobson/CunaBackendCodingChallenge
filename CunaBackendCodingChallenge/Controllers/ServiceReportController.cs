using CunaBackendCodingChallenge.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        
    }
}
