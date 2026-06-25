using System.ComponentModel.DataAnnotations;

namespace InsurancePartners.ViewModels
{
    public class PolicyCreateViewModel
    {
        [Required]
        public int PartnerId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string PolicyNumber { get; set; } = "";

        [Required]
        [Range(0.01, 999999999)]
        public decimal PolicyAmount { get; set; }
    }
}