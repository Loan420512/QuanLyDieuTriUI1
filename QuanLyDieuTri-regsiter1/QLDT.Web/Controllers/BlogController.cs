using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL.Models; // Ensure this namespace is correct for your models

namespace QLDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private BlogSvc blogSvc;
        public BlogController()
        {
            blogSvc = new BlogSvc();
        }

        // POST: api/Blog/create-blog
        [HttpPost("create-blog")]
        public IActionResult CreateBlog([FromBody] BlogReq blogReq)
        {
            var res = blogSvc.CreateBlog(blogReq);
            return Ok(res);
        }

        // POST: api/Blog/search-blog
        [HttpPost("search-blog")]
        public IActionResult SearchBlog([FromQuery] string? keyword, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var res = blogSvc.SearchBlog(keyword, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/Blog/{id}
        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var res = blogSvc.Read(id);
            return Ok(res);
        }

        // GET: api/Blog
        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            var res = blogSvc.GetBlogs();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/Blog/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] BlogReq blogReq)
        {
            if (blogReq == null)
            {
                return BadRequest("Invalid blog data.");
            }

            blogReq.IdBlog = id; // Ensure the ID from the URL is used

            var res = blogSvc.UpdateBlog(blogReq);
            return Ok(res);
        }

        // DELETE: api/Blog/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var res = blogSvc.DeleteBlog(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "Blog deleted successfully",
                    Data = res.Data
                });
            }
            return BadRequest(new
            {
                ErrorCode = res.Code,
                Message = res.Message
            });
        }
    }
}