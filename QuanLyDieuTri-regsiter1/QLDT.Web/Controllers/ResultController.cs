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
    public class ResultController : ControllerBase
    {
        private ResultSvc resultSvc;
        public ResultController()
        {
            resultSvc = new ResultSvc();
        }

        // POST: api/Result/create-result
        [HttpPost("create-result")]
        public IActionResult CreateResult([FromBody] ResultReq resultReq)
        {
            var res = resultSvc.CreateResult(resultReq);
            return Ok(res);
        }

        // POST: api/Result/search-result
        [HttpPost("search-result")]
        public IActionResult SearchResult(
            [FromQuery] int? examinationId,
            [FromQuery] string? keyword,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var res = resultSvc.SearchResult(examinationId, keyword, page, size);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Error in search.");
        }

        // GET: api/Result/{id}
        [HttpGet("{id}")]
        public IActionResult GetResultById(int id)
        {
            var res = resultSvc.Read(id);
            return Ok(res);
        }

        // GET: api/Result
        [HttpGet]
        public IActionResult GetAllResults()
        {
            var res = resultSvc.GetResults();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res);
        }

        // PUT: api/Result/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateResult(int id, [FromBody] ResultReq resultReq)
        {
            if (resultReq == null)
            {
                return BadRequest("Invalid result data.");
            }

            resultReq.ResultId = id;

            var res = resultSvc.UpdateResult(resultReq);
            return Ok(res);
        }

        // DELETE: api/Result/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteResult(int id)
        {
            var res = resultSvc.DeleteResult(id);
            if (res.Success)
            {
                return Ok(new
                {
                    Message = "Result deleted successfully",
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