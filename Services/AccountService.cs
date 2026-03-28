
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using VolunteerHub.Database;
using VolunteerHub.Models;
using VolunteerHub.RequestModels;
using VolunteerHub.ResponseModels;
using VolunteerHub.Results;
using VolunteerHub.Services.Interfaces;

namespace VolunteerHub.Services
{
    public class AccountService(IHttpContextAccessor httpContextAccessor, DBM context, ILogger<AccountService> logger, IOrganizationService organizationService) : VolunteerHubBaseService(httpContextAccessor, context, logger), IAccountService
    {
        public async Task<ServiceResult<LoginResponseModel>> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return ServiceResult<LoginResponseModel>.CreateError("Account not found", HttpStatusCode.NotFound);

            var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Username == username && !a.Deleted);

            if (account == null)
                return ServiceResult<LoginResponseModel>.CreateError("Account not found", HttpStatusCode.NotFound);

            if (!VerifyPassword(password, account.Password))
                return new ServiceResult<LoginResponseModel>(null, false, HttpStatusCode.Unauthorized, "Wrong credentials");

            var token = GenerateToken(account);

            return ServiceResult<LoginResponseModel>.CreateSuccess(new LoginResponseModel()
            {
                Token = token,
                Username = account.Username
            });
        }

        public async Task<ServiceResult> Register(RegisterRequestModel registerRequestModel)
        {
            if (await IsUsernameOrEmailTaken(registerRequestModel.Username, registerRequestModel.Email))
            {
                return ServiceResult.CreateError("Username is already taken", HttpStatusCode.BadRequest);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var password = AddPepperToPassword(registerRequestModel.Password);
                var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                var account = new Account
                {
                    Username = registerRequestModel.Username,
                    FirstName = registerRequestModel.FirstName,
                    LastName = registerRequestModel.LastName,
                    Email = registerRequestModel.Email,
                    Password = hashedPassword,
                    GlobalRole = GlobalRole.User
                };

                await _context.Accounts.AddAsync(account);

                if (registerRequestModel.Organization != null)
                {
                    var orgResult = await organizationService.CreateOrganizationWithoutSaving(registerRequestModel.Organization, account.Id);
                    if (!orgResult.IsSuccess)
                        return orgResult;
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return ServiceResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError($"An error occured while creating your account { ex.ToString() }");
                return ServiceResult.CreateError("An error occured while creating your account", HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        private string GenerateToken(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim("GlobalRole", account.GlobalRole.ToString())
            };

            var secretKey = Environment.GetEnvironmentVariable("JWT_KEY");

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("Error getting jwt key");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims: claims,
                expires: DateTime.UtcNow.AddYears(100),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<bool> IsUsernameOrEmailTaken(string username, string email)
        {
            return await _context.Accounts.AsNoTracking().AnyAsync(a => a.Username == username || a.Email == email);
        }

        private string GetPepper()
        {
            return Environment.GetEnvironmentVariable("PEPPER") ?? "some_pepper";
        }
        private string AddPepperToPassword(string password)
        {
            return password + GetPepper();
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {   
            if (BCrypt.Net.BCrypt.Verify(password + GetPepper(), hashedPassword))
                return true;
            
            return false;
        }
    }
}
