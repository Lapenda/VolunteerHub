using VolunteerHub.Services;
using VolunteerHub.Services.Interfaces;

namespace VolunteerHub.Installers
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<VolunteerHubBaseService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            return services;
        }
    }
}
