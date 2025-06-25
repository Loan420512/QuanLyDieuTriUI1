using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;
using System.Linq;

namespace QLDT.BLL
{
    public class FeedbackSvc : GenericSvc<FeedbackRep, Feedback>
    {
        private FeedbackRep feedbackRep;
        public FeedbackSvc()
        {
            feedbackRep = new FeedbackRep();
        }

        #region -- Methods --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            var feedback = _rep.Read(id);
            res.Data = feedback;
            return res;
        }

        // Search Feedback
        public object SearchFeedback(int? memberId, string? targetType, int? rating, int page, int size)
        {
            var feedbacks = All.AsQueryable();

            if (memberId.HasValue)
            {
                feedbacks = feedbacks.Where(x => x.MemberId == memberId.Value);
            }
            if (!string.IsNullOrEmpty(targetType))
            {
                feedbacks = feedbacks.Where(x => x.TargetType.Contains(targetType));
            }
            if (rating.HasValue)
            {
                feedbacks = feedbacks.Where(x => x.Rating == rating.Value);
            }

            var offset = (page - 1) * size;
            var total = feedbacks.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)(1 + (total / size));

            var data = feedbacks.OrderByDescending(x => x.CreateAt).Skip(offset).Take(size).ToList();

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

        // Create Feedback
        public SingleRes CreateFeedback(FeedbackReq feedbackReq)
        {
            var res = new SingleRes();
            Feedback feedback = new Feedback();
            feedback.MemberId = feedbackReq.MemberId;
            feedback.ContentFeedback = feedbackReq.ContentFeedback;
            feedback.Rating = feedbackReq.Rating;
            feedback.TargetType = feedbackReq.TargetType;
            feedback.CreateAt = feedbackReq.CreateAt ?? DateTime.Now;

            res = feedbackRep.CreateFeedback(feedback);
            return res;
        }

        // Update Feedback
        public SingleRes UpdateFeedback(FeedbackReq feedbackReq)
        {
            var res = new SingleRes();
            var feedback = _rep.Read(feedbackReq.FeedbackId);
            if (feedback == null)
            {
                res.SetError("EZ404", "Feedback not found");
                return res;
            }

            feedback.MemberId = feedbackReq.MemberId;
            feedback.ContentFeedback = feedbackReq.ContentFeedback;
            feedback.Rating = feedbackReq.Rating;
            feedback.TargetType = feedbackReq.TargetType;

            res = feedbackRep.UpdateFeedback(feedback);
            return res;
        }

        public SingleRes DeleteFeedback(int id)
        {
            var res = new SingleRes();
            var feedback = _rep.Read(id);
            if (feedback == null)
            {
                res.SetError("EZ404", "Feedback not found");
            }
            else
            {
                res = feedbackRep.DeleteFeedback(id);
            }
            return res;
        }

        // Get all Feedbacks
        public SingleRes GetFeedbacks()
        {
            var res = new SingleRes();
            var feedbacks = _rep.All.ToList();
            res.SetData("200", feedbacks);
            return res;
        }

        #endregion
    }
}