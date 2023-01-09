using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork15
{
    //серилизацию и десирилазцию в JSON
    public class JsonOperations<T>
    {
        // Лямбда выражение на чтение из JSON
        public static CompanyLoop Read() =>
        JsonConvert.DeserializeObject<CompanyLoop>(File.ReadAllText("companies.json"));

        // Данный метод более универсальный, чем метод выше. Т.к. принимает в себя любой объект класса с помощью обобщенных типов (generic)
        // Лямбда выражение на чтение из JSON
        public static T Reads(string file) =>
                JsonConvert.DeserializeObject<T>(File.ReadAllText(file));

        // Лямбда выражение на запись в JSON
        public static string Write(object root) =>
        JsonConvert.SerializeObject(root, Formatting.Indented);
    }
}
