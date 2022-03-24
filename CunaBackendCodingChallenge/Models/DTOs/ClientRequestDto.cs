namespace CunaBackendCodingChallenge.Models.DTOs
{
    
    public class ClientRequestDto
    {
        public ClientRequestDto(string body)
        {
            this.Body = body;
        }
        public string Body { get; set; } = string.Empty;
    }
}
