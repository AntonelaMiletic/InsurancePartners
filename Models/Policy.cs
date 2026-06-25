namespace InsurancePartners.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public string PolicyNumber { get; set; } = "";
        public decimal PolicyAmount { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}