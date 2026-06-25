using InsurancePartners.Models;
using InsurancePartners.Repositories;
using InsurancePartners.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InsurancePartners.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPartnerRepository _partnerRepository;

        public IndexModel(ILogger<IndexModel> logger, IPartnerRepository partnerRepository)
        {
            _logger = logger;
            _partnerRepository = partnerRepository;
        }

        public List<PartnerListViewModel> Partners { get; set; } = new();

        public int? CreatedPartnerId { get; set; }

        public async Task OnGetAsync(int? createdPartnerId)
        {
            Partners = await _partnerRepository.GetAllAsync();
            CreatedPartnerId = createdPartnerId;
        }

        public async Task<IActionResult> OnGetPartnerDetailsAsync(int id)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);

            if (partner == null)
            {
                return NotFound();
            }

            return new JsonResult(partner);
        }

        public async Task<IActionResult> OnPostAddPolicyAsync([FromBody] PolicyCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var policy = new Policy
            {
                PartnerId = model.PartnerId,
                PolicyNumber = model.PolicyNumber,
                PolicyAmount = model.PolicyAmount
            };

            await _partnerRepository.AddPolicyAsync(policy);

            return new JsonResult(new { success = true });
        }
    }
}