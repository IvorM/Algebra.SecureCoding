namespace Algebra.SecureCoding.Web.Models.CryptographicFailures
{
    public record CardInformationDto
    {
        public string CardNumber { get; set; } = "";
        public string CardholdersName { get; set; } = "";
        public string ExpirationDate { get; set; } = "";
        public string Ccv { get; set; } = "";
    }
}
