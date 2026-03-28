using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VolunteerHub.Controllers.Base;
using VolunteerHub.RequestModels;
using VolunteerHub.ResponseModels;
using VolunteerHub.Results;
using VolunteerHub.Services;
using VolunteerHub.Services.Interfaces;

namespace VolunteerHub.Controllers
{
    public class AccountsController(IAccountService accountService) : VolunteerBaseController
    {
        [Authorize]
        [HttpGet]
        public ActionResult<String> Get()
        {
            return "Hi from accounts controller";
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ServiceResult<LoginResponseModel>> Login([FromBody] LoginRequestModel loginRequest)
        {
            return await accountService.Login(loginRequest.Username, loginRequest.Password);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ServiceResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            return await accountService.Register(registerRequestModel);
        }
    }
}
