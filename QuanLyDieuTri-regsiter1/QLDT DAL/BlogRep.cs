using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDT.Common.DAL;
using QLDT.Common.Res;
using QLDT_DAL.Models; // Ensure this namespace is correct for your models

namespace QLDT_DAL
{
    public class BlogRep : GenericRep<Test6Context, Blog> // Assuming Test6Context is your DbContext
    {
        public BlogRep()
        {

        }

        #region -- Overrides --

        public override Blog Read(int id)
        {
            var res = All.FirstOrDefault(p => p.IdBlog == id);
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateBlog(Blog blog)
        {
            var res = new SingleRes();
            using (var context = new Test6Context()) // Assuming Test6Context is your DbContext
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Blogs.Add(blog);
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

        public SingleRes UpdateBlog(Blog blog)
        {
            var res = new SingleRes();
            using (var context = new Test6Context()) // Assuming Test6Context is your DbContext
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Blogs.Update(blog);
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

        public SingleRes DeleteBlog(int id) // Phương thức Delete được thêm vào
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var blogToDelete = context.Blogs.Find(id);
                        if (blogToDelete != null)
                        {
                            context.Blogs.Remove(blogToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetData("200", "Blog deleted successfully."); // Hoặc trả về đối tượng đã xóa nếu cần
                        }
                        else
                        {
                            res.SetError("EZ404", "Blog not found for deletion.");
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