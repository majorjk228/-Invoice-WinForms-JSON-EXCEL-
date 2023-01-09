using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork15
{
    public class CompanyLoop
    {
        public List<Company> Companies { get; set; }
    }

    public class Company
    {
        public string Name { get; set; }
        public string Fio { get; set; }
        public string Adress { get; set; }
        public string Cityindex { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public override string ToString() => Name;
    }
}
