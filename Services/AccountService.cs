
using VolunteerHub.ResponseModels;
using VolunteerHub.Results;
using VolunteerHub.Services.Interfaces;

namespace VolunteerHub.Services
{
    public class AccountService() : IAccountService
    {  
        public async Task<ServiceResult<LoginResponseModel>> Login(string username, string password)
        {
            return ServiceResult<LoginResponseModel>.CreateSuccess(new LoginResponseModel() 
            {
                Token = "hi from login response"
            });
        }

        public Task Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
