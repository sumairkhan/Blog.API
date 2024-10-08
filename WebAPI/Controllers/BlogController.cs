using Application.Features.Blog.Queries.GetBlogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetBlogsResponse>> GetBlogs()
        {
            var response = await Mediator.Send(new GetBlogsQuery());
            return response;
        }
    }
}
