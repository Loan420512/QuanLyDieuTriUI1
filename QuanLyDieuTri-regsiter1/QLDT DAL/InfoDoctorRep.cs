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
    public class InfoDoctorRep : GenericRep<Test6Context, InfoDoctor>
    {
        public InfoDoctorRep()
        {

        }

        #region -- Overrides --

        public override InfoDoctor Read(int id)
        {
            var res = All.FirstOrDefault(p => p.InfoId == id);
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateInfoDoctor(InfoDoctor infoDoctor)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.InfoDoctors.Add(infoDoctor);
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

        public SingleRes UpdateInfoDoctor(InfoDoctor infoDoctor)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.InfoDoctors.Update(infoDoctor);
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

        public SingleRes DeleteInfoDoctor(int id)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var infoDoctorToDelete = context.InfoDoctors.Find(id);
                        if (infoDoctorToDelete != null)
                        {
                            context.InfoDoctors.Remove(infoDoctorToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetData("200", "InfoDoctor deleted successfully.");
                        }
                        else
                        {
                            res.SetError("EZ404", "InfoDoctor not found for deletion.");
                            tran.Rollback();
                        }
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