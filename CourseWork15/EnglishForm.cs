using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork15
{
    public partial class EnglishForm : Form
    {

        public string Date, DName, Fio, Adress, Cityindex, Country, Phone, ToCompany,
                      ToFio, ToAdress, ToCityindex, ToCountry, ToPhone,
                      InvoiceNumber, InvoiceCarier,
                      TInkoterms, TSignature, InvoiceWeight_brutto, InvoiceWeight_netto, InvoicePlaces,
                      TPrice_of_export, TTotal_price, TFio,
                      Description, Code_tnved, DСountry, DСount, DCrice_per_one, DPommon_price;

        private void EnglishForm_Load(object sender, EventArgs e)
        {
            tbcompany.Text = DName;
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

            this.Close();
            Printdoc(this.panelPrint);
        }

        public int RowsCount, Count;

        public EnglishForm()
        {
            InitializeComponent();
            Date = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void Printdoc(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            panelPrint = pnl;
            getprtinarea(pnl);
            printDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();
        }

        private Bitmap memoryimg;

        private void getprtinarea(Panel pnl)
        {
            memoryimg = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memoryimg, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memoryimg, (pagearea.Width / 2) - (this.panelPrint.Width / 2), (pagearea.Height / 2) - (this.panelPrint.Height / 2));
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Printdoc(this.panelPrint);
        }
    }
}
