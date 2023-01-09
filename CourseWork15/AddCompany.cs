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
    public partial class AddCompany : Form
    {
        public AddCompany()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка на заполнение полей
                if (tbCompany.Text == "" || tbFIO.Text == "" || tbAdress.Text == "" || tbCity.Text == "" || tbCountry.Text == "" || tbPhone.Text == "")
                {
                    throw new InvoiceExceptions("Не все строки заполнены");
                }

                var obj = new CompanyLoop
                {
                    Companies = new List<Company>
                    {
                        new Company()
                        {
                            Name = tbCompany.Text,
                            Fio = tbFIO.Text,
                            Adress = tbAdress.Text,
                            Cityindex = tbCity.Text,
                            Country = tbCountry.Text,
                            Phone = tbPhone.Text
                        }
                    }
                };
                string file = @"companies.json";     // Наш файл
                bool fileExist = File.Exists(file);  // Проверка существует ли файл
                if (fileExist)
                {
                    var companies = JsonConvert.DeserializeObject<CompanyLoop>(File.ReadAllText("companies.json"));
                    companies.Companies.Add(new Company() // Добавим новую компанию
                    {
                        Name = tbCompany.Text,
                        Fio = tbFIO.Text,
                        Adress = tbAdress.Text,
                        Cityindex = tbCity.Text,
                        Country = tbCountry.Text,
                        Phone = tbPhone.Text
                    });
                    string json = JsonConvert.SerializeObject(companies, Formatting.Indented);
                    File.WriteAllText(file, json); // Перезаписываем с добавлением новой компании
                }
                else
                {
                    //сохранить в json файл в виде массива об  объектов
                    string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    File.WriteAllText(file, json); // Если нет, то создаем файл и записываем в него объект
                }

                MessageBox.Show("Компания добавлена", "Успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void noNum(object sender, KeyPressEventArgs e)
        {
            NoNumber.NoNum(sender, e);
        }
        public void noChar(object sender, KeyPressEventArgs e)
        {
            NoNumber.NoChar(sender, e);
        }
    }
}
