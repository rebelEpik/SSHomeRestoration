using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.IO;
using Logger;



namespace SSHomeRestoration
{

    public delegate void InvoiceAdded(Invoices invoice);
    public delegate void EmployeeAdded(Employee emp);

    public partial class SSHomeRestore : MaterialForm
    {
        public static string connection = "Server=localhost;Database=sshr;Uid=root;Pwd=root2018";
        public string selectInvoicescommand = "SELECT * FROM invoices";
        public string selectEmployeesCommand = "SELECT name, address, phoneNum, email, aspenGroveID FROM employees";
        public string selectOrderInfo = "SELECT workordernum, acctnum, bankid, street, city, state, zip, workperformed, dateadded FROM invoices WHERE workordernum = @ORDER";
        public string selectEmployeeInfo = "SELECT name, address, phoneNum, email, aspenGroveID FROM employees WHERE aspenGroveID = @aGID";
        public string updateOrderInfo;
        public string deleteSQLInfo = "DELETE FROM invoices WHERE workordernum = @woNum";
        public string filePath = "C:\\Users\\Bryan\\Desktop\\Dropbox\\S & S\\S&S Home Restoration\\Clients\\Geaux REO LLC\\Invoices\\MasterInvoiceList.csv";
        public string workordernum;
        public MainLogger mL = new MainLogger();
        

        public SSHomeRestore()
        {
             
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            showInvoiceListView();
            showEmployeesListView();
            setInvoiceAssignmentBox();
            showInventoryListView();


            Opt options = new Opt();
            if (options.firstrun == true)
            {
                //ADD FIRST RUN PAGE HERE
            }
            if (options.invoicemanageonstart == true)
            {
                //AddNewInvoice im = new AddNewInvoice();
                //im.Show();
            }
            if (options.workordermanagershow == true)
            {
                WorkOrders wo = new WorkOrders();
                wo.Show();
            }
        }
        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.Show();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        } //Close GUI by Menu Click
        private void weeklyInvoiceToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        public void showInventoryListView()
        {
            using (var sqlCon = new MySqlConnection(connection))
            {
                try
                {
                    sqlCon.Open();
                    using (var command = new MySqlCommand("SELECT * FROM inventory", sqlCon))
                    {
                        using (var rdr = command.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var listViewItem = new ListViewItem(rdr[1].ToString())
                                {
                                    Tag = rdr[1]
                                };
                                listViewItem.SubItems.Add(rdr[2].ToString());
                                listViewItem.SubItems.Add(rdr[3].ToString());
                                listViewItem.SubItems.Add(rdr[4].ToString());
                                listViewItem.SubItems.Add(rdr[5].ToString());
                                //listViewItem.SubItems.Add(address.ToString());
                                inventoryListView.Items.Add(listViewItem);
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



        private void OnEmployeeAdded(Employee emp)
        {
            writeEmployeeToListView(emp);
        }
        private void writeEmployeeToListView(Employee emp)
        {
            ListViewItem lvI = new ListViewItem(emp.name)
            {
                Tag = emp.aspenGroveID
            };
            employeesListView.Items.Add(lvI);
        }
        private void employeesListView_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < employeesListView.Items.Count; i++)
                if (employeesListView.Items[i].Selected == true)
                {

                    string aspenGrove = employeesListView.Items[i].Tag.ToString();
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            con.Open(); // You might want to have this in a try{}catch()-block.

                            using (MySqlCommand command = con.CreateCommand())
                            {
                                command.CommandText = selectEmployeeInfo;
                                command.Parameters.AddWithValue("@aGID", aspenGrove);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())// If you're expecting only one line, change this to if(reader.Read()).
                                    {
                                        empNameTb.Text = reader.GetString(0);
                                        empAddTb.Text = reader.GetString(1);
                                        empPhoneTb.Text = reader.GetString(2);
                                        empEmailTb.Text = reader.GetString(3);
                                        empAspenGroveTb.Text = reader.GetString(4);
                                        aspenPicture.Load(Directory.GetCurrentDirectory() + "\\Pictures\\" + reader.GetString(4) + ".jpg");

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception f)
                    {
                        mL.ErrorLog(f.ToString());
                    }
                }

        }
        public void showEmployeesListView()
        {
            using (var sqlCon = new MySqlConnection())
            {
                sqlCon.ConnectionString = connection;
                try
                {
                    sqlCon.Open();
                    using (var command = new MySqlCommand())
                    {
                        command.CommandText = selectEmployeesCommand;
                        command.Connection = sqlCon;
                        using (var rdr = command.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                ListViewItem listViewItem = new ListViewItem(rdr[0].ToString())
                                {
                                    Tag = rdr[4].ToString()
                                };

                                employeesListView.Items.Add(listViewItem);
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

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            AddEmployee addEmp = new AddEmployee(OnEmployeeAdded);
            addEmp.Show();
           
        }

        //INVOICE HANDLING
        /// <summary>
        ///Handles all aspects of Invoice tab
        /// </summary>
        /// <param name="InvoiceHandling"></param>
        /// <returns></returns>
        public bool updateInvoiceInfo(Invoices inv)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection())
                {
                    sqlCon.ConnectionString = connection;
                    string updateOrderInfo =
                    "UPDATE invoices SET workordernum = @woNum , acctnum = @acctNum , bankid = @bId , street = @streetName , city = @cityName , state = @stateName , zip = @zipCode , workperformed = @wP , dateadded = @dA WHERE workordernum = @woNum";
                    sqlCon.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand())
                    {
                        sqlCommand.Connection = sqlCon;
                        sqlCommand.CommandText = updateOrderInfo;

                        sqlCommand.Parameters.AddWithValue("@woNum", inv.workOrderNum);
                        sqlCommand.Parameters.AddWithValue("@acctNum", inv.accountNum);
                        sqlCommand.Parameters.AddWithValue("@bID", inv.bankID.Trim(new Char[] { '(', ')' }));//SANITIZE THIS
                        sqlCommand.Parameters.AddWithValue("@streetName", inv.streetName);
                        sqlCommand.Parameters.AddWithValue("@cityName", inv.cityName.ToString());
                        sqlCommand.Parameters.AddWithValue("@stateName", inv.stateName);
                        sqlCommand.Parameters.AddWithValue("@zipCode", inv.zipCode);
                        sqlCommand.Parameters.AddWithValue("@wP", inv.workCompleted);
                        sqlCommand.Parameters.AddWithValue("@dA", inv.dateAdded.ToString());

                        int numRowsUpdated = sqlCommand.ExecuteNonQuery();
                        if (numRowsUpdated == 1)
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
        private void onUpdateRecordClick(object sender, EventArgs e)
        {
            Invoices inv = new Invoices();
            inv.workOrderNum = workOrderNumberTb.Text;
            inv.bankID = bankIDTb.Text.ToString();
            inv.accountNum = acctNumBox.SelectedItem.ToString().Replace('(', ' ').Replace(')', ' ');
            inv.workCompleted = richTb.Text.ToString();
            inv.dateAdded = DateTime.Today.ToString("MM-dd-yyyy");
            inv.streetName = streetAddressTb.Text.ToString();
            inv.cityName = cityTb.Text.ToString();
            inv.stateName = stateTb.Text.ToString();
            inv.zipCode = zipTb.Text.ToString();
            if (updateInvoiceInfo(inv) == true)
            {
                invoiceView.Items.Remove(invoiceView.SelectedItems[0]);
                writeInvoiceToListView(inv);
            }
        }
        private void onAddNewInvoiceClicked(object sender, EventArgs e)
        {
            AddNewInvoice anI = new AddNewInvoice(OnInvoiceAdded);
            anI.Show();
        }
        private void InvoiceView_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < invoiceView.Items.Count; i++)
                if (invoiceView.Items[i].Selected == true)
                {

                    workordernum = invoiceView.Items[i].SubItems[0].Text;
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            con.Open(); // You might want to have this in a try{}catch()-block.

                            using (MySqlCommand command = con.CreateCommand())
                            {
                                command.CommandText = selectOrderInfo;
                                command.Parameters.AddWithValue("@ORDER", workordernum);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())// If you're expecting only one line, change this to if(reader.Read()).
                                    {
                                        workOrderNumberTb.Text = reader.GetString(0);
                                        bankIDTb.Text = reader.GetString(2);
                                        acctNumBox.Text = reader.GetString(1);
                                        richTb.Text = reader.GetString(7);
                                        cityTb.Text = reader.GetString(4);
                                        streetAddressTb.Text = reader.GetString(3);
                                        stateTb.Text = reader.GetString(5);
                                        zipTb.Text = reader.GetString(6);
                                        //1465
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception f)
                    {
                        mL.ErrorLog(f.ToString());
                    }
                }




        }
        private void OnInvoiceAdded(Invoices invoice)
        {
            writeInvoiceToListView(invoice);
        }
        private void writeInvoiceToListView(Invoices invoices)
        {
            var listViewItem = new ListViewItem(invoices.workOrderNum)
            {
                Tag = invoices.workOrderNum
            };
            listViewItem.SubItems.Add(invoices.bankID);
            listViewItem.SubItems.Add(invoices.Address);

            invoiceView.Items.Add(listViewItem);
        }
        private void onDeleteInvoiceClick(object sender, EventArgs e)
        {
            Invoices inv = new Invoices()
            {
                workOrderNum = workOrderNumberTb.Text,
                accountNum = acctNumBox.Text,
                bankID = bankIDTb.Text,
                streetName = streetAddressTb.Text,
                cityName = cityTb.Text,
                stateName = stateTb.Text,
                zipCode = zipTb.Text,
                workCompleted = richTb.Text,
            };
            foreach (ListViewItem item in invoiceView.Items)
            {
                if ((string)item.Tag == inv.workOrderNum)
                {
                    item.Remove();
                    if (DeleteInvoiceRecord(inv) == true)
                    {
                        mL.GeneralLog("DELETED ----" + inv.workOrderNum + " - " + inv.Address);
                    }

                }
            }
        }
        private void updateInvoiceBtnClick(object sender, EventArgs e)
        {
            Invoices inv = new Invoices();
            inv.workOrderNum = workOrderNumberTb.Text;
            inv.bankID = bankIDTb.Text.ToString();
            if (acctNumBox.SelectedItem == null)
            {
                inv.accountNum = acctNumBox.Text;
            }
            else
            {
                inv.accountNum = acctNumBox.SelectedItem.ToString();
            }

            inv.workCompleted = richTb.Text.ToString();
            inv.dateAdded = DateTime.Today.ToString("MM-dd-yyyy");
            inv.streetName = streetAddressTb.Text.ToString();
            inv.cityName = cityTb.Text.ToString();
            inv.stateName = stateTb.Text.ToString();
            inv.zipCode = zipTb.Text.ToString();
            if (updateInvoiceInfo(inv) == true)
            {
                invoiceView.Items.Remove(invoiceView.SelectedItems[0]);
                writeInvoiceToListView(inv);
            }
        } //When clicking Update Order, this fires
        private bool DeleteInvoiceRecord(Invoices inv)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection())
                {
                    try
                    {
                        mySqlCon.ConnectionString = connection;
                        mySqlCon.Open();

                        using (MySqlCommand mySqlCommand = new MySqlCommand())
                        {
                            mySqlCommand.Connection = mySqlCon;
                            mySqlCommand.CommandText = deleteSQLInfo;
                            mySqlCommand.Parameters.AddWithValue("@woNum", inv.workOrderNum);
                            int i = mySqlCommand.ExecuteNonQuery();
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void showInvoiceListView()
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
                                invoiceView.Items.Add(listViewItem);
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
        public void setInvoiceAssignmentBox()
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection())
                {
                    sqlCon.ConnectionString = connection;
                    sqlCon.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand())
                    {
                        sqlCommand.Connection = sqlCon;
                        sqlCommand.CommandText = "SELECT * FROM employees";
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                string assignBoxText = reader[1].ToString() + " (" + reader[6].ToString() + ")";
                                assignOrderBox.Items.Add(assignBoxText);


                                
                            }
                        }
                    }
                }
            }catch(Exception e)
            {
                mL.ErrorLog(e.ToString());
            }
        }

        private void assignOrderSelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(assignOrderBox.SelectedItem.ToString());
        }

        private void inventoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < inventoryListView.Items.Count; i++)
                if (inventoryListView.Items[i].Selected == true)
                {

                    string pID = inventoryListView.Items[i].SubItems[0].Text;
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            con.Open(); // You might want to have this in a try{}catch()-block.

                            using (MySqlCommand command = con.CreateCommand())
                            {
                                string selectInventoryInfo = "SELECT * FROM inventory WHERE itemID = @pID";
                                command.CommandText = selectInventoryInfo;
                                command.Parameters.AddWithValue("@pID", pID );

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())// If you're expecting only one line, change this to if(reader.Read()).
                                    {
                                        partNumLbl.Text = reader.GetString(1);
                                        partNameLbl.Text = reader.GetString(2);

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception f)
                    {
                        mL.ErrorLog(f.ToString());
                    }
                }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            WeeklyInvoiceGenerator wiG = new WeeklyInvoiceGenerator();
            wiG.Show();
        }
    }
}
