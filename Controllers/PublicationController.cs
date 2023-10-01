using AnySocialNetwork.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnySocialNetwork.Controllers
{
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;
        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpGet]
        [Route("getAll")]
        [Authorize]
        public async Task<ActionResult> GetAllAsync()
        {
            var result = await _publicationService.GetAllAsync();
            if (!result.Any()) return NoContent();
            return Ok(result);
        }
    }
}