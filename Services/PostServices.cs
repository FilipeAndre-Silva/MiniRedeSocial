using AnySocialNetwork.Data;
using AnySocialNetwork.Models;
using AnySocialNetwork.Requests;
using AnySocialNetwork.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnySocialNetwork.Services
{
    public class PostServices : IPostService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AnySocialNetworkDbContext _anySocialNetworkDbContext;
        private readonly IUserServices _userService;

        public PostServices(UserManager<User> userManager, SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager, AnySocialNetworkDbContext anySocialNetworkDbContext,
        IUserServices userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _anySocialNetworkDbContext = anySocialNetworkDbContext;
            _userService = userService;
        }

        public async Task<List<Post>> GetAllAsync()
        {
           var user = _userService.GetLoggedUser().Result;
            var result = await _anySocialNetworkDbContext.Posts.Where(p => p.UserId == user.Id).ToListAsync();
            return result;
        }

        public async Task<Post> CreateAsync(CreatePostRequest createPostRequest)
        {
            var user = _userService.GetLoggedUser().Result;
            Post newPost = new Post(createPostRequest.Content, user.Id);

            await _anySocialNetworkDbContext.Posts.AddAsync(newPost);
            await _anySocialNetworkDbContext.SaveChangesAsync();

            return newPost;
        }
    }
}