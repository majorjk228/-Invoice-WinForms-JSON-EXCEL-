using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork15
{
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbDesc.Text == "" || tbCountry.Text == "" || tbPrice.Text == "" || tbQNT.Text == "" || tbTNVED.Text == "" || tbPriceper.Text == "")
                {
                    throw new InvoiceExceptions("Не все поля товара заполнены!");
                }

                var obj = new TableLoop
                {
                    Tables = new List<Table>
                    {
                        new Table()
                        {
                            description = tbDesc.Text,
                            code_tnved = Convert.ToInt32(tbTNVED.Text),
                            country = tbCountry.Text,
                            count = Convert.ToInt32(tbQNT.Text),
                            price_per_one = Convert.ToInt32(tbPriceper.Text),
                            common_price = Convert.ToInt32(tbPrice.Text),
                            Date = DateTime.Now.ToString("dd.MM.yyyy")
                        }
                    }
                };
                string file = @"goods.json";  
                bool fileExist = File.Exists(file);  // Проверка существует ли файл
                if (fileExist)
                {
                    var goods = JsonConvert.DeserializeObject<TableLoop>(File.ReadAllText(file));
                    goods.Tables.Add(new Table() // Добавим новый товар
                    {
                        description = tbDesc.Text,
                        code_tnved = Convert.ToInt32(tbTNVED.Text),
                        country = tbCountry.Text,
                        count = Convert.ToInt32(tbQNT.Text),
                        price_per_one = Convert.ToInt32(tbPriceper.Text),
                        common_price = Convert.ToInt32(tbPrice.Text),
                        Date = DateTime.Now.ToString("dd.MM.yyyy")

                    });
                    string json = JsonConvert.SerializeObject(goods, Formatting.Indented);
                    File.WriteAllText(file, json);   // Перезаписываем с добавлением новой компании
                }
                else
                {
                    //сохранить в json файл в виде массива объектов
                    string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    File.WriteAllText(file, json);  // Если нет, то создаем файл и записываем в него объект
                }

                MessageBox.Show("Товар добавлен", "Успешно!");
                this.Close();
            }
            catch (InvoiceExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void noChar(object sender, KeyPressEventArgs e)
        {
            NoNumber.NoChar(sender, e);
        }
        public void noNum(object sender, KeyPressEventArgs e)
        {
            NoNumber.NoNum(sender, e);
        }

        private void tbPriceper_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                tbPrice.Text = Convert.ToString(Convert.ToInt32(tbQNT.Text) * Convert.ToInt32(tbPriceper.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Введите цену!");
            }
        }
    }
}
