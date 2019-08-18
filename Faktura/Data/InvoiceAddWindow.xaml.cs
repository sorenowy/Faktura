using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Faktura.Configuration;
using System.Data.SqlClient;
using Faktura.Connection;

namespace Faktura.Data
{
    /// <summary>
    /// Interaction logic for InvoiceAddWindow.xaml
    /// </summary>
    public partial class InvoiceAddWindow : Window
    {
        public InvoiceAddWindow()
        {
            InitializeComponent();
        }

        private void buttonAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            LocalParameters.invoiceDate = txtInvoiceDate.Text;
            LocalParameters.invoiceProductType = txtInvoiceInventory.Text;
            LocalParameters.invoiceNumber = txtInvoiceNumber.Text;
            LocalParameters.invoiceMoney = txtInvoiceMoneyVal.Text;
            if (LocalParameters.netconnection == true)
            {
                ServerSQLConnection _connection = new ServerSQLConnection();
                _connection.AddRecord();
            }
            else if (LocalParameters.netconnection == false)
            {
                LocalSQLConnection _connection = new LocalSQLConnection();
                _connection.LocalAddRecord();
            }
        }
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (LocalParameters.netconnection == true)
            {
                ServerSQLConnection _connection = new ServerSQLConnection();
                _connection.RefreshView();
            }
            else if (LocalParameters.netconnection == false)
            {
                LocalSQLConnection _connection = new LocalSQLConnection();
                _connection.LocalRefreshView();
            }
        }
    }
}
