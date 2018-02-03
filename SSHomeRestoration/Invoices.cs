using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SSHomeRestoration
{
    public class Invoices
    {
        public string workOrderNum { get; set; }
        public string accountNum { get; set; }
        public string bankID { get; set; }
        public string streetName { get; set; }
        public string cityName { get; set; }
        public string stateName { get; set; }
        public string zipCode { get; set; }
        public string workCompleted { get; set; }
        public string filePath = "C:\\Users\\Bryan\\Desktop\\Dropbox\\S & S\\S&S Home Restoration\\Clients\\Geaux REO LLC\\Invoices\\MasterInvoiceList.csv";
        public string clientHeader = "Your Name" + "," + "Account #" + "," + "Bank ID/Client #" + "," + "Wo Order #" + "," + "Address" + "," + "City" + "," + "State" + "," + "Zip" + "," + "Leave blank" + "," + "Work performed" + "," + "Date Added" + Environment.NewLine;
        public string address { get; set; }
        public string dateAdded { get; set; }
        public string invInfo { get; set; }


        public string Address
        {
            get => $"{streetName} {cityName}, {stateName} {zipCode}";
        }

        public void saveWeeklyInvoice(string wonum, string acct, string bank, string street, string city, string state, string zip, string workcomplete)
        {
            

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, clientHeader);
 
            }
            invInfo = "S&S Home Restoration LLP" + "," + acct + "," + bank + "," + wonum + "," + street + "," + city + "," + state + "," + zip + ",," + workcomplete + "," + DateTime.Today.ToString("MM-dd-yyyy") + Environment.NewLine;
            File.AppendAllText(filePath, invInfo);

        }

        public void checkInvoiceList()
        {
            if (!File.Exists(filePath))
            {
                this.clientHeader = "Your Name" + "," + "Account #" + "," + "Bank ID/Client #" + "," + "Wo Order #" + "," + "Address" + "," + "City" + "," + "State" + "," + "Zip" + "," + "Leave blank" + "," + "Work performed" + "," + "Date Added" + Environment.NewLine;
                File.WriteAllText(filePath, clientHeader);

            }

        }
    }
}
