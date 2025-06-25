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
    public class MemberController : ControllerBase
    {
        private MemberSvc memberSvc;
        public MemberController()
        {
            memberSvc = new MemberSvc();
        }

        // POST: api/Member/create-member
        [HttpPost("create-member")]
        public IActionResult CreateMember([FromBody] MemberReq memberReq)
        {
            var res = memberSvc.CreateMember(memberReq);
            return Ok(res);
        }

        // POST: api/Member/search-member
        [HttpPost("search-member")]
        public IActionResult SearchMember(
            [FromQuery] string? phoneNumber,
            [FromQuery] string? name,
            [FromQuery] string? gender,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var res = memberSvc.SearchMember(phoneNumber, name, gender, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/Member/{id}
        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            var res = memberSvc.Read(id);
            return Ok(res);
        }

        // GET: api/Member
        [HttpGet]
        public IActionResult GetAllMembers()
        {
            var res = memberSvc.GetMembers();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/Member/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, [FromBody] MemberReq memberReq)
        {
            if (memberReq == null)
            {
                return BadRequest("Invalid member data.");
            }

            memberReq.MemberId = id;

            var res = memberSvc.UpdateMember(memberReq);
            return Ok(res);
        }

        // DELETE: api/Member/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            var res = memberSvc.DeleteMember(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "Member deleted successfully",
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