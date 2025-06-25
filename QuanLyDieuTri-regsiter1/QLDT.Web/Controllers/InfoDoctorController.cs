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
    public class InfoDoctorController : ControllerBase
    {
        private InfoDoctorSvc infoDoctorSvc;
        public InfoDoctorController()
        {
            infoDoctorSvc = new InfoDoctorSvc();
        }

        // POST: api/InfoDoctor/create-infoDoctor
        [HttpPost("create-infoDoctor")]
        public IActionResult CreateInfoDoctor([FromBody] InfoDoctorReq infoDoctorReq)
        {
            var res = infoDoctorSvc.CreateInfoDoctor(infoDoctorReq);
            return Ok(res);
        }

        // POST: api/InfoDoctor/search-infoDoctor
        [HttpPost("search-infoDoctor")]
        public IActionResult SearchInfoDoctor(
            [FromQuery] string? fullName,
            [FromQuery] string? speciality,
            [FromQuery] int? experianYear,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var res = infoDoctorSvc.SearchInfoDoctor(fullName, speciality, experianYear, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/InfoDoctor/{id}
        [HttpGet("{id}")]
        public IActionResult GetInfoDoctorById(int id)
        {
            var res = infoDoctorSvc.Read(id);
            return Ok(res);
        }

        // GET: api/InfoDoctor
        [HttpGet]
        public IActionResult GetAllInfoDoctors()
        {
            var res = infoDoctorSvc.GetInfoDoctors();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/InfoDoctor/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateInfoDoctor(int id, [FromBody] InfoDoctorReq infoDoctorReq)
        {
            if (infoDoctorReq == null)
            {
                return BadRequest("Invalid infoDoctor data.");
            }

            infoDoctorReq.InfoId = id;

            var res = infoDoctorSvc.UpdateInfoDoctor(infoDoctorReq);
            return Ok(res);
        }

        // DELETE: api/InfoDoctor/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteInfoDoctor(int id)
        {
            var res = infoDoctorSvc.DeleteInfoDoctor(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "InfoDoctor deleted successfully",
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