using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;

namespace QLDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private BookingSvc bookingSvc;
        public BookingController() {
            bookingSvc = new BookingSvc();
        }

        [HttpPost("get-by-id")]
        public IActionResult GetBookingByID([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRes();
            res = bookingSvc.Read(simpleReq.Id);
            return Ok(res.Data);
        }
    }
}
