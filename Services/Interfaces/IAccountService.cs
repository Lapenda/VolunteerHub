using VolunteerHub.ResponseModels;
using VolunteerHub.Results;

namespace VolunteerHub.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResult<LoginResponseModel>> Login(string username, string password);
        Task Register(string username, string password);
    }
}
