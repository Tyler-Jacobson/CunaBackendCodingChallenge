﻿namespace CunaBackendCodingChallenge
{
    public class CreateServiceReportDto
    {
        public CreateServiceReportDto(string body)
        {
            this.Body = body;
        }
        public string Body { get; set; } = string.Empty;

    }
}
