using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using GemBox.Spreadsheet;
using Logger;

namespace SSHomeRestoration
{
    public partial class WeeklyInvoiceGenerator : MaterialForm
    {

        public static string connection = "Server=localhost;Database=sshr;Uid=root;Pwd=root2018";
        public string selectInvoicescommand = "SELECT * FROM invoices";
        MainLogger mL = new MainLogger();

        
        

        public WeeklyInvoiceGenerator()
        {
            InitializeComponent();
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            SSHomeRestore sshr = new SSHomeRestore();
            showWeeklyInvoice();

            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet workSheet = workbook.Worksheets.Add("1");
            workSheet.Cells["A1"].Value = "Hello world!";

            // Save Excel file.
            workbook.Save("Workbook.xls");

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void showWeeklyInvoice()
        {
            using (var sqlCon = new MySqlConnection(connection))
            {
                try
                {
                    sqlCon.Open();
                    using (var command = new MySqlCommand(selectInvoicescommand, sqlCon))
                    {
                        using (var rdr = command.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var address = $"{rdr[4]} {rdr[5]} {rdr[6]}";
                                var listViewItem = new ListViewItem(rdr[1].ToString())
                                {
                                    Tag = rdr[1]
                                };
                                listViewItem.SubItems.Add(rdr[3].ToString());
                                listViewItem.SubItems.Add(address.ToString());
                                weeklyInvoiceView.Items.Add(listViewItem);
                            }
                            rdr.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    mL.ErrorLog(e.ToString());
                }

            }
        }

        private void weeklyInvoiceView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Invoices inv = new Invoices();
        }
    }
}
