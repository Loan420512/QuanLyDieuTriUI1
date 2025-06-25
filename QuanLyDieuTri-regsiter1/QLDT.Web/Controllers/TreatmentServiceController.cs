using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;

namespace QLDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentServiceController : ControllerBase
    {
        private TreatmentServiceSvc treatmentServiceSvc;

        public TreatmentServiceController()
        {
            treatmentServiceSvc = new TreatmentServiceSvc();
        }

        [HttpPost("create-treatmentservice")]
        public IActionResult CreateTreatmentService([FromBody] TreatmentServiceReq treatmentServiceReq)
        {
            var res = new SingleRes();
            res = treatmentServiceSvc.CreateTreatmentService(treatmentServiceReq);
            return Ok(res.Data);
        }
    }

}
