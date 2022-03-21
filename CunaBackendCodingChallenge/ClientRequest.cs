using System.Text.Json.Serialization;

namespace CunaBackendCodingChallenge
{
    public class ClientRequest
    {
        public int Id { get; set; }

        public string Body { get; set; }
        public ServiceReport? ServiceReport { get; set; }
    }
}
