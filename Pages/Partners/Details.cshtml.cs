using InsurancePartners.Models;
using InsurancePartners.Repositories;
using InsurancePartners.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InsurancePartners.Pages.Partners
{
    public class DetailsModel : PageModel
    {
        private readonly IPartnerRepository _partnerRepository;

        public DetailsModel(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public PartnerDetailsViewModel? PartnerDetails { get; set; }

        [BindProperty]
        public PolicyCreateViewModel Policy { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PartnerDetails = await _partnerRepository.GetByIdAsync(id);

            if (PartnerDetails == null)
            {
                return NotFound();
            }

            Policy.PartnerId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            PartnerDetails = await _partnerRepository.GetByIdAsync(id);

            if (PartnerDetails == null)
            {
                return NotFound();
            }

            Policy.PartnerId = id;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var policy = new Policy
            {
                PartnerId = id,
                PolicyNumber = Policy.PolicyNumber,
                PolicyAmount = Policy.PolicyAmount
            };

            await _partnerRepository.AddPolicyAsync(policy);

            return RedirectToPage("/Index");
        }
    }
}