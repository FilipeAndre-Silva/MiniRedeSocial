using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnySocialNetwork.Models;
using AnySocialNetwork.Requests;

namespace AnySocialNetwork.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllAsync();
        Task<Post> CreateAsync(CreatePostRequest createPostRequest);
    }
}