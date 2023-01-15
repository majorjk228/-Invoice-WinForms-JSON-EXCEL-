using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork15
{
    public class Total
    {
        public string price_of_export { get; set; }
        public string inkoterms { get; set; }
        public int total_price { get; set; }
        public string signature { get; set; }
        public string fio { get; set; }
        public string date { get; set; }
    }

    public class ResultLoop
    {
        public List<Result> Results = new List<Result>();     
    }

    public class Result
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }
}
