using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace CourseWork15
{
    public partial class ChooseItem : Form
    {
        Form1 form1; // Объвляем объект первой формы
        public ChooseItem(Form1 owner) // Принимаем владельца первой формы,
                                       // т.е Сейчас мы как владелец первой формы, зашли на дочернюю форму
                                       // и можешь из 2 формы управлять первой формой (мы делаем это для установки свойств на первой форме)
        {
            form1 = owner;             // устанавливаем владельца
            InitializeComponent();
        }

        public string file = "goods.json";
        private void comboBox1_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.DataSource = JsonOperations<TableLoop>.Reads(file).Tables.OrderBy(Table => Table.description).ToList(); // Выводим весь список компаний
            }
            catch (Exception)
            {
                MessageBox.Show("Товары отсутсвуют");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var data = comboBox1.SelectedItem;
                if (data == null) { throw new Exception(); } // Проверка на выбранный товар
                var items = JsonOperations<TableLoop>.Reads(file).Tables.OrderBy(Table => Table.description).ToList();
                foreach (var item in items)
                {
                    if (data.ToString() == item.description)
                    {
                        form1.WriteRow(form1.dataGridView1, item);
                        this.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Кажется вы не выбрали товар", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
