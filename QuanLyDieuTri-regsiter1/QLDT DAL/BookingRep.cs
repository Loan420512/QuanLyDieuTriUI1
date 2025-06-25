using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDT.Common.DAL;
using QLDT_DAL.Models;

namespace QLDT_DAL
{
    public class BookingRep : GenericRep<Test6Context, Booking>
    {
        public BookingRep()
        {
            
        }
        public override Booking Read(int id)
        {
            var res = All.FirstOrDefault(C => C.BookingId == id);
            return res;
        }
    }
}
