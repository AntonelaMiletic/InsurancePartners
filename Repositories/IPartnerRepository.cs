using InsurancePartners.Models;
using InsurancePartners.ViewModels;

namespace InsurancePartners.Repositories
{
    public interface IPartnerRepository
    {
        Task<List<PartnerListViewModel>> GetAllAsync();
        Task<PartnerDetailsViewModel?> GetByIdAsync(int id);
        Task<int> CreateAsync(Partner partner);
        Task AddPolicyAsync(Policy policy);
    }
}