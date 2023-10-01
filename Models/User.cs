using Microsoft.AspNetCore.Identity;

namespace AnySocialNetwork.Models
{
    public class User : IdentityUser
    {
        public List<Post> Posts { get; set; }
    }
}