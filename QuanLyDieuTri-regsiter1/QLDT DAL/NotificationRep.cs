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
    public class NotificationRep : GenericRep<Test6Context, Notification>
    {
        public NotificationRep()
        {

        }

        #region -- Overrides --

        public override Notification Read(int id)
        {
            var res = All.FirstOrDefault(p => p.NotificationId == id);
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateNotification(Notification notification)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Notifications.Add(notification);
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

        public SingleRes UpdateNotification(Notification notification)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Notifications.Update(notification);
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

        public SingleRes DeleteNotification(int id)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var notificationToDelete = context.Notifications.Find(id);
                        if (notificationToDelete != null)
                        {
                            context.Notifications.Remove(notificationToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetData("200", "Notification deleted successfully.");
                        }
                        else
                        {
                            res.SetError("EZ404", "Notification not found for deletion.");
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