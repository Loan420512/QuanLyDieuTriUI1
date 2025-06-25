using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.BLL
{
    using System.Linq.Expressions;
    using DAL;
    using QLDT.Common.Res;
    using Res;
    using static QLDT.Common.DAL.IGenericRep;

    public class GenericSvc<D, T> : IGenericSvc<T> where T : class where D : IGenericRep<T>, new()
    {
        #region -- Implements --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Create(T m)
        {
            var res = new SingleRes();

            try
            {
                var now = DateTime.Now;
                _rep.Create(m);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Create the models
        /// </summary>
        /// <param name="l">List model</param>
        /// <returns>Return the result</returns>
        public virtual MultipleRes Create(List<T> l)
        {
            var res = new MultipleRes();

            try
            {
                _rep.Create(l);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>
        public IQueryable<T> Read(Expression<Func<T, bool>> p)
        {
            return _rep.Read(p);
        }


        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual SingleRes Read(int id)
        {
            return null;
        }

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        public virtual SingleRes Read(string code)
        {
            return null;
        }
        public virtual SingleRes Read(string code, string pass)
        {
            return null;
        }

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Update(T m)
        {
            var res = new SingleRes();

            try
            {
                _rep.Update(m);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Update the models
        /// </summary>
        /// <param name="l">List model</param>
        /// <returns>Return the result</returns>
        public virtual MultipleRes Update(List<T> l)
        {
            var res = new MultipleRes();

            try
            {
                _rep.Update(l);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Delete(string code)
        {
            return null;
        }

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Restore(int id)
        {
            return null;
        }

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Restore(string code)
        {
            return null;
        }

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public virtual int Remove(int id)
        {
            return 0;
        }

        SingleRes IGenericSvc<T>.Create(T m)
        {
            throw new NotImplementedException();
        }

        MultipleRes IGenericSvc<T>.Create(List<T> l)
        {
            throw new NotImplementedException();
        }

        SingleRes IGenericSvc<T>.Read(int id)
        {
            throw new NotImplementedException();
        }

        SingleRes IGenericSvc<T>.Read(string code)
        {
            throw new NotImplementedException();
        }

        SingleRes IGenericSvc<T>.Update(T m)
        {
            throw new NotImplementedException();
        }

        MultipleRes IGenericSvc<T>.Update(List<T> l)
        {
            throw new NotImplementedException();
        }

        SingleRes IGenericSvc<T>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        SingleRes IGenericSvc<T>.Delete(string code)
        {
            throw new NotImplementedException();
        }

        SingleRes IGenericSvc<T>.Restore(int id)
        {
            throw new NotImplementedException();
        }

        SingleRes IGenericSvc<T>.Restore(string code)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return query all data
        /// </summary>
        public IQueryable<T> All
        {
            get
            {
                return _rep.All;
            }
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GenericSvc()
        {
            _rep = new D();
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// The repository
        /// </summary>
        protected D _rep;

        #endregion
    }
}