using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization; //через проект добавить System.Web.Extensions
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseWork15
{
    public partial class Form1 : Form
    {
        public string Date; // Текущая дата

        public static int Count; // Счетчик инвойсов
        public Form1()
        {
            InitializeComponent();
            Date = DateTime.Now.ToString("dd.MM.yyyy");
            файлToolStripMenuItem1.DropDownItems[4].Visible = false; // Скрываем английскую печать
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tbdate.Text = Date;
        }

        // Запись данных в файл JSON
        private void WriteRootLoop(RootLoop value)
        {
            string json = JsonConvert.SerializeObject(value, Formatting.Indented);
            File.WriteAllText("invoice.json", json); // Если нет, то создаем файл и записываем в него объект
        }
        // Чтение данных с файла JSON (Если файл отсутствует, возвращаем пустой объект)
        private RootLoop ReadRootLoop()
        {
            bool fileExist = File.Exists("invoice.json");
            if (fileExist)
            {
                return JsonConvert.DeserializeObject<RootLoop>(File.ReadAllText("invoice.json"));
            }
            return new RootLoop { Roots = new List<Root>() };
        }

        // Собираем данные с датагрид
        private Table GetTableRow(int RowIndex, DataGridView View)
        {
            return new Table()
            {
                description = View.Rows[RowIndex].Cells[0].Value.ToString(),
                code_tnved = Convert.ToInt32(View.Rows[RowIndex].Cells[1].Value),
                country = View.Rows[RowIndex].Cells[2].Value.ToString(),
                count = Convert.ToInt32(View.Rows[RowIndex].Cells[3].Value),
                price_per_one = Convert.ToInt32(View.Rows[RowIndex].Cells[4].Value),
                common_price = Convert.ToInt32(View.Rows[RowIndex].Cells[5].Value)
            };
        }

        //Пробегаемся по "датагриду(таблице)" и собираем данные в коллекцию
        private List<Table> GetRows(DataGridView View)
        {
            var list = new List<Table>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                var row = GetTableRow(i, View); // Функция возвращает строку таблицы
                list.Add(row);                  // Складываем в список
            }

            return list;
        }

        // Записываем данные в датагрид, считывая данные с колекции "таблиц"
        public void WriteRows(DataGridView View, List<Table> rows)
        {
            foreach (var row in rows)
            {
                View.Rows.Add(row.description, row.code_tnved, row.country, row.count, row.price_per_one, row.common_price);
            }
        }

        // Метод позволяет "переводить" датагрид на английский язык, при смене языка в программе
        public void WriteEngRows(DataGridView View, List<Table> rows) // Пишем весь датагрид сразу с переводом на английский
        {
            foreach (var row in rows)
            {
                View.Rows.Add(TranslateText(row.description, 0), row.code_tnved, TranslateText(row.country, 0), row.count, row.price_per_one, row.common_price);
            }
        }

        // Метод используется в выборе товара, он позволяет записать выбранный товар в датагрид
        public void WriteRow(DataGridView View, Table rows) // Пишем по одному объекту
        {
            View.Rows.Add(rows.description, rows.code_tnved, rows.country, rows.count, rows.price_per_one, rows.common_price);
            int totalSum = Convert.ToInt32(lblallprice.Text) + rows.common_price;
            lblallprice.Text = Convert.ToString(totalSum); //Convert.ToInt32(rows.common_price);   // Считаем общую стоимость
        }
        //перевод
        public string TranslateText(string input, int i)
        {
            if (input.Length > 0)
            {
                string url = null;
                if (i == 0)
                {
                    url = String.Format
                ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                 "ru", "en", Uri.EscapeUriString(input));
                }
                else
                {
                    url = String.Format
                   ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                    "en", "ru", Uri.EscapeUriString(input));
                }
                HttpClient httpClient = new HttpClient();
                string result = httpClient.GetStringAsync(url).Result;
                var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);
                var translationItems = jsonData[0];
                string translation = "";
                foreach (object item in translationItems)
                {
                    IEnumerable translationLineObject = item as IEnumerable;
                    IEnumerator translationLineString = translationLineObject.GetEnumerator();
                    translationLineString.MoveNext();
                    translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
                }
                if (translation.Length > 1) { translation = translation.Substring(1); };
                return translation;
            }
            else
            {
                return input;
            }
        }
        bool flag_ru_lang = true;
        private void EngPic_Click(object sender, EventArgs e)
        {
            if (flag_ru_lang)
            {
                label1.Text = "Attention! Filling in by hand is not allowed!";
                label2.Text = "PROFORMA INVOICE";
                label3.Text = "Account number (if necessary)";
                label5.Text = "Company";
                label4.Text = "From whom";
                label5.Text = "Company:";
                label6.Text = "Full name:";
                label7.Text = "Address:";
                label8.Text = "City:";
                label9.Text = "County:";
                label10.Text = "Phone/Fax:";
                label17.Text = "Company:";
                label16.Text = "Full name:";
                label15.Text = "Address:";
                label14.Text = "City:";
                label13.Text = "Country:";
                label12.Text = "Phone/Fax:";
                label22.Text = "Count places:";
                label21.Text = "Gross Weight:";
                label20.Text = "Net Weigh:";
                label19.Text = "Carrier:";
                label11.Text = "Whom";
                label18.Text = "Invoice №";
                dataGridView1.Columns[0].HeaderText = "Full product description";
                dataGridView1.Columns[1].HeaderText = "Customs Commodity Code";
                dataGridView1.Columns[2].HeaderText = "Country of origin";
                dataGridView1.Columns[3].HeaderText = "Quantity";
                dataGridView1.Columns[4].HeaderText = "Unit Value, Currency";
                dataGridView1.Columns[5].HeaderText = "Subtotal value, Currency";
                label23.Text = "Total value, currency";
                файлToolStripMenuItem1.Text = "File";
                файлToolStripMenuItem1.DropDownItems[0].Text = "Save in JSON";
                файлToolStripMenuItem1.DropDownItems[1].Text = "Save in Excel";
                файлToolStripMenuItem1.DropDownItems[3].Visible = false;
                файлToolStripMenuItem1.DropDownItems[4].Visible = true;
                файлToolStripMenuItem1.DropDownItems[6].Text = "Exit";
                компанииToolStripMenuItem.Text = "Companies";
                компанииToolStripMenuItem.DropDownItems[0].Text = "Add company";
                компанииToolStripMenuItem.DropDownItems[1].Text = "Delete company";
                компанииToolStripMenuItem.DropDownItems[2].Text = "Choose company";
                товарToolStripMenuItem.Text = "Products";
                товарToolStripMenuItem.DropDownItems[0].Text = "Add product";
                товарToolStripMenuItem.DropDownItems[1].Text = "Delete product";
                товарToolStripMenuItem.DropDownItems[2].Text = "Choose product";
                статистикаToolStripMenuItem.Text = "Statistic";
                label29.Text = "Transportation conditions (INCOTERMS) ";
                label28.Text = "Reason for export";
                label27.Text = "I declare that the information mentioned above is true and correct to the best of my knowledge";
                label25.Text = "Name:";
                label26.Text = "Signature:";
                label24.Text = "Date:";
                label30.Text = "Place to print";
                tbcompany.Text = TranslateText(tbcompany.Text, 0);
                tbfio.Text = TranslateText(tbfio.Text, 0);
                tbadress.Text = TranslateText(tbadress.Text, 0);
                tbcity.Text = TranslateText(tbcity.Text, 0);
                tbcountryfrom.Text = TranslateText(tbcountryfrom.Text, 0);
                tbtelfax.Text = TranslateText(tbtelfax.Text, 0);
                tbfio2.Text = TranslateText(tbfio2.Text, 0);
                tbadress2.Text = TranslateText(tbadress2.Text, 0);
                tbcity2.Text = TranslateText(tbcity2.Text, 0);
                tbcounry2.Text = TranslateText(tbcounry2.Text, 0);
                tbfax2.Text = TranslateText(tbfax2.Text, 0);
                nakladnaya.Text = TranslateText(nakladnaya.Text, 0);
                places.Text = TranslateText(places.Text, 0);
                brutto.Text = TranslateText(brutto.Text, 0);
                netto.Text = TranslateText(netto.Text, 0);
                carier.Text = TranslateText(carier.Text, 0);
                tbinkoterms.Text = TranslateText(tbinkoterms.Text, 0);
                tbexportprice.Text = TranslateText(tbexportprice.Text, 0);
                tbsignature.Text = TranslateText(tbsignature.Text, 0);
                tbfio3.Text = TranslateText(tbfio3.Text, 0);
                tbdate.Text = TranslateText(tbdate.Text, 0);

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = TranslateText(dataGridView1.Rows[i].Cells[j].Value.ToString(), 0);
                    }
                }


                flag_ru_lang = false;
            }
        }

        private void pBRus_Click(object sender, EventArgs e)
        {
            label1.Text = "Внимание! Заполнение от руки не допускается!";
            label2.Text = "СЧЕТ-ПРОФОРМА";
            label3.Text = "Номер счета(если необходимо)";
            label4.Text = "Компании";
            label4.Text = "ОТ КОГО";
            label5.Text = "Компания:";
            label6.Text = "ФИО:";
            label7.Text = "Адрес:";
            label8.Text = "Город:";
            label9.Text = "Страна:";
            label10.Text = "Тел./Факс:";
            label17.Text = "Компания";
            label16.Text = "ФИО";
            label15.Text = "Адрес:";
            label14.Text = "Город:";
            label13.Text = "Страна:";
            label12.Text = "Тел./Факс:";
            label22.Text = "Кол-во мест:";
            label21.Text = "Вес Брутто:";
            label20.Text = "Вес Нетто:";
            label19.Text = "Перевозчик:";
            label11.Text = "Кому";
            label18.Text = "НАКЛАДНАЯ №";
            dataGridView1.Columns[0].HeaderText = "Полное описание товара";
            dataGridView1.Columns[1].HeaderText = "Код ТНВЭД";
            dataGridView1.Columns[2].HeaderText = "Страна";
            dataGridView1.Columns[3].HeaderText = "Кол-во";
            dataGridView1.Columns[4].HeaderText = "Цена за ед., валюта";
            dataGridView1.Columns[5].HeaderText = "Общая стоимость, валюта";
            label23.Text = "Общая стоимость, валюта";
            файлToolStripMenuItem1.Text = "Файл";
            файлToolStripMenuItem1.DropDownItems[0].Text = "Сохранить в JSON";
            файлToolStripMenuItem1.DropDownItems[1].Text = "Сохранить в Excel";
            файлToolStripMenuItem1.DropDownItems[3].Text = "Печать";
            файлToolStripMenuItem1.DropDownItems[3].Visible = true;
            файлToolStripMenuItem1.DropDownItems[4].Visible = false;
            файлToolStripMenuItem1.DropDownItems[6].Text = "Выход";
            компанииToolStripMenuItem.Text = "Компании";
            компанииToolStripMenuItem.DropDownItems[0].Text = "Добавить компанию";
            компанииToolStripMenuItem.DropDownItems[1].Text = "Удалить компанию";
            компанииToolStripMenuItem.DropDownItems[2].Text = "Выбрать компанию";
            товарToolStripMenuItem.Text = "Товары";
            товарToolStripMenuItem.DropDownItems[0].Text = "Добавить товар";
            товарToolStripMenuItem.DropDownItems[1].Text = "Удалить товар";
            товарToolStripMenuItem.DropDownItems[2].Text = "Выбрать товар";
            статистикаToolStripMenuItem.Text = "Статистика";
            label29.Text = "Условия транспортировки (ИНКОТЕРМС) -";
            label28.Text = "Цель экспорта:";
            label27.Text = "Подтверждаю, что все вышеуказанное верно";
            label26.Text = "ФИО:";
            label25.Text = "Подпись:";
            label24.Text = "Дата:";
            label30.Text = "Место для печати";
            flag_ru_lang = true;
            tbcompany.Text = TranslateText(tbcompany.Text, 1);
            tbfio.Text = TranslateText(tbfio.Text, 1);
            tbadress.Text = TranslateText(tbadress.Text, 1);
            tbcity.Text = TranslateText(tbcity.Text, 1);
            tbcountryfrom.Text = TranslateText(tbcountryfrom.Text, 1);
            tbtelfax.Text = TranslateText(tbtelfax.Text, 1);
            tbfio2.Text = TranslateText(tbfio2.Text, 1);
            tbadress2.Text = TranslateText(tbadress2.Text, 1);
            tbcity2.Text = TranslateText(tbcity2.Text, 1);
            tbcounry2.Text = TranslateText(tbcounry2.Text, 1);
            tbfax2.Text = TranslateText(tbfax2.Text, 1);
            nakladnaya.Text = TranslateText(nakladnaya.Text, 1);
            places.Text = TranslateText(places.Text, 1);
            brutto.Text = TranslateText(brutto.Text, 1);
            netto.Text = TranslateText(netto.Text, 1);
            carier.Text = TranslateText(carier.Text, 1);
            tbinkoterms.Text = TranslateText(tbinkoterms.Text, 1);
            tbexportprice.Text = TranslateText(tbexportprice.Text, 1);
            tbsignature.Text = TranslateText(tbsignature.Text, 1);
            tbfio3.Text = TranslateText(tbfio3.Text, 1);
            tbdate.Text = TranslateText(tbdate.Text, 1);

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = TranslateText(dataGridView1.Rows[i].Cells[j].Value.ToString(), 1);
                }
            }

        }

        private void сохранитьВJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var rootloop = ReadRootLoop();

                // Идем постепенно, собираем все объекты в переменную
                var from = new From()
                {
                    company = tbcompany.Text,
                    fio = tbfio.Text,
                    adress = tbadress.Text,
                    cityindex = tbcity.Text,
                    country = tbcountryfrom.Text,
                    phone = tbtelfax.Text
                };
                var to = new To()
                {
                    company = tbcompany2.Text,
                    fio = tbfio2.Text,
                    adress = tbadress2.Text,
                    cityindex = tbcity2.Text,
                    country = tbcounry2.Text,
                    phone = tbfax2.Text
                };
                var invoice = new Invoice()
                {
                    number = Convert.ToInt32(nakladnaya.Text),
                    weight_brutto = Convert.ToInt32(brutto.Text),
                    weight_netto = Convert.ToInt32(netto.Text),
                    places = Convert.ToInt32(places.Text),
                    carier = carier.Text
                };
                var total = new Total()
                {
                    price_of_export = tbexportprice.Text,
                    inkoterms = tbinkoterms.Text,
                    total_price = Convert.ToInt32(lblallprice.Text),
                    signature = tbsignature.Text,
                    fio = tbfio3.Text,
                    date = Date
                };
                // Здесь все объединяю в 1 общий объект Root
                var root = new Root()
                {
                    From = from,
                    To = to,
                    Invoice = invoice,
                    Table = GetRows(dataGridView1),
                    Total = total
                };
                rootloop.Roots.Add(root);

                WriteRootLoop(rootloop);

                MessageBox.Show("Данные записаны в JSON");

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void сохранитьВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // метод формирование общего объекта 
                Root root = new Root();
                root.From = new From();
                root.From.company = tbcompany.Text;
                root.From.fio = tbfio.Text;
                root.From.adress = tbadress.Text;
                root.From.cityindex = tbcity.Text;
                root.From.country = tbcountryfrom.Text;
                root.From.phone = tbtelfax.Text;

                root.To = new To();
                root.To.company = tbcompany2.Text;
                root.To.fio = tbfio2.Text;
                root.To.adress = tbadress2.Text;
                root.To.cityindex = tbcity2.Text;
                root.To.country = tbcounry2.Text;
                root.To.phone = tbfax2.Text;

                root.Invoice = new Invoice();
                root.Invoice.number = Convert.ToInt32(nakladnaya.Text);
                root.Invoice.weight_brutto = Convert.ToInt32(brutto.Text);
                root.Invoice.weight_netto = Convert.ToInt32(netto.Text);
                root.Invoice.places = Convert.ToInt32(places.Text);
                root.Invoice.carier = carier.Text;

                root.Table = new List<Table>();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    Table table = new Table();
                    table.description = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    table.code_tnved = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    table.country = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    table.count = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    table.price_per_one = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                    table.common_price = Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                    root.Table.Add(table);
                }

                root.Total = new Total();
                root.Total.price_of_export = tbexportprice.Text;
                root.Total.inkoterms = tbinkoterms.Text;
                root.Total.total_price = Convert.ToInt32(lblallprice.Text);
                root.Total.signature = tbsignature.Text;
                root.Total.fio = tbfio3.Text;
                root.Total.date = DateTime.Now.ToString("dd.MM.yyyy");


                Excel.Application excel = new Excel.Application(); // Создаем обект экселя

                var wb = excel.Workbooks.Add(); // Создаем рабочую область в экселе(книгу)  
                //var sheet = wb.Worksheets.Add(); // Если необходимо добавить еще один лист
                Excel.Worksheet worksheet = wb.ActiveSheet; // Выбираем активный лист экселя, и вставляем в него данные

                // Вставка данных (В начале идет номер строки, затем номер колонки)
                worksheet.Cells[1, 12] = "price_of_export";
                worksheet.Cells[1, 13] = root.Total.price_of_export;
                worksheet.Cells[2, 12] = "inkoterms";
                worksheet.Cells[2, 13] = root.Total.inkoterms;
                worksheet.Cells[3, 12] = "total_price";
                worksheet.Cells[3, 13] = root.Total.total_price;
                worksheet.Cells[4, 12] = "signature";
                worksheet.Cells[4, 13] = root.Total.signature;
                worksheet.Cells[5, 12] = "fio";
                worksheet.Cells[5, 13] = root.Total.fio;
                worksheet.Cells[6, 12] = "date";
                worksheet.Cells[6, 13] = root.Total.date;

                //записать данные в excel HEADER
                worksheet.Cells[1, 2] = "FROM";
                worksheet.Cells[1, 3] = "TO";
                worksheet.Cells[1, 4] = "TABLE";
                worksheet.Cells[1, 14] = "TOTAL";

                //Первый столбец
                worksheet.Cells[2, 1] = "company";
                worksheet.Cells[3, 1] = "fio";
                worksheet.Cells[4, 1] = "adress";
                worksheet.Cells[5, 1] = "cityindex";
                worksheet.Cells[6, 1] = "country";
                worksheet.Cells[7, 1] = "phone";

                //Блок FROM            
                worksheet.Cells[2, 2] = root.From.company;
                worksheet.Cells[3, 2] = root.From.fio;
                worksheet.Cells[4, 2] = root.From.adress;
                worksheet.Cells[5, 2] = root.From.cityindex;
                worksheet.Cells[6, 2] = root.From.country;
                worksheet.Cells[7, 2] = root.From.phone;

                //Блок TO
                worksheet.Cells[2, 3] = root.To.company;
                worksheet.Cells[3, 3] = root.To.fio;
                worksheet.Cells[4, 3] = root.To.adress;
                worksheet.Cells[5, 3] = root.To.cityindex;
                worksheet.Cells[6, 3] = root.To.country;
                worksheet.Cells[7, 3] = root.To.phone;

                // Блок TABLE
                worksheet.Cells[2, 4] = "number";
                worksheet.Cells[2, 5] = root.Invoice.number;
                worksheet.Cells[3, 4] = "weight_brutto";
                worksheet.Cells[3, 5] = root.Invoice.weight_brutto;
                worksheet.Cells[4, 4] = "weight_netto";
                worksheet.Cells[4, 5] = root.Invoice.weight_netto;
                worksheet.Cells[5, 4] = "places";
                worksheet.Cells[5, 5] = root.Invoice.places;
                worksheet.Cells[6, 4] = "carier";
                worksheet.Cells[6, 5] = root.Invoice.carier;

                // Блок Товаров
                worksheet.Cells[1, 7] = "DESCRIPTION";
                worksheet.Cells[1, 8] = "CODE_TNVED";
                worksheet.Cells[1, 9] = "COUNTRY";
                worksheet.Cells[1, 10] = "COUNT";
                worksheet.Cells[1, 11] = "PRICE_PER_ONE";
                worksheet.Cells[1, 12] = "COMMON_PRICE";

                for (int i = 0; i < root.Table.Count; i++)
                {
                    worksheet.Cells[i + 3, 7] = root.Table[i].description;
                    worksheet.Cells[i + 3, 8] = root.Table[i].code_tnved;
                    worksheet.Cells[i + 3, 9] = root.Table[i].country;
                    worksheet.Cells[i + 3, 10] = root.Table[i].count;
                    worksheet.Cells[i + 3, 11] = root.Table[i].price_per_one;
                    worksheet.Cells[i + 3, 12] = root.Table[i].common_price;
                }

                worksheet.Cells[1, 12] = "PRICE_OF_EXPORT";
                worksheet.Cells[1, 13] = root.Total.price_of_export;
                worksheet.Cells[2, 12] = "inkoterms";
                worksheet.Cells[2, 13] = root.Total.inkoterms;
                worksheet.Cells[3, 12] = "total_price";
                worksheet.Cells[3, 13] = root.Total.total_price;
                worksheet.Cells[4, 12] = "signature";
                worksheet.Cells[4, 13] = root.Total.signature;
                worksheet.Cells[5, 12] = "fio";
                worksheet.Cells[5, 13] = root.Total.fio;
                worksheet.Cells[6, 12] = "date";
                worksheet.Cells[6, 13] = root.Total.date;

                wb.SaveAs(@"test23.xlsx"); // Сохраняем файл (Хранится в документах на компе(не в папке с проектом))
                excel.Visible = true; // Открываем эксель

                /*                // метод формирование общего объекта 
                                Root root = new Root();
                                root.From = new From();
                                root.From.company = tbcompany.Text;
                                root.From.fio = tbfio.Text;
                                root.From.adress = tbadress.Text;
                                root.From.cityindex = tbcity.Text;
                                root.From.country = tbcountryfrom.Text;
                                root.From.phone = tbtelfax.Text;

                                root.To = new To();
                                root.To.company = tbcompany2.Text;
                                root.To.fio = tbfio2.Text;
                                root.To.adress = tbadress2.Text;
                                root.To.cityindex = tbcity2.Text;
                                root.To.country = tbcounry2.Text;
                                root.To.phone = tbfax2.Text;

                                root.Invoice = new Invoice();
                                root.Invoice.number = Convert.ToInt32(nakladnaya.Text);
                                root.Invoice.weight_brutto = Convert.ToInt32(brutto.Text);
                                root.Invoice.weight_netto = Convert.ToInt32(netto.Text);
                                root.Invoice.places = Convert.ToInt32(places.Text);
                                root.Invoice.carier = carier.Text;

                                root.Table = new List<Table>();
                                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                                {
                                    Table table = new Table();
                                    table.description = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                    table.code_tnved = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                                    table.country = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                    table.count = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                                    table.price_per_one = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                                    table.common_price = Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                                    root.Table.Add(table);
                                }

                                root.Total = new Total();
                                root.Total.price_of_export = tbexportprice.Text;
                                root.Total.inkoterms = tbinkoterms.Text;
                                root.Total.total_price = Convert.ToInt32(lblallprice.Text);
                                root.Total.signature = tbsignature.Text;
                                root.Total.fio = tbfio3.Text;
                                root.Total.date = DateTime.Now.ToString("dd.MM.yyyy");*/

                //создать новый excel файл
                /*Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];*/

                //string file = @"C:\Users\Asus-pro\source\repos\CourseWork15\test.xlsx";
                //string file = @"test.xlsx";
                //MyExcel excel = new MyExcel(file);
                //Excel xl = new Excel(); //создаем инстанс
                //string file = "C:\Users\Asus-pro\source\repos\CourseWork15\test.xlsx";

                //int countRows = excel.CountRows(1);

                //cntRow = list1.Cells[list1.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row; //последняя заполненная строка в первом столбце
                //cntCol = list1.Cells[1, list1.Columns.Count].End[Excel.XlDirection.xlToLeft].Column;


                /* excel.WriteToCell(1, 1, 1, "FROM");
                 excel.WriteToCell(1, 2, 1, "TO");
                 excel.WriteToCell(1, 3, 1, "INVOICE");
                 excel.WriteToCell(1, 4, 1, "TABLE");
                 excel.WriteToCell(1, 5, 1, "TOTAL");

                 excel.WriteToCell(2, 1, 1, "company");
                 excel.WriteToCell(2, 2, 1, root.From.company);
                 excel.WriteToCell(3, 1, 1, "fio");
                 excel.WriteToCell(3, 2, 1, root.From.fio);
                 excel.WriteToCell(4, 1, 1, "adress");
                 excel.WriteToCell(4, 2, 1, root.From.adress);
                 excel.WriteToCell(5, 1, 1, "cityindex");
                 excel.WriteToCell(5, 2, 1, root.From.cityindex);
                 excel.WriteToCell(6, 1, 1, "country");
                 excel.WriteToCell(6, 2, 1, root.From.country);
                 excel.WriteToCell(7, 1, 1, "phone");
                 excel.WriteToCell(7, 2, 1, root.From.phone);


                 excel.WriteToCell(2, 3, 1, "company");
                 excel.WriteToCell(2, 4, 1, root.To.company);
                 excel.WriteToCell(3, 3, 1, "fio");
                 excel.WriteToCell(3, 4, 1, root.To.fio);
                 excel.WriteToCell(4, 3, 1, "adress");
                 excel.WriteToCell(4, 4, 1, root.To.adress);
                 excel.WriteToCell(5, 3, 1, "cityindex");
                 excel.WriteToCell(5, 4, 1, root.To.cityindex);
                 excel.WriteToCell(6, 3, 1, "country");
                 excel.WriteToCell(6, 4, 1, root.To.country);
                 excel.WriteToCell(7, 3, 1, "phone");
                 excel.WriteToCell(7, 4, 1, root.To.phone);

                 excel.WriteToCell(2, 5, 1, "number");
                 excel.WriteToCell(2, 6, 1, Convert.ToString(root.Invoice.number));
                 excel.WriteToCell(3, 5, 1, "weight_brutto");
                 excel.WriteToCell(3, 6, 1, Convert.ToString(root.Invoice.weight_brutto));
                 excel.WriteToCell(4, 5, 1, "weight_netto");
                 excel.WriteToCell(4, 6, 1, Convert.ToString(root.Invoice.weight_netto));
                 excel.WriteToCell(5, 5, 1, "places");
                 excel.WriteToCell(5, 6, 1, Convert.ToString(root.Invoice.places));
                 excel.WriteToCell(6, 5, 1, "carier");
                 excel.WriteToCell(6, 6, 1, root.Invoice.carier);

                 excel.WriteToCell(2, 7, 1, "description");
                 excel.WriteToCell(2, 8, 1, "code_tnved");
                 excel.WriteToCell(2, 9, 1, "country");
                 excel.WriteToCell(2, 10, 1, "count");
                 excel.WriteToCell(2, 11, 1, "price_per_one");
                 excel.WriteToCell(2, 12, 1, "common_price");

                 for (int i = 0; i < root.Table.Count; i++)
                 {
                     excel.WriteToCell(i + 3, 7, 1, root.Table[i].description);
                     excel.WriteToCell(i + 3, 8, 1, Convert.ToString(root.Table[i].code_tnved));
                     excel.WriteToCell(i + 3, 9, 1, root.Table[i].country);
                     excel.WriteToCell(i + 3, 10, 1, Convert.ToString(root.Table[i].count));
                     excel.WriteToCell(i + 3, 11, 1, Convert.ToString(root.Table[i].price_per_one));
                     excel.WriteToCell(i + 3, 12, 1, Convert.ToString(root.Table[i].common_price));
                 }

                 excel.WriteToCell(2, 13, 1, "price_of_export");
                 excel.WriteToCell(2, 14, 1, root.Total.price_of_export);
                 excel.WriteToCell(3, 13, 1, "inkoterms");
                 excel.WriteToCell(3, 14, 1, root.Total.inkoterms);
                 excel.WriteToCell(4, 13, 1, "total_price");
                 excel.WriteToCell(4, 14, 1, Convert.ToString(root.Total.total_price));
                 excel.WriteToCell(5, 13, 1, "signature");
                 excel.WriteToCell(5, 14, 1, root.Total.signature);
                 excel.WriteToCell(6, 13, 1, "fio");
                 excel.WriteToCell(6, 14, 1, root.Total.fio);
                 excel.WriteToCell(7, 13, 1, "date");
                 excel.WriteToCell(7, 14, 1, root.Total.date);*/

                /*worksheet.Cells[1, 12].PutValue("price_of_export");
                worksheet.Cells[1, 13].PutValue(root.Total.price_of_export);
                worksheet.Cells[2, 12].PutValue("inkoterms");
                worksheet.Cells[2, 13].PutValue(root.Total.inkoterms);
                worksheet.Cells[3, 12].PutValue("total_price");
                worksheet.Cells[3, 13].PutValue(root.Total.total_price);
                worksheet.Cells[4, 12].PutValue("signature");
                worksheet.Cells[4, 13].PutValue(root.Total.signature);
                worksheet.Cells[5, 12].PutValue("fio");
                worksheet.Cells[5, 13].PutValue(root.Total.fio);
                worksheet.Cells[6, 12].PutValue("date");
                worksheet.Cells[6, 13].PutValue(root.Total.date); */

                //cntRow2 = list2.Cells[list2.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;

                //записать данные в excel
                /*worksheet.Cells[0, 0].PutValue("FROM");
                worksheet.Cells[0, 1].PutValue("TO");
                worksheet.Cells[0, 2].PutValue("INVOICE");
                worksheet.Cells[0, 3].PutValue("TABLE");
                worksheet.Cells[0, 4].PutValue("TOTAL");

                worksheet.Cells[1, 0].PutValue("company");
                worksheet.Cells[1, 1].PutValue(root.From.company);
                worksheet.Cells[2, 0].PutValue("fio");
                worksheet.Cells[2, 1].PutValue(root.From.fio);
                worksheet.Cells[3, 0].PutValue("adress");
                worksheet.Cells[3, 1].PutValue(root.From.adress);
                worksheet.Cells[4, 0].PutValue("cityindex");
                worksheet.Cells[4, 1].PutValue(root.From.cityindex);
                worksheet.Cells[5, 0].PutValue("country");
                worksheet.Cells[5, 1].PutValue(root.From.country);
                worksheet.Cells[6, 0].PutValue("phone");
                worksheet.Cells[6, 1].PutValue(root.From.phone);

                worksheet.Cells[1, 2].PutValue("company");
                worksheet.Cells[1, 3].PutValue(root.To.company);
                worksheet.Cells[2, 2].PutValue("fio");
                worksheet.Cells[2, 3].PutValue(root.To.fio);
                worksheet.Cells[3, 2].PutValue("adress");
                worksheet.Cells[3, 3].PutValue(root.To.adress);
                worksheet.Cells[4, 2].PutValue("cityindex");
                worksheet.Cells[4, 3].PutValue(root.To.cityindex);
                worksheet.Cells[5, 2].PutValue("country");
                worksheet.Cells[5, 3].PutValue(root.To.country);
                worksheet.Cells[6, 2].PutValue("phone");
                worksheet.Cells[6, 3].PutValue(root.To.phone);

                worksheet.Cells[1, 4].PutValue("number");
                worksheet.Cells[1, 5].PutValue(root.Invoice.number);
                worksheet.Cells[2, 4].PutValue("weight_brutto");
                worksheet.Cells[2, 5].PutValue(root.Invoice.weight_brutto);
                worksheet.Cells[3, 4].PutValue("weight_netto");
                worksheet.Cells[3, 5].PutValue(root.Invoice.weight_netto);
                worksheet.Cells[4, 4].PutValue("places");
                worksheet.Cells[4, 5].PutValue(root.Invoice.places);
                worksheet.Cells[5, 4].PutValue("carier");
                worksheet.Cells[5, 5].PutValue(root.Invoice.carier);

                worksheet.Cells[1, 6].PutValue("description");
                worksheet.Cells[1, 7].PutValue("code_tnved");
                worksheet.Cells[1, 8].PutValue("country");
                worksheet.Cells[1, 9].PutValue("count");
                worksheet.Cells[1, 10].PutValue("price_per_one");
                worksheet.Cells[1, 11].PutValue("common_price");
                for (int i = 0; i < root.Table.Count; i++)
                {
                    worksheet.Cells[i + 2, 6].PutValue(root.Table[i].description);
                    worksheet.Cells[i + 2, 7].PutValue(root.Table[i].code_tnved);
                    worksheet.Cells[i + 2, 8].PutValue(root.Table[i].country);
                    worksheet.Cells[i + 2, 9].PutValue(root.Table[i].count);
                    worksheet.Cells[i + 2, 10].PutValue(root.Table[i].price_per_one);
                    worksheet.Cells[i + 2, 11].PutValue(root.Table[i].common_price);
                }

                worksheet.Cells[1, 12].PutValue("price_of_export");
                worksheet.Cells[1, 13].PutValue(root.Total.price_of_export);
                worksheet.Cells[2, 12].PutValue("inkoterms");
                worksheet.Cells[2, 13].PutValue(root.Total.inkoterms);
                worksheet.Cells[3, 12].PutValue("total_price");
                worksheet.Cells[3, 13].PutValue(root.Total.total_price);
                worksheet.Cells[4, 12].PutValue("signature");
                worksheet.Cells[4, 13].PutValue(root.Total.signature);
                worksheet.Cells[5, 12].PutValue("fio");
                worksheet.Cells[5, 13].PutValue(root.Total.fio);
                worksheet.Cells[6, 12].PutValue("date");
                worksheet.Cells[6, 13].PutValue(root.Total.date);*/

                //сделать так, чтобы столбцы автоматически подстраивались под размер данных
                //worksheet.AutoFitColumns();

                //сохранить excel файл
                //workbook.Save("C:\\Users\\Asus-pro\\source\\repos\\CourseWork15\\test.xlsx"); // Это сохранит в каталог который у тебя на компе,
                // на моем к примеру он уже другой)
                //WB.Save("test.xlsx"); // Если хочешь универсально сохранять файл то это так.




                //string file = @"test.xlsx";     // Наш файл

                //list1.SaveAs(file);
                //WB.SaveAs(file);
                //WB.Close(true);
                //app.UserControl = true;
                //app.Quit(); //закрываем приложение

                //excel.FileSave(file);

                /*  excel.CleanUp();

                  bool fileExist = File.Exists(file);  // Проверка существует ли файл
                  if (fileExist)
                  {
                      System.Diagnostics.Process.Start(file); // Запускаем эксель
                  }
                  else
                  {
                      throw new Exception("Excel не был создан!");
                  }

                  MessageBox.Show("Данные записаны в Excel");*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void добавитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCompany addCompany = new AddCompany();
            addCompany.ShowDialog();
        }

        private void добавитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem(); // Вызываем формочку с добавлением товаров
            addItem.ShowDialog();
        }

        private void удалитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompDel compDel = new CompDel();
            compDel.ShowDialog();
        }

        private void выбратьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseCompany chooseCompany = new ChooseCompany(this);  // Передаем в качестве владельца второй формы себя, для того чтобы легко управлять со 2 формы на 1 первую.
            chooseCompany.ShowDialog();
        }

        private void удалитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelItem delItem = new DelItem();
            delItem.ShowDialog();
        }

        private void выборТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseItem chooseItem = new ChooseItem(this); // Передаем в качестве владельца второй формы себя, для того чтобы легко управлять со 2 формы на 1 первую.
            chooseItem.ShowDialog();
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graph graph = new Graph();
            graph.ShowDialog();
        }
        public void noChar(object sender, KeyPressEventArgs e)
        {
            NoNumber.NoChar(sender, e);
        }

        public void noNum(object sender, KeyPressEventArgs e)
        {
            NoNumber.NoNum(sender, e);
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print print = new Print(); // Форма
            try
            {
                var from = new From()
                {
                    company = tbcompany.Text,
                    fio = tbfio.Text,
                    adress = tbadress.Text,
                    cityindex = tbcity.Text,
                    country = tbcountryfrom.Text,
                    phone = tbtelfax.Text
                };
                var to = new To()
                {
                    company = tbcompany2.Text,
                    fio = tbfio2.Text,
                    adress = tbadress2.Text,
                    cityindex = tbcity2.Text,
                    country = tbcounry2.Text,
                    phone = tbfax2.Text
                };
                var invoice = new Invoice()
                {
                    number = Convert.ToInt32(nakladnaya.Text),
                    weight_brutto = Convert.ToInt32(brutto.Text),
                    weight_netto = Convert.ToInt32(netto.Text),
                    places = Convert.ToInt32(places.Text),
                    carier = carier.Text
                };
                var total = new Total()
                {
                    price_of_export = tbexportprice.Text,
                    inkoterms = tbinkoterms.Text,
                    total_price = Convert.ToInt32(lblallprice.Text),
                    signature = tbsignature.Text,
                    fio = tbfio3.Text,
                    date = Date
                };
                var root = new Root()
                {
                    From = from,
                    To = to,
                    Invoice = invoice,
                    Table = GetRows(dataGridView1),
                    Total = total
                };

                WriteRows(print.dataGridView1, root.Table); // Пишем данные с 1 формы датагрида, на 2 форму.            

                // Передаем данные с формы на печать
                // 
                print.SName = tbcompany.Text;
                print.Fio = tbfio.Text;
                print.Adress = tbadress.Text;
                print.Cityindex = tbcity.Text;
                print.Country = tbcountryfrom.Text;
                print.Phone = tbtelfax.Text;
                print.ToCompany = tbcompany2.Text;
                print.ToFio = tbfio2.Text;
                print.ToAdress = tbadress2.Text;
                print.ToCityindex = tbcity2.Text;
                print.ToCountry = tbcounry2.Text;
                print.ToPhone = tbfax2.Text;
                print.InvoiceNumber = nakladnaya.Text;
                print.InvoiceWeight_brutto = brutto.Text;
                print.InvoiceWeight_netto = netto.Text;
                print.InvoicePlaces = places.Text;
                print.InvoiceCarier = carier.Text;
                print.RowsCount = dataGridView1.Rows.Count;
                print.TPrice_of_export = tbexportprice.Text;
                print.TInkoterms = tbinkoterms.Text;
                print.TTotal_price = lblallprice.Text;
                print.TSignature = tbsignature.Text;
                print.TFio = tbfio3.Text;
                Count = Convert.ToInt32(label31.Text) + 1; // Увеличиваем СЧЕТ-ПРОФОРМУ
                label31.Text = Convert.ToString(Count);
                print.Count = Count;

                print.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void печтьАнглВерсиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnglishForm englishForm = new EnglishForm(); // Форма

            try
            {
                var from = new From()
                {
                    company = tbcompany.Text,
                    fio = tbfio.Text,
                    adress = tbadress.Text,
                    cityindex = tbcity.Text,
                    country = tbcountryfrom.Text,
                    phone = tbtelfax.Text
                };
                var to = new To()
                {
                    company = tbcompany2.Text,
                    fio = tbfio2.Text,
                    adress = tbadress2.Text,
                    cityindex = tbcity2.Text,
                    country = tbcounry2.Text,
                    phone = tbfax2.Text
                };
                var invoice = new Invoice()
                {
                    number = Convert.ToInt32(nakladnaya.Text),
                    weight_brutto = Convert.ToInt32(brutto.Text),
                    weight_netto = Convert.ToInt32(netto.Text),
                    places = Convert.ToInt32(places.Text),
                    carier = carier.Text
                };
                var total = new Total()
                {
                    price_of_export = tbexportprice.Text,
                    inkoterms = tbinkoterms.Text,
                    total_price = Convert.ToInt32(lblallprice.Text),
                    signature = tbsignature.Text,
                    fio = tbfio3.Text,
                    date = Date
                };
                var root = new Root()
                {
                    From = from,
                    To = to,
                    Invoice = invoice,
                    Table = GetRows(dataGridView1),
                    Total = total
                };

                WriteEngRows(englishForm.dataGridView1, root.Table); // Пишем данные с 1 формы датагрида, на 2 форму.            

                // Передаем данные с формы на печать     
                englishForm.DName = tbcompany.Text;
                englishForm.Fio = tbfio.Text;
                englishForm.Adress = tbadress.Text;
                englishForm.Cityindex = tbcity.Text;
                englishForm.Country = tbcountryfrom.Text;
                englishForm.Phone = tbtelfax.Text;
                englishForm.ToCompany = tbcompany2.Text;
                englishForm.ToFio = tbfio2.Text;
                englishForm.ToAdress = tbadress2.Text;
                englishForm.ToCityindex = tbcity2.Text;
                englishForm.ToCountry = tbcounry2.Text;
                englishForm.ToPhone = tbfax2.Text;
                englishForm.InvoiceNumber = nakladnaya.Text;
                englishForm.InvoiceWeight_brutto = brutto.Text;
                englishForm.InvoiceWeight_netto = netto.Text;
                englishForm.InvoicePlaces = places.Text;
                englishForm.InvoiceCarier = carier.Text;
                englishForm.RowsCount = dataGridView1.Rows.Count;
                englishForm.TPrice_of_export = tbexportprice.Text;
                englishForm.TInkoterms = tbinkoterms.Text;
                englishForm.TTotal_price = lblallprice.Text;
                englishForm.TSignature = tbsignature.Text;
                englishForm.TFio = tbfio3.Text;
                /*                Count = Convert.ToInt32(label31.Text) + 1; // Увеличиваем СЧЕТ-ПРОФОРМУ
                                label31.Text = Convert.ToString(Count);*/
                englishForm.Count = Count;

                englishForm.ShowDialog();
                Count = Convert.ToInt32(label31.Text) + 1; // Увеличиваем СЧЕТ-ПРОФОРМУ
                label31.Text = Convert.ToString(Count);
            }
            catch (Exception exception)
            {
                MessageBox.Show(TranslateText(exception.Message, 0));
            }
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
