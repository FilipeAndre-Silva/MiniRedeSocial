using AnySocialNetwork.Data;
using AnySocialNetwork.Models;
using AnySocialNetwork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnySocialNetwork.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly AnySocialNetworkDbContext _anySocialNetworkDbContext;
        public PublicationService(AnySocialNetworkDbContext anySocialNetworkDbContext)
        {
            _anySocialNetworkDbContext = anySocialNetworkDbContext;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _anySocialNetworkDbContext.Posts.OrderByDescending(p => p.CreationDate)
                                                         .ToListAsync();
        }
    }
}