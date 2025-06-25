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
    public class ExaminationRep : GenericRep<Test6Context, Examination>
    {
        public ExaminationRep()
        {

        }

        #region -- Overrides --

        public override Examination Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ExaminationId == id);
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateExamination(Examination examination)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Examinations.Add(examination);
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

        public SingleRes UpdateExamination(Examination examination)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Examinations.Update(examination);
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

        public SingleRes DeleteExamination(int id)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var examinationToDelete = context.Examinations.Find(id);
                        if (examinationToDelete != null)
                        {
                            context.Examinations.Remove(examinationToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetData("200", "Examination deleted successfully.");
                        }
                        else
                        {
                            res.SetError("EZ404", "Examination not found for deletion.");
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