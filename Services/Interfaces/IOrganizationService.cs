using VolunteerHub.Models;
using VolunteerHub.Results;

namespace VolunteerHub.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<ServiceResult> CreateOrganizationWithoutSaving(Organization organization, string organizationAdminId);
    }
}
