using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL.Models;

namespace QLDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private FeedbackSvc feedbackSvc;
        public FeedbackController()
        {
            feedbackSvc = new FeedbackSvc();
        }

        // POST: api/Feedback/create-feedback
        [HttpPost("create-feedback")]
        public IActionResult CreateFeedback([FromBody] FeedbackReq feedbackReq)
        {
            var res = feedbackSvc.CreateFeedback(feedbackReq);
            return Ok(res);
        }

        // POST: api/Feedback/search-feedback
        [HttpPost("search-feedback")]
        public IActionResult SearchFeedback(
            [FromQuery] int? memberId,
            [FromQuery] string? targetType,
            [FromQuery] int? rating,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var res = feedbackSvc.SearchFeedback(memberId, targetType, rating, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/Feedback/{id}
        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(int id)
        {
            var res = feedbackSvc.Read(id);
            return Ok(res);
        }

        // GET: api/Feedback
        [HttpGet]
        public IActionResult GetAllFeedbacks()
        {
            var res = feedbackSvc.GetFeedbacks();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/Feedback/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateFeedback(int id, [FromBody] FeedbackReq feedbackReq)
        {
            if (feedbackReq == null)
            {
                return BadRequest("Invalid feedback data.");
            }

            feedbackReq.FeedbackId = id;

            var res = feedbackSvc.UpdateFeedback(feedbackReq);
            return Ok(res);
        }

        // DELETE: api/Feedback/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(int id)
        {
            var res = feedbackSvc.DeleteFeedback(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "Feedback deleted successfully",
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