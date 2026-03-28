using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VolunteerHub.Database;
using VolunteerHub.Models;
using VolunteerHub.Results;

namespace VolunteerHub.Services
{
    public class VolunteerHubBaseService(IHttpContextAccessor httpContextAccessor, DBM context, ILogger<VolunteerHubBaseService> logger)
    {
        protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        protected readonly DBM _context = context;
        
        public Account? Me { get; private set; }
        protected string? CurrentUserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<ServiceResult<Account>> GetMeAsync()
        {
            if (Me != null)
                return ServiceResult<Account>.CreateSuccess(Me);

            var accountId = CurrentUserId;

            if (string.IsNullOrWhiteSpace(accountId))
                return ServiceResult<Account>.CreateError("Not authorized");

            var account = await _context
                .Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == accountId && !a.Deleted);
            
            if (account == null)
                return ServiceResult<Account>.CreateError("Account not found");

            Me = account;

            return ServiceResult<Account>.CreateSuccess(account);
        }
    }
}
