using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CourseWork15
{
    public partial class ChooseCompany : Form
    {
        Form1 form1;
        public ChooseCompany(Form1 owner)   // Принимаем владельца первой формы,
                                            // т.е Сейчас мы как владелец первой формы, зашли на дочернюю форму
                                            // и можешь из 2 формы управлять первой формой (мы делаем это для установки свойств на первой форме)
        {
            InitializeComponent();
            form1 = owner;                  // устанавливаем владельца
        }
        public string file = "companies.json";  // Наш файл    
        private void comboBox1_Click(object sender, EventArgs e)
        {
            try
            {
                //comboBox1.DataSource = JsonOperations<CompanyLoop>.Reads(file).Companies.OrderBy(Company => Company.Name).ToList(); // Выводим весь список компаний
                var companies = JsonOperations<CompanyLoop>.Reads(file).Companies.OrderBy(Company => Company.Name).ToList(); // Выводим весь список компаний
                companies.RemoveAll(x => x.Name == form1.tbcompany.Text); // Находим элемент, который был добавлен и удаляем его из списка
                comboBox1.DataSource = companies;
            }
            catch (Exception)
            {
                MessageBox.Show("Компании отсутствуют", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var data = comboBox1.SelectedItem;
                if (data == null) { throw new Exception(); } // Проверка на выбранную компанию
                var items = JsonOperations<CompanyLoop>.Reads(file).Companies.OrderBy(Company => Company.Name).ToList();
                foreach (var item in items)
                {
                    if (data.ToString() == item.Name)
                    {
                        if (rbFrom.Checked) // Если выбрана кнопка "От кого", то заполняется этот блок.
                        {
                            form1.tbcompany.Text = item.Name;
                            form1.tbfio.Text = item.Fio;
                            form1.tbadress.Text = item.Adress;
                            form1.tbcity.Text = item.Cityindex;
                            form1.tbcountryfrom.Text = item.Country;
                            form1.tbtelfax.Text = item.Phone;
                        }
                        else
                        {
                            form1.tbcompany2.Text = item.Name;
                            form1.tbfio2.Text = item.Fio;
                            form1.tbadress2.Text = item.Adress;
                            form1.tbcity2.Text = item.Cityindex;
                            form1.tbcounry2.Text = item.Country;
                            form1.tbfax2.Text = item.Phone;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Кажется вы не выбрали компанию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
    }
}
