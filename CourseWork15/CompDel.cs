using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CourseWork15
{
    public partial class CompDel : Form
    {
        public CompDel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var data = comboBox1.SelectedItem;
                var items = JsonOperations<string>.Read().Companies.OrderBy(Company => Company.Name).ToList();
                foreach (var item in items)
                {
                    if (data.ToString() == item.Name) // Находим необходимый нам элемент
                    {
                        var companies = JsonConvert.DeserializeObject<CompanyLoop>(File.ReadAllText("companies.json"));
                        companies.Companies.RemoveAll(x => x.Name == data.ToString()); // Удаляем элемент который соответсвует выбранному айтому из комбобокса
                        string json = JsonConvert.SerializeObject(companies, Formatting.Indented);
                        File.WriteAllText("companies.json", json); // Перезаписываем
                        MessageBox.Show("Компания " + data.ToString() + " успешно удалена!");
                        this.Close(); // Закрываем текущую форму
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "companies.json";
                //comboBox1.DataSource = JsonOperations.Read().Companies.OrderBy(Company => Company.Name).ToList();         // Удаляем, написали универсальный метод 
                comboBox1.DataSource = JsonOperations<CompanyLoop>.Reads(file).Companies.OrderBy(Company => Company.Name).ToList(); // Выводим весь список компаний
            }
            catch (Exception)
            {
                MessageBox.Show("Компании отсутствуют.");
                this.Close();
            }
        }
    }
}
