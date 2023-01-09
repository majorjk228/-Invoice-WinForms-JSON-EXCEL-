using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork15
{
    public partial class Graph : Form
    {
        string file = @"goods.json";

        public Graph()
        {
            InitializeComponent();
            chart1.Series["Series1"].LegendText = "Кол-во товаров";

            var goods = JsonOperations<TableLoop>.Reads(file).Tables
                                                .GroupBy(Table => Table.Date, Table => Table.count)
                                                .Select(g => new { Name = g.Key, Count = g.Count(), Cnt = g.Sum() }); // Тут группируется по месяцам,
                                                                                                                      // при этом считается общая сумма за месяц
            foreach (var item in goods)
            {
                switch (Convert.ToInt32((Convert.ToDateTime(item.Name).Month)))
                {
                    case 1:
                        chart1.Series["Series1"].Points.AddXY("Январь", item.Cnt);
                        break;
                    case 2:
                        chart1.Series["Series1"].Points.AddXY("Февраль", item.Cnt);
                        break;
                    case 3:
                        chart1.Series["Series1"].Points.AddXY("Март", item.Cnt);
                        break;
                    case 4:
                        chart1.Series["Series1"].Points.AddXY("Апрель", item.Cnt);
                        break;
                    case 5:
                        chart1.Series["Series1"].Points.AddXY("Май", item.Cnt);
                        break;
                    case 6:
                        chart1.Series["Series1"].Points.AddXY("Июнь", item.Cnt);
                        break;
                    case 7:
                        chart1.Series["Series1"].Points.AddXY("Июль", item.Cnt);
                        break;
                    case 8:
                        chart1.Series["Series1"].Points.AddXY("Август", item.Cnt);
                        break;
                    case 9:
                        chart1.Series["Series1"].Points.AddXY("Сентябрь", item.Cnt);
                        break;
                    case 10:
                        chart1.Series["Series1"].Points.AddXY("Октябрь", item.Cnt);
                        break;
                    case 11:
                        chart1.Series["Series1"].Points.AddXY("Ноябрь", item.Cnt);
                        break;
                    case 12:
                        chart1.Series["Series1"].Points.AddXY("Декабрь", item.Cnt);
                        break;
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
