namespace InsurancePartners.ViewModels
{
    public class PartnerListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string PartnerNumber { get; set; } = "";
        public string? CroatianPIN { get; set; }
        public int PartnerTypeId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public bool IsForeign { get; set; }
        public string Gender { get; set; } = "";
        public bool IsImportant { get; set; }
    }
}