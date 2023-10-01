using AnySocialNetwork.Models;

namespace AnySocialNetwork.Services.Interfaces
{
    public interface IPublicationService
    {
        Task<List<Post>> GetAllAsync();
    }
}