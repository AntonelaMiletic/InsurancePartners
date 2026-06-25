using InsurancePartners.Models;
using InsurancePartners.Repositories;
using InsurancePartners.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InsurancePartners.Pages.Partners
{
    public class CreateModel : PageModel
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IConfiguration _configuration;

        public CreateModel(IPartnerRepository partnerRepository, IConfiguration configuration)
        {
            _partnerRepository = partnerRepository;
            _configuration = configuration;
        }

        [BindProperty]
        public PartnerCreateViewModel Partner { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var partner = new Partner
            {
                FirstName = Partner.FirstName,
                LastName = Partner.LastName,
                Address = Partner.Address,
                PartnerNumber = Partner.PartnerNumber,
                CroatianPIN = Partner.CroatianPIN,
                PartnerTypeId = Partner.PartnerTypeId,
                CreatedByUser = _configuration["AppSettings:DefaultCreatedByUser"] ?? "system@insurance.hr",
                IsForeign = Partner.IsForeign,
                ExternalCode = Partner.ExternalCode,
                Gender = Partner.Gender
            };

            var newPartnerId = await _partnerRepository.CreateAsync(partner);

            return RedirectToPage("/Index", new { createdPartnerId = newPartnerId });
        }
    }
}