namespace CunaBackendCodingChallenge
{
    public class ServiceReport
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public string? Detail { get; set; } = string.Empty;
        public ClientRequest ClientRequest { get; set; }
        public int ClientRequestId { get; set; }
    }
}
