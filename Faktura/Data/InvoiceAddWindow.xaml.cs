using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Faktura.Configuration;
using Faktura.Connection;
using Faktura.Logs;

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
            try
            {
                LocalParameters.invoiceDate = txtInvoiceDate.Text;
                LocalParameters.invoiceProductType = txtInvoiceInventory.Text;
                LocalParameters.invoiceNumber = txtInvoiceNumber.Text;
                LocalParameters.invoiceMoney = txtInvoiceMoneyVal.Text;
                if (LocalParameters.netconnection == true)
                {
                    ServerSQLConnection _connection = new ServerSQLConnection();
                    _connection.AddRecord();
                    LogWriter.LogWrite("Dodano fakturę do bazy SQLServer!");
                }
                else if (LocalParameters.netconnection == false)
                {
                    LocalSQLConnection _connection = new LocalSQLConnection();
                    _connection.LocalAddRecord();
                    LogWriter.LogWrite("Dodano fakturę do bazy lokalnej!");
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite(ex.ToString());
            }
            
        }
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogWriter.LogWrite(ex.ToString());
            }
        }
    }
}
