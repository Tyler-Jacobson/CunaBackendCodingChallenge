namespace CunaBackendCodingChallenge.Models.DTOs
{
    public class UpdateServiceReportDto
    {
        public UpdateServiceReportDto(string status, string detail)
        {
            this.Status = status;
            this.Detail = detail;
        }
        public string? Status { get; set; } = string.Empty;
        public string? Detail { get; set; } = string.Empty;
    }
}
