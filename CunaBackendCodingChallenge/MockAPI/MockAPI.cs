using CunaBackendCodingChallenge.Data;
using CunaBackendCodingChallenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CunaBackendCodingChallenge.MockAPI
{
    public class Stub : ControllerBase
    {
        public async Task<ActionResult<ServiceReport>> MockPostAsync(string url, Dictionary<string, string> headers)
        {
            // this third party service would make a post request back to
            // https://cunabackend.com/api/ServiceReport/callback/id whenever it is ready
            return Ok();
        }
    }
}
