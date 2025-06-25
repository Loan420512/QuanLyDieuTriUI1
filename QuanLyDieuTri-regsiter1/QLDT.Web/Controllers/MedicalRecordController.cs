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
    public class MedicalRecordController : ControllerBase
    {
        private MedicalRecordSvc medicalRecordSvc;
        public MedicalRecordController()
        {
            medicalRecordSvc = new MedicalRecordSvc();
        }

        // POST: api/MedicalRecord/create-medicalRecord
        [HttpPost("create-medicalRecord")]
        public IActionResult CreateMedicalRecord([FromBody] MedicalRecordReq medicalRecordReq)
        {
            var res = medicalRecordSvc.CreateMedicalRecord(medicalRecordReq);
            return Ok(res);
        }

        // POST: api/MedicalRecord/search-medicalRecord
        [HttpPost("search-medicalRecord")]
        public IActionResult SearchMedicalRecord(
            [FromQuery] int? memberId,
            [FromQuery] string? keyword,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var res = medicalRecordSvc.SearchMedicalRecord(memberId, keyword, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/MedicalRecord/{id}
        [HttpGet("{id}")]
        public IActionResult GetMedicalRecordById(int id)
        {
            var res = medicalRecordSvc.Read(id);
            return Ok(res);
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public IActionResult GetAllMedicalRecords()
        {
            var res = medicalRecordSvc.GetMedicalRecords();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/MedicalRecord/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMedicalRecord(int id, [FromBody] MedicalRecordReq medicalRecordReq)
        {
            if (medicalRecordReq == null)
            {
                return BadRequest("Invalid medicalRecord data.");
            }

            medicalRecordReq.RecordId = id;

            var res = medicalRecordSvc.UpdateMedicalRecord(medicalRecordReq);
            return Ok(res);
        }

        // DELETE: api/MedicalRecord/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicalRecord(int id)
        {
            var res = medicalRecordSvc.DeleteMedicalRecord(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "MedicalRecord deleted successfully",
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