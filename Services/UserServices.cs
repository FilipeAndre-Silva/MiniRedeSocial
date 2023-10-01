using AnySocialNetwork.Data;
using AnySocialNetwork.Models;
using AnySocialNetwork.Models.Constants;
using AnySocialNetwork.Requests;
using AnySocialNetwork.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnySocialNetwork.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserDbContext _anySocialNetworkDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager, UserDbContext anySocialNetworkDbContext,
        IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _anySocialNetworkDbContext = anySocialNetworkDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> CreateAsync(CreateUserRequest createUserRequest)
        {
            var newUser = new User
            {
                UserName = createUserRequest.UserName,
                Email = createUserRequest.Email,
            };
        
            var result = await _userManager.CreateAsync(newUser, createUserRequest.Password);

            if (result.Succeeded)
            {
                var role = createUserRequest.Email.Contains("admin")? UserTypes.Administrator: UserTypes.CommonUser;
                await _userManager.AddToRoleAsync(newUser, role);
                return result;
            }

            return result;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _anySocialNetworkDbContext.Users.ToListAsync();
        }

        public async Task<Object> Authenticate(LoginUserRequest loginUserRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginUserRequest.Email);

            if (user == null) return null;

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginUserRequest.Password, false);

            if (!signInResult.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = TokenService.GenerateToken(user, roles.First());

            return new
            {
                AccessToken = token,
            };
        }

        public async Task<User> GetLoggedUser()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");;
            return await _userManager.FindByEmailAsync(userEmail.Value);
        }
    }
}