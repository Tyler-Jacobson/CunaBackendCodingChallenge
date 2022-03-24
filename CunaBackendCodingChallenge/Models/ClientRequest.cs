namespace CunaBackendCodingChallenge.Models
{
    public class ClientRequest
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
        public ServiceReport? ServiceReport { get; set; }
    }
}
