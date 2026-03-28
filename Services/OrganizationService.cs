using Microsoft.EntityFrameworkCore;
using System.Net;
using VolunteerHub.Database;
using VolunteerHub.Models;
using VolunteerHub.Results;
using VolunteerHub.Services.Interfaces;

namespace VolunteerHub.Services
{
    public class OrganizationService(IHttpContextAccessor httpContextAccessor, DBM context, ILogger<OrganizationService> logger) : VolunteerHubBaseService(httpContextAccessor, context, logger), IOrganizationService
    {
        public async Task<ServiceResult> CreateOrganizationWithoutSaving(Organization organization, string organizationOwnerId)
        {
            if (await IsOrganizationEmailTaken(organization.Email))
            {
                logger.LogDebug("Organization email is taken");
                return ServiceResult.CreateError("Organization email is taken", HttpStatusCode.BadRequest);
            }

            try
            {
                organization.OrganizationOwnerId = organizationOwnerId;
                await _context.Organizations.AddAsync(organization);

                var orgMember = new OrganizationMember
                {
                    OrganizationId = organization.Id,
                    AccountId = organizationOwnerId,
                    OrganizationRole = OrganizationRole.Admin
                };
                await _context.OrganizationMembers.AddAsync(orgMember);

                return ServiceResult.CreateSuccess();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.ToString(), "Error creating organization, {Message}", ex.Message);
                return ServiceResult.CreateError("Error creating organization", HttpStatusCode.InternalServerError);
            }
        }

        private async Task<bool> IsOrganizationEmailTaken(string email)
        {
            return await _context.Organizations.AsNoTracking().AnyAsync(o => o.Email == email);
        }
    }
}
