using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;
using System.Linq;

namespace QLDT.BLL
{
    public class MedicalRecordSvc : GenericSvc<MedicalRecordRep, MedicalRecord>
    {
        private MedicalRecordRep medicalRecordRep;
        public MedicalRecordSvc()
        {
            medicalRecordRep = new MedicalRecordRep();
        }

        #region -- Methods --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            var medicalRecord = _rep.Read(id);
            res.Data = medicalRecord;
            return res;
        }

        // Search MedicalRecord
        public object SearchMedicalRecord(int? memberId, string? keyword, int page, int size)
        {
            var medicalRecords = All.AsQueryable();

            if (memberId.HasValue)
            {
                medicalRecords = medicalRecords.Where(x => x.MemberId == memberId.Value);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                medicalRecords = medicalRecords.Where(x => x.Summary.Contains(keyword));
            }

            var offset = (page - 1) * size;
            var total = medicalRecords.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)(1 + (total / size));

            var data = medicalRecords.OrderByDescending(x => x.CreatedAt).Skip(offset).Take(size).ToList();

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

        // Create MedicalRecord
        public SingleRes CreateMedicalRecord(MedicalRecordReq medicalRecordReq)
        {
            var res = new SingleRes();
            MedicalRecord medicalRecord = new MedicalRecord();
            medicalRecord.MemberId = medicalRecordReq.MemberId;
            medicalRecord.Summary = medicalRecordReq.Summary;
            medicalRecord.CreatedAt = medicalRecordReq.CreatedAt ?? DateTime.Now;

            res = medicalRecordRep.CreateMedicalRecord(medicalRecord);
            return res;
        }

        // Update MedicalRecord
        public SingleRes UpdateMedicalRecord(MedicalRecordReq medicalRecordReq)
        {
            var res = new SingleRes();
            var medicalRecord = _rep.Read(medicalRecordReq.RecordId);
            if (medicalRecord == null)
            {
                res.SetError("EZ404", "MedicalRecord not found");
                return res;
            }

            medicalRecord.MemberId = medicalRecordReq.MemberId;
            medicalRecord.Summary = medicalRecordReq.Summary;

            res = medicalRecordRep.UpdateMedicalRecord(medicalRecord);
            return res;
        }

        public SingleRes DeleteMedicalRecord(int id)
        {
            var res = new SingleRes();
            var medicalRecord = _rep.Read(id);
            if (medicalRecord == null)
            {
                res.SetError("EZ404", "MedicalRecord not found");
            }
            else
            {
                res = medicalRecordRep.DeleteMedicalRecord(id);
            }
            return res;
        }

        // Get all MedicalRecords
        public SingleRes GetMedicalRecords()
        {
            var res = new SingleRes();
            var medicalRecords = _rep.All.ToList();
            res.SetData("200", medicalRecords);
            return res;
        }

        #endregion
    }
}