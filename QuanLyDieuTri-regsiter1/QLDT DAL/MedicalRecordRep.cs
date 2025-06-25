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
    public class MedicalRecordRep : GenericRep<Test6Context, MedicalRecord>
    {
        public MedicalRecordRep()
        {

        }

        #region -- Overrides --

        public override MedicalRecord Read(int id)
        {
            var res = All.FirstOrDefault(p => p.RecordId == id);
            return res;
        }

        #endregion

        #region -- Methods --

        public SingleRes CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.MedicalRecords.Add(medicalRecord);
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

        public SingleRes UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.MedicalRecords.Update(medicalRecord);
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

        public SingleRes DeleteMedicalRecord(int id)
        {
            var res = new SingleRes();
            using (var context = new Test6Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var medicalRecordToDelete = context.MedicalRecords.Find(id);
                        if (medicalRecordToDelete != null)
                        {
                            context.MedicalRecords.Remove(medicalRecordToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetData("200", "MedicalRecord deleted successfully.");
                        }
                        else
                        {
                            res.SetError("EZ404", "MedicalRecord not found for deletion.");
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