using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork15
{
    public class RootLoop
    {
        public List<Root> Roots { get; set; }
    }
    public class Root
    {
        public From From { get; set; }
        public To To { get; set; }
        public Invoice Invoice { get; set; }
        public List<Table> Table { get; set; }
        public Total Total { get; set; }
    }
}
