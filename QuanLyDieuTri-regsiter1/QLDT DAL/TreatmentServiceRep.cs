using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDT.Common.DAL;
using QLDT.Common.Res;
using QLDT_DAL.Models;

namespace QLDT_DAL
{
    public class TreatmentServiceRep : GenericRep<Test6Context, TreatmentService>
    {
        #region -- Overrides --


        public override TreatmentService Read(int id)
        {
            var res = All.FirstOrDefault(p => p.TreatmentServiceId == id);
            return res;
        }


        public int Remove(int id)
        {
            var m = base.All.First(i => i.TreatmentServiceId == id);
            m = base.Delete(m);
            return m.TreatmentServiceId;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateTreatmentService(TreatmentService treatmentService)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.TreatmentServices.Add(treatmentService);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public SingleRes UpdateTreatmentService(TreatmentService treatmentService)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.TreatmentServices.Update(treatmentService);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        #endregion
    }
}