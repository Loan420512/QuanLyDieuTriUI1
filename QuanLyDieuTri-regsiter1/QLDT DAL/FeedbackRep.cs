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
    public class FeedbackRep : GenericRep<Test6Context, Feedback>
    {
        public FeedbackRep()
        {

        }

        #region -- Overrides --

        public override Feedback Read(int id)
        {
            var res = All.FirstOrDefault(p => p.FeedbackId == id);
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateFeedback(Feedback feedback)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Feedbacks.Add(feedback);
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

        public SingleRes UpdateFeedback(Feedback feedback)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Feedbacks.Update(feedback);
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

        public SingleRes DeleteFeedback(int id)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var feedbackToDelete = context.Feedbacks.Find(id);
                        if (feedbackToDelete != null)
                        {
                            context.Feedbacks.Remove(feedbackToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetData("200", "Feedback deleted successfully.");
                        }
                        else
                        {
                            res.SetError("EZ404", "Feedback not found for deletion.");
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