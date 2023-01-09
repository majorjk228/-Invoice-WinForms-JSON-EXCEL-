using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork15
{
    public class InvoiceExceptions : Exception
    {
        public InvoiceExceptions() { }

        public InvoiceExceptions(string message) : base(message)
        { }
    }
}
