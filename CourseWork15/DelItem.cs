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
    public partial class DelItem : Form
    {
        public DelItem()
        {
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
                MessageBox.Show("Товары отсутствуют.");
                this.Close();
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var data = comboBox1.SelectedItem;
                var items = JsonOperations<TableLoop>.Reads(file).Tables.OrderBy(Table => Table.description).ToList();
                foreach (var item in items)
                {
                    if (data.ToString() == item.description)                            // Находим необходимый нам элемент
                    {
                        var goods = JsonConvert.DeserializeObject<TableLoop>(File.ReadAllText(file));
                        goods.Tables.RemoveAll(x => x.description == data.ToString()); // Удаляем элемент который соответсвует выбранному айтому из комбобокса
                        string json = JsonConvert.SerializeObject(goods, Formatting.Indented);
                        File.WriteAllText(file, json);                                 // Перезаписываем
                        MessageBox.Show("Товар " + data.ToString() + " успешно удален!");
                        this.Close();                                                  // Закрываем текущую форму
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
