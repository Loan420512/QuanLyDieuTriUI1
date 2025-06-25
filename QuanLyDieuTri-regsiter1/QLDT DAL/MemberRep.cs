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
    public class MemberRep : GenericRep<Test6Context, Member>
    {
        public MemberRep()
        {

        }

        #region -- Overrides --

        public override Member Read(int id)
        {
            var res = All.FirstOrDefault(p => p.MemberId == id);
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateMember(Member member)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Members.Add(member);
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

        public SingleRes UpdateMember(Member member)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Members.Update(member);
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

        public SingleRes DeleteMember(int id)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var memberToDelete = context.Members.Find(id);
                        if (memberToDelete != null)
                        {
                            context.Members.Remove(memberToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetData("200", "Member deleted successfully.");
                        }
                        else
                        {
                            res.SetError("EZ404", "Member not found for deletion.");
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