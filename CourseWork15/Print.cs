using Aspose.Words;
using Aspose.Words.Saving;
using iTextSharp.text.pdf;
using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CourseWork15
{
    public partial class Print : System.Windows.Forms.Form
    {
        public string Date, SName, Fio, Adress, Cityindex, Country, Phone, ToCompany,
                      ToFio, ToAdress, ToCityindex, ToCountry, ToPhone,
                      InvoiceNumber, InvoiceCarier,
                      TInkoterms, TSignature, InvoiceWeight_brutto, InvoiceWeight_netto, InvoicePlaces,
                      TPrice_of_export, TTotal_price, TFio,
                      Description, Code_tnved, DСountry, DСount, DCrice_per_one, DPommon_price;

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
/*            var document = new iTextSharp.text.Document();

            using (var writer = PdfWriter.GetInstance(document, new FileStream("result.pdf", FileMode.Create)))
            {
                document.Open();

                // do some work here
                var logo = iTextSharp.text.Image.GetInstance(new FileStream(@"C:\Users\typakek\Desktop\CourseWork15\CourseWork15\bin\Debug\Invoce.Jpeg", FileMode.Open));
                logo.SetAbsolutePosition(0, 0);
                writer.DirectContent.AddImage(logo);
                document.Close();
                writer.Close();
            }*/
        }


        private string Save()
        {
            //Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";//|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    return saveFileDialog1.FileName;
            }
            return "";
        }

        public int RowsCount, Count;

        public Print()
        {
            InitializeComponent();
            Date = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void Printdoc(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            panelPrint = pnl;
            getprtinarea(pnl); // Этот метод копирует нашу форму
            printDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();

        }

        private Bitmap memoryimg;

      /*  private void getprtinarea(Panel pnl)
        {
            memoryimg = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memoryimg, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }*/

        private void getprtinarea(Panel pnl)
        {
            var doc = new Document();
            var builder = new DocumentBuilder(doc);
            

            memoryimg = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memoryimg, new System.Drawing.Rectangle(0, 0, pnl.Width, pnl.Height));
            using (StreamWriter bitmapWriter = new StreamWriter("Invoce.Jpeg"))
            {
                pnl.DrawToBitmap(memoryimg, new System.Drawing.Rectangle(0, 0, pnl.Width, pnl.Height));
                memoryimg.Save(bitmapWriter.BaseStream, ImageFormat.Jpeg);
                builder.InsertImage(memoryimg);
                doc.Save(Save() /*+ "Output.pdf"*/); // Преобразование из jpeg v pdf    + диалоговое сохранение                            
            }            
        }

        public void Print_Load(object sender, EventArgs e)
        {
            tbcompany.Text = SName;
            tbfio.Text = Fio;
            tbadress.Text = Adress;
            tbcity.Text = Cityindex;
            tbcountryfrom.Text = Country;
            tbtelfax.Text = Phone;
            tbcompany2.Text = ToCompany;
            tbfio2.Text = ToFio;
            tbadress2.Text = ToAdress;
            tbcity2.Text = ToCityindex;
            tbcounry2.Text = ToCountry;
            tbfax2.Text = ToPhone;
            nakladnaya.Text = InvoiceNumber;
            brutto.Text = InvoiceWeight_brutto;
            netto.Text = InvoiceWeight_netto;
            places.Text = InvoicePlaces;
            carier.Text = InvoiceCarier;
            tbexportprice.Text = TPrice_of_export;
            tbinkoterms.Text = TInkoterms;
            lblallprice.Text = TTotal_price;
            tbsignature.Text = TSignature;
            tbfio3.Text = TFio;
            tbdate.Text = Date;
            label31.Text = Convert.ToString(Count - 1);

            //this.Close();
            Printdoc(this.panelPrint);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memoryimg, (pagearea.Width / 2) - (this.panelPrint.Width / 2), (pagearea.Height / 2) - (this.panelPrint.Height / 2));
        }
    }
}
