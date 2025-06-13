using BackendExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        IPostsService _titleService;

        public PostsController(IPostsService titleService)
        {
            _titleService = titleService;
        }

        [HttpGet]
        public async Task<IEnumerable<DTOs.PostDto>> Get() =>
            await _titleService.Get();


    }
}
