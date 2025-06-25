using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;
using System.Linq;

namespace QLDT.BLL
{
    public class NotificationSvc : GenericSvc<NotificationRep, Notification>
    {
        private NotificationRep notificationRep;
        public NotificationSvc()
        {
            notificationRep = new NotificationRep();
        }

        #region -- Methods --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            var notification = _rep.Read(id);
            res.Data = notification;
            return res;
        }

        // Search Notification
        public object SearchNotification(int? userId, string? keyword, bool? isRead, int page, int size)
        {
            var notifications = All.AsQueryable();

            if (userId.HasValue)
            {
                notifications = notifications.Where(x => x.UserId == userId.Value);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                notifications = notifications.Where(x => x.ContentNoti.Contains(keyword));
            }
            if (isRead.HasValue)
            {
                notifications = notifications.Where(x => x.IsRead == isRead.Value);
            }

            var offset = (page - 1) * size;
            var total = notifications.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)(1 + (total / size));

            var data = notifications.OrderByDescending(x => x.CreatedAt).Skip(offset).Take(size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = page,
                Size = size
            };

            return res;
        }

        // Create Notification
        public SingleRes CreateNotification(NotificationReq notificationReq)
        {
            var res = new SingleRes();
            Notification notification = new Notification();
            notification.UserId = notificationReq.UserId;
            notification.ContentNoti = notificationReq.ContentNoti;
            notification.CreatedAt = notificationReq.CreatedAt ?? DateTime.Now;
            notification.IsRead = notificationReq.IsRead ?? false;

            res = notificationRep.CreateNotification(notification);
            return res;
        }

        // Update Notification
        public SingleRes UpdateNotification(NotificationReq notificationReq)
        {
            var res = new SingleRes();
            var notification = _rep.Read(notificationReq.NotificationId);
            if (notification == null)
            {
                res.SetError("EZ404", "Notification not found");
                return res;
            }

            notification.UserId = notificationReq.UserId;
            notification.ContentNoti = notificationReq.ContentNoti;
            notification.CreatedAt = notificationReq.CreatedAt;
            notification.IsRead = notificationReq.IsRead;

            res = notificationRep.UpdateNotification(notification);
            return res;
        }

        public SingleRes DeleteNotification(int id)
        {
            var res = new SingleRes();
            var notification = _rep.Read(id);
            if (notification == null)
            {
                res.SetError("EZ404", "Notification not found");
            }
            else
            {
                res = notificationRep.DeleteNotification(id);
            }
            return res;
        }

        // Get all Notifications
        public SingleRes GetNotifications(bool? isRead = null)
        {
            var res = new SingleRes();
            var notifications = _rep.All.AsQueryable();

            if (isRead.HasValue)
            {
                notifications = notifications.Where(x => x.IsRead == isRead.Value);
            }

            res.SetData("200", notifications.ToList());
            return res;
        }

        #endregion
    }
}