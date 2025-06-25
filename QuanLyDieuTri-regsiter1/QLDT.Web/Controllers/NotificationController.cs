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
    public class NotificationController : ControllerBase
    {
        private NotificationSvc notificationSvc;
        public NotificationController()
        {
            notificationSvc = new NotificationSvc();
        }

        // POST: api/Notification/create-notification
        [HttpPost("create-notification")]
        public IActionResult CreateNotification([FromBody] NotificationReq notificationReq)
        {
            var res = notificationSvc.CreateNotification(notificationReq);
            return Ok(res);
        }

        // POST: api/Notification/search-notification
        [HttpPost("search-notification")]
        public IActionResult SearchNotification(
            [FromQuery] int? userId,
            [FromQuery] string? keyword,
            [FromQuery] bool? isRead,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var res = notificationSvc.SearchNotification(userId, keyword, isRead, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/Notification/{id}
        [HttpGet("{id}")]
        public IActionResult GetNotificationById(int id)
        {
            var res = notificationSvc.Read(id);
            return Ok(res);
        }

        // GET: api/Notification
        [HttpGet]
        public IActionResult GetAllNotifications([FromQuery] bool? isRead)
        {
            var res = notificationSvc.GetNotifications(isRead);
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/Notification/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateNotification(int id, [FromBody] NotificationReq notificationReq)
        {
            if (notificationReq == null)
            {
                return BadRequest("Invalid notification data.");
            }

            notificationReq.NotificationId = id;

            var res = notificationSvc.UpdateNotification(notificationReq);
            return Ok(res);
        }

        // DELETE: api/Notification/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var res = notificationSvc.DeleteNotification(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "Notification deleted successfully",
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