using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDT.Common.BLL;
using QLDT.Common.Res;
using QLDT_DAL;
using QLDT_DAL.Models;

namespace QLDT.BLL
{
    public class BookingSvc : GenericSvc<BookingRep, Booking>
    {
        private BookingRep bookingReq;
        public BookingSvc()
        {
            bookingReq = new BookingRep();
        }
        public override SingleRes Read(int id)
        {
            var res = new SingleRes();
            res.Data = _rep.Read(id);
            return res;
        }
    }
}
