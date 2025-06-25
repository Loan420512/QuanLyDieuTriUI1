using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;
using System.Linq;

namespace QLDT.BLL
{
    public class ResultSvc : GenericSvc<ResultRep, Result>
    {
        private ResultRep resultRep;
        public ResultSvc()
        {
            resultRep = new ResultRep();
        }

        #region -- Methods --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            var result = _rep.Read(id);
            res.Data = result;
            return res;
        }

        // Search Result
        public object SearchResult(int? examinationId, string? keyword, int page, int size)
        {
            var results = All.AsQueryable();

            if (examinationId.HasValue)
            {
                results = results.Where(x => x.ExaminationId == examinationId.Value);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                results = results.Where(x => x.ResultTest.Contains(keyword));
            }

            var offset = (page - 1) * size;
            var total = results.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)(1 + (total / size));

            var data = results.OrderBy(x => x.ResultId).Skip(offset).Take(size).ToList();

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

        // Create Result
        public SingleRes CreateResult(ResultReq resultReq)
        {
            var res = new SingleRes();
            Result result = new Result();
            result.ExaminationId = resultReq.ExaminationId;
            result.ResultTest = resultReq.ResultTest;

            res = resultRep.CreateResult(result);
            return res;
        }

        // Update Result
        public SingleRes UpdateResult(ResultReq resultReq)
        {
            var res = new SingleRes();
            var result = _rep.Read(resultReq.ResultId);
            if (result == null)
            {
                res.SetError("EZ404", "Result not found");
                return res;
            }

            result.ExaminationId = resultReq.ExaminationId;
            result.ResultTest = resultReq.ResultTest;

            res = resultRep.UpdateResult(result);
            return res;
        }

        public SingleRes DeleteResult(int id)
        {
            var res = new SingleRes();
            var result = _rep.Read(id);
            if (result == null)
            {
                res.SetError("EZ404", "Result not found");
            }
            else
            {
                res = resultRep.DeleteResult(id);
            }
            return res;
        }

        // Get all Results
        public SingleRes GetResults()
        {
            var res = new SingleRes();
            var results = _rep.All.ToList();
            res.SetData("200", results);
            return res;
        }

        #endregion
    }
}