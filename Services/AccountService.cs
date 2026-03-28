
using Microsoft.EntityFrameworkCore;
using VolunteerHub.Database;
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
    }
}
