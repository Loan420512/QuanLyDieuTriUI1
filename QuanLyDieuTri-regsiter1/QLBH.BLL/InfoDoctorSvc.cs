using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;
using System.Linq;

namespace QLDT.BLL
{
    public class InfoDoctorSvc : GenericSvc<InfoDoctorRep, InfoDoctor>
    {
        private InfoDoctorRep infoDoctorRep;
        public InfoDoctorSvc()
        {
            infoDoctorRep = new InfoDoctorRep();
        }

        #region -- Methods --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            var infoDoctor = _rep.Read(id);
            res.Data = infoDoctor;
            return res;
        }

        // Search InfoDoctor
        public object SearchInfoDoctor(string? fullName, string? speciality, int? experianYear, int page, int size)
        {
            var infoDoctors = All.AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
            {
                infoDoctors = infoDoctors.Where(x => x.FullName.Contains(fullName));
            }
            if (!string.IsNullOrEmpty(speciality))
            {
                infoDoctors = infoDoctors.Where(x => x.Speciality.Contains(speciality));
            }
            if (experianYear.HasValue)
            {
                infoDoctors = infoDoctors.Where(x => x.ExperianYear >= experianYear.Value);
            }

            var offset = (page - 1) * size;
            var total = infoDoctors.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)(1 + (total / size));

            var data = infoDoctors.OrderBy(x => x.FullName).Skip(offset).Take(size).ToList();

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

        // Create InfoDoctor
        public SingleRes CreateInfoDoctor(InfoDoctorReq infoDoctorReq)
        {
            var res = new SingleRes();
            InfoDoctor infoDoctor = new InfoDoctor();
            infoDoctor.UserId = infoDoctorReq.UserId;
            infoDoctor.Certificate = infoDoctorReq.Certificate;
            infoDoctor.ExperianYear = infoDoctorReq.ExperianYear;
            infoDoctor.FullName = infoDoctorReq.FullName;
            infoDoctor.Speciality = infoDoctorReq.Speciality;
            infoDoctor.Degree = infoDoctorReq.Degree;
            infoDoctor.PhoneNumber = infoDoctorReq.PhoneNumber;

            res = infoDoctorRep.CreateInfoDoctor(infoDoctor);
            return res;
        }

        // Update InfoDoctor
        public SingleRes UpdateInfoDoctor(InfoDoctorReq infoDoctorReq)
        {
            var res = new SingleRes();
            var infoDoctor = _rep.Read(infoDoctorReq.InfoId);
            if (infoDoctor == null)
            {
                res.SetError("EZ404", "InfoDoctor not found");
                return res;
            }

            infoDoctor.UserId = infoDoctorReq.UserId;
            infoDoctor.Certificate = infoDoctorReq.Certificate;
            infoDoctor.ExperianYear = infoDoctorReq.ExperianYear;
            infoDoctor.FullName = infoDoctorReq.FullName;
            infoDoctor.Speciality = infoDoctorReq.Speciality;
            infoDoctor.Degree = infoDoctorReq.Degree;
            infoDoctor.PhoneNumber = infoDoctorReq.PhoneNumber;

            res = infoDoctorRep.UpdateInfoDoctor(infoDoctor);
            return res;
        }

        public SingleRes DeleteInfoDoctor(int id)
        {
            var res = new SingleRes();
            var infoDoctor = _rep.Read(id);
            if (infoDoctor == null)
            {
                res.SetError("EZ404", "InfoDoctor not found");
            }
            else
            {
                res = infoDoctorRep.DeleteInfoDoctor(id);
            }
            return res;
        }

        // Get all InfoDoctors
        public SingleRes GetInfoDoctors()
        {
            var res = new SingleRes();
            var infoDoctors = _rep.All.ToList();
            res.SetData("200", infoDoctors);
            return res;
        }

        #endregion
    }
}