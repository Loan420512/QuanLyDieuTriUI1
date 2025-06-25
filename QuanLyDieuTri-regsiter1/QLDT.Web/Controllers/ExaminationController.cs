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
    public class ExaminationController : ControllerBase
    {
        private ExaminationSvc examinationSvc;
        public ExaminationController()
        {
            examinationSvc = new ExaminationSvc();
        }

        // POST: api/Examination/create-examination
        [HttpPost("create-examination")]
        public IActionResult CreateExamination([FromBody] ExaminationReq examinationReq)
        {
            var res = examinationSvc.CreateExamination(examinationReq);
            return Ok(res);
        }

        // POST: api/Examination/search-examination
        [HttpPost("search-examination")]
        public IActionResult SearchExamination(
            [FromQuery] int? bookingId,
            [FromQuery] int? doctorUserId,
            [FromQuery] DateOnly? dateMeet,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var res = examinationSvc.SearchExamination(bookingId, doctorUserId, dateMeet, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/Examination/{id}
        [HttpGet("{id}")]
        public IActionResult GetExaminationById(int id)
        {
            var res = examinationSvc.Read(id);
            return Ok(res);
        }

        // GET: api/Examination
        [HttpGet]
        public IActionResult GetAllExaminations()
        {
            var res = examinationSvc.GetExaminations();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/Examination/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateExamination(int id, [FromBody] ExaminationReq examinationReq)
        {
            if (examinationReq == null)
            {
                return BadRequest("Invalid examination data.");
            }

            examinationReq.ExaminationId = id;

            var res = examinationSvc.UpdateExamination(examinationReq);
            return Ok(res);
        }

        // DELETE: api/Examination/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteExamination(int id)
        {
            var res = examinationSvc.DeleteExamination(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "Examination deleted successfully",
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