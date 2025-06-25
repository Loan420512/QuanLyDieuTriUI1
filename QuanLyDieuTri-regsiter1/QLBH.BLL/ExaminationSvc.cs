using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;
using System.Linq;

namespace QLDT.BLL
{
    public class ExaminationSvc : GenericSvc<ExaminationRep, Examination>
    {
        private ExaminationRep examinationRep;
        public ExaminationSvc()
        {
            examinationRep = new ExaminationRep();
        }

        #region -- Methods --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            var examination = _rep.Read(id);
            res.Data = examination;
            return res;
        }

        // Search Examination
        public object SearchExamination(int? bookingId, int? doctorUserId, DateOnly? dateMeet, int page, int size)
        {
            var examinations = All.AsQueryable();

            if (bookingId.HasValue)
            {
                examinations = examinations.Where(x => x.BookingId == bookingId.Value);
            }
            if (doctorUserId.HasValue)
            {
                examinations = examinations.Where(x => x.DoctorUserId == doctorUserId.Value);
            }
            if (dateMeet.HasValue)
            {
                examinations = examinations.Where(x => x.DateMeet == dateMeet.Value);
            }

            var offset = (page - 1) * size;
            var total = examinations.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)(1 + (total / size));

            var data = examinations.OrderByDescending(x => x.DateMeet).Skip(offset).Take(size).ToList();

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

        // Create Examination
        public SingleRes CreateExamination(ExaminationReq examinationReq)
        {
            var res = new SingleRes();
            Examination examination = new Examination();
            examination.BookingId = examinationReq.BookingId;
            examination.DoctorUserId = examinationReq.DoctorUserId;
            examination.DateMeet = examinationReq.DateMeet;

            res = examinationRep.CreateExamination(examination);
            return res;
        }

        // Update Examination
        public SingleRes UpdateExamination(ExaminationReq examinationReq)
        {
            var res = new SingleRes();
            var examination = _rep.Read(examinationReq.ExaminationId);
            if (examination == null)
            {
                res.SetError("EZ404", "Examination not found");
                return res;
            }

            examination.BookingId = examinationReq.BookingId;
            examination.DoctorUserId = examinationReq.DoctorUserId;
            examination.DateMeet = examinationReq.DateMeet;

            res = examinationRep.UpdateExamination(examination);
            return res;
        }

        public SingleRes DeleteExamination(int id)
        {
            var res = new SingleRes();
            var examination = _rep.Read(id);
            if (examination == null)
            {
                res.SetError("EZ404", "Examination not found");
            }
            else
            {
                res = examinationRep.DeleteExamination(id);
            }
            return res;
        }

        // Get all Examinations
        public SingleRes GetExaminations()
        {
            var res = new SingleRes();
            var examinations = _rep.All.ToList();
            res.SetData("200", examinations);
            return res;
        }

        #endregion
    }
}