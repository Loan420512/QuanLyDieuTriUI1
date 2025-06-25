using QLDT.Common.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;
using System.Linq;

namespace QLDT.BLL
{
    public class MemberSvc : GenericSvc<MemberRep, Member>
    {
        private MemberRep memberRep;
        public MemberSvc()
        {
            memberRep = new MemberRep();
        }

        #region -- Methods --

        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            var member = _rep.Read(id);
            res.Data = member;
            return res;
        }

        // Search Member
        public object SearchMember(string? phoneNumber, string? name, string? gender, int page, int size)
        {
            var members = All.AsQueryable();

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                members = members.Where(x => x.PhoneNumber.Contains(phoneNumber));
            }
            if (!string.IsNullOrEmpty(name))
            {
                members = members.Where(x => x.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(gender))
            {
                members = members.Where(x => x.Gender == gender);
            }

            var offset = (page - 1) * size;
            var total = members.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)(1 + (total / size));

            var data = members.OrderBy(x => x.Name).Skip(offset).Take(size).ToList();

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

        // Create Member
        public SingleRes CreateMember(MemberReq memberReq)
        {
            var res = new SingleRes();
            Member member = new Member();
            member.UserId = memberReq.UserId;
            member.PhoneNumber = memberReq.PhoneNumber;
            member.Name = memberReq.Name;
            member.Gender = memberReq.Gender;

            res = memberRep.CreateMember(member);
            return res;
        }

        // Update Member
        public SingleRes UpdateMember(MemberReq memberReq)
        {
            var res = new SingleRes();
            var member = _rep.Read(memberReq.MemberId);
            if (member == null)
            {
                res.SetError("EZ404", "Member not found");
                return res;
            }

            member.UserId = memberReq.UserId;
            member.PhoneNumber = memberReq.PhoneNumber;
            member.Name = memberReq.Name;
            member.Gender = memberReq.Gender;

            res = memberRep.UpdateMember(member);
            return res;
        }

        public SingleRes DeleteMember(int id)
        {
            var res = new SingleRes();
            var member = _rep.Read(id);
            if (member == null)
            {
                res.SetError("EZ404", "Member not found");
            }
            else
            {
                res = memberRep.DeleteMember(id);
            }
            return res;
        }

        // Get all Members
        public SingleRes GetMembers()
        {
            var res = new SingleRes();
            var members = _rep.All.ToList();
            res.SetData("200", members);
            return res;
        }

        #endregion
    }
}