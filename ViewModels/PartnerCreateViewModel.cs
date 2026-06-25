using System.ComponentModel.DataAnnotations;

namespace InsurancePartners.ViewModels
{
    public class PartnerCreateViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [RegularExpression(@"^[a-zA-ZčćžšđČĆŽŠĐ0-9 ]+$")]
        public string FirstName { get; set; } = "";

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [RegularExpression(@"^[a-zA-ZčćžšđČĆŽŠĐ0-9 ]+$")]
        public string LastName { get; set; } = "";

        [RegularExpression(@"^[a-zA-ZčćžšđČĆŽŠĐ0-9 ,.-]*$")]
        public string? Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "Partner number must contain exactly 20 digits.")]
        public string PartnerNumber { get; set; } = "";

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Croatian PIN must contain 11 digits.")]
        public string? CroatianPIN { get; set; }

        [Required]
        [Range(1, 2)]
        public int PartnerTypeId { get; set; }

        public bool IsForeign { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-ZčćžšđČĆŽŠĐ0-9]+$")]
        public string ExternalCode { get; set; } = "";

        [Required]
        [RegularExpression(@"^(M|F|N)$")]
        public string Gender { get; set; } = "";
    }
}