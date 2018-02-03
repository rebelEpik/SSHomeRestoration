using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Logger;

namespace SSHomeRestoration
{
    public partial class AddNewInvoice : MaterialForm
    {

        public MainLogger mL = new MainLogger();
        public string insertSQL = 
@"INSERT INTO invoices
(workordernum, acctnum, bankid, street, city, state, zip, workperformed, dateadded)
VALUES( @woNum, @acctNum, @bID, @streetName, @cityName, @stateName, @zipCode, @wP, @dA)";

        
        

        private InvoiceAdded _invoiceAddedCallback;

        public AddNewInvoice(
            InvoiceAdded invoiceAddedCallback)
        {
            InitializeComponent();
            _invoiceAddedCallback = invoiceAddedCallback;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Invoices inv = new Invoices()
            {
                workOrderNum = textBox1.Text,
                accountNum = comboBox1.Text,
                bankID = textBox3.Text,
                streetName = textBox4.Text,
                cityName = textBox5.Text,
                stateName = textBox6.Text,
                zipCode = textBox7.Text,
                workCompleted = textBox2.Text,
                dateAdded = DateTime.Today.ToString("dd-MM-yyyy")
            };

            if (insertNewInvoice(inv) == true)
            {
                mL.GeneralLog("INSERT ---- " + inv.workOrderNum + " - " + inv.Address);
                _invoiceAddedCallback(inv);

                textBox1.Text = "";
                comboBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox2.Text = "";

            }

        }



        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool insertNewInvoice(Invoices inv)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(SSHomeRestore.connection))
                {
                    sqlCon.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand())
                    {
                        sqlCommand.CommandText = insertSQL;
                        sqlCommand.Connection = sqlCon;
                        sqlCommand.Parameters.AddWithValue("@woNum", inv.workOrderNum);
                        sqlCommand.Parameters.AddWithValue("@acctNum", inv.accountNum);
                        sqlCommand.Parameters.AddWithValue("@bID", inv.bankID.Trim(new Char[] { '(', ')' }));//SANITIZE THIS
                        sqlCommand.Parameters.AddWithValue("@streetName", inv.streetName);
                        sqlCommand.Parameters.AddWithValue("@cityName", inv.cityName.ToString());
                        sqlCommand.Parameters.AddWithValue("@stateName", inv.stateName);
                        sqlCommand.Parameters.AddWithValue("@zipCode", inv.zipCode);
                        sqlCommand.Parameters.AddWithValue("@wP", inv.workCompleted);
                        sqlCommand.Parameters.AddWithValue("@dA", inv.dateAdded.ToString());
                        int numRowsInserted = sqlCommand.ExecuteNonQuery();
                        if (numRowsInserted == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                mL.ErrorLog(e.ToString());
                return false;
            }
        

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
