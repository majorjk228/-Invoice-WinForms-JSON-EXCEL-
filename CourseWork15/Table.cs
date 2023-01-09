using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork15
{
    public class Table
    {
        public string description { get; set; }
        public int code_tnved { get; set; }
        public string country { get; set; }
        public int count { get; set; }
        public int price_per_one { get; set; }
        public int common_price { get; set; }
        public string Date { get; set; }
        public override string ToString() => description;
    }
    public class TableLoop
    {
        public List<Table> Tables { get; set; }
    }
}
