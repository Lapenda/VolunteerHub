
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VolunteerHub.Database;
using VolunteerHub.Models;
using VolunteerHub.RequestModels;
using VolunteerHub.ResponseModels;
using VolunteerHub.Results;
using VolunteerHub.Services.Interfaces;

namespace VolunteerHub.Services
{
    public class AccountService(DBM context) : IAccountService
    {  
        public async Task<ServiceResult<LoginResponseModel>> Login(string username, string password)
        {
            return ServiceResult<LoginResponseModel>.CreateSuccess(new LoginResponseModel() 
            {
                Token = "hi from login response"
            });
        }

        public Task<ServiceResult> Register(RegisterRequestModel registerRequestModel)
        {
            throw new NotImplementedException();
        }

        private string GenerateToken(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim("GlobalRole", account.GlobalRole.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            return "";
        }
    }
}
