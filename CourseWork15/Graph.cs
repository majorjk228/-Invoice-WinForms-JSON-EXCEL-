using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CourseWork15
{
    public partial class Graph : Form
    {
        //string file = @"goods.json";

        string file = @"results.json";

        public Graph()
        {
            InitializeComponent();

            MyExcel excel = new MyExcel();
            excel.Open();
            excel.Find_Month();
            MyExcel.CleanUp();
            var month = JsonOperations<ResultLoop>.Reads(file).Results.ToList();
            foreach (var item in month)
            {
                switch (item.Month)
                {
                    case "Январь":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Январь", item.Count);
                        break;
                    case "Февраль":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Февраль", item.Count);
                        break;
                    case "Март":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Март", item.Count);
                        break;
                    case "Апрель":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Апрель", item.Count);
                        break;
                    case "Май":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Май", item.Count);
                        break;
                    case "Июнь":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Июнь", item.Count);
                        break;
                    case "Июль":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Июль", item.Count);
                        break;
                    case "Август":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Август", item.Count);
                        break;
                    case "Сентябрь":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Сентябрь", item.Count);
                        break;
                    case "Октябрь":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Октябрь", item.Count);
                        break;
                    case "Ноябрь":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Ноябрь", item.Count);
                        break;
                    case "Декабрь":
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Декабрь", item.Count);
                        break;
                }
            }


            /*var goods = JsonOperations<TableLoop>.Reads(file).Tables
                                                .GroupBy(Table => Convert.ToDateTime(Table.Date).Month, Table => Table.count)
                                                .Select(g => new { Name = g.Key, Count = g.Count(), Cnt = g.Sum() }); // Тут группируется по месяцам,
                                                                                                                      // при этом считается общая сумма за месяц
            foreach (var item in goods)
            {
                switch (Convert.ToInt32(item.Name))
                {
                    case 1:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Январь", item.Cnt);
                        break;
                    case 2:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Февраль", item.Cnt);
                        break;
                    case 3:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Март", item.Cnt);
                        break;
                    case 4:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Апрель", item.Cnt);
                        break;
                    case 5:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Май", item.Cnt);
                        break;
                    case 6:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Июнь", item.Cnt);
                        break;
                    case 7:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Июль", item.Cnt);
                        break;
                    case 8:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Август", item.Cnt);
                        break;
                    case 9:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Сентябрь", item.Cnt);
                        break;
                    case 10:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Октябрь", item.Cnt);
                        break;
                    case 11:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Ноябрь", item.Cnt);
                        break;
                    case 12:
                        chart1.Series["Cтатистика по количеству проданных товаров (Statistics on the number of products sold)"].Points.AddXY("Декабрь", item.Cnt);
                        break;
                }
            }*/
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
