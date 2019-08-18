using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;
using Faktura.Connection;
using Faktura.Output;
using Faktura.Configuration;
using System.Data.SqlClient;

namespace Faktura.Data
{
    /// <summary>
    /// Logika interakcji dla klasy InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        SqlConnection connection;
        public InvoiceWindow()
        {
            InitializeComponent();
            try
            {
                invoiceGrid.ItemsSource = MainParameters.dataTable.DefaultView;
                invoiceGrid.ColumnWidth = 155;
                txtsumInvoice.Text = Convert.ToString(LocalParameters.sumInvoice) + " PLN";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            InvoiceAddWindow adwin = new InvoiceAddWindow();
            adwin.ShowDialog();
        }
        private void butttonRemove_Click(object sender, RoutedEventArgs e)
        {
            LocalParameters.idNumber = invoiceGrid.SelectedIndex + 1;
            if (LocalParameters.netconnection == true)
            {
                ServerSQLConnection _connection = new ServerSQLConnection();
                _connection.DeleteRecord();
            }
            else if (LocalParameters.netconnection == false)
            {
                LocalSQLConnection _connection = new LocalSQLConnection();
                _connection.LocalDeleteRecord();
            }
        }
        private void buttonExitInvoice_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            invoiceGrid.ItemsSource = null;
            MainParameters.dataTable.Clear();
        }
        private void buttonPrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            PDFPrinter print = new PDFPrinter();
            print.CreatePDF();
            print.PrintPDF();
        }

        private void buttonHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto:" + "hubert.kuszynski@go.policja.gov.pl" + "?subject=" + "Problem z programem Faktura 2019" + "&body="
                + "Proszę o załączenie loga programu z katalogu Logs/ProgramLogs.");
        }
    }
}
