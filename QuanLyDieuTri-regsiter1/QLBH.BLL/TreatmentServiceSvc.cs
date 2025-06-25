using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;

namespace QLDT.BLL
{
    public class TreatmentServiceSvc : GenericSvc<TreatmentServiceRep, TreatmentService>
    {
        private TreatmentServiceRep treatmentServiceRep;

        #region -- Overrides --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }
        public override SingleRes Update(TreatmentService m)
        {
            var res = new SingleRes();

            var m1 = m.TreatmentServiceId > 0 ? _rep.Read(m.TreatmentServiceId) : _rep.Read(m.Name);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        public TreatmentServiceSvc()
        {
            treatmentServiceRep = new TreatmentServiceRep();
        }

        #endregion
        public SingleRes CreateTreatmentService(TreatmentServiceReq treatmentServiceReq)
        {
            var res = new SingleRes();
            TreatmentService treatmentService = new TreatmentService();
            treatmentService.TreatmentServiceId = treatmentServiceReq.TreatmentServiceId;
            treatmentService.Name = treatmentServiceReq.Name;
            treatmentService.Price = treatmentServiceReq.Price;
            treatmentService.Durations = treatmentServiceReq.Durations;
            treatmentService.Descriptions = treatmentServiceReq.Descriptions;
            res = treatmentServiceRep.CreateTreatmentService(treatmentService);
            return res;
        }

    }
}
