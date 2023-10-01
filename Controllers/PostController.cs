using AnySocialNetwork.Requests;
using AnySocialNetwork.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnySocialNetwork.Controllers
{
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("getAllByUser")]
        [Authorize]
        public async Task<ActionResult> GetAllByUserAsync()
        {
            var result = await _postService.GetAllAsync();
            if (!result.Any()) return NoContent();
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<dynamic>> CreateAsync([FromBody]CreatePostRequest createPostRequest)
        {
            var result = await _postService.CreateAsync(createPostRequest);
            return result;
        }
    }
}