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
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dzik\source\repos\Faktura\Faktura\Data\Invoice.mdf;Integrated Security=True";
        string query = "DELETE FROM InvoiceData WHERE Id=@Id";
        string queryRefreshRow = "DBCC CHECKIDENT(InvoiceData, RESEED,@Id)";
        public InvoiceWindow()
        {
            InitializeComponent();
            try
            {
                /*var securePassword = new System.Security.SecureString();
                foreach (var character in LocalParameters.password)
                {
                    securePassword.AppendChar(character);
                }
                securePassword.MakeReadOnly();
                var credential = new SqlCredential(LocalParameters.username, securePassword);
                using (connection = new SqlConnection("Data Source=DESKTOP-9BU76HS\\SQLEXPRESS;Initial Catalog=Faktura;Integrated Security=False",credential))
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT Id as Numer, InvoiceDate as Data_Faktury, Type as Rodzaj_Sprzetu, InvoiceNumber as Numer_Faktury, MoneyValue as Kwota FROM TableInvoice", connection))
                {
                    connection.Open();
                    sqladapter.Fill(MainParameters.dataTable);
                    invoiceGrid.ItemsSource = MainParameters.dataTable.DefaultView;
                    invoiceGrid.ColumnWidth = 155;
                    double sum = Convert.ToDouble(MainParameters.dataTable.Compute("SUM(Kwota)", string.Empty));
                    txtsumInvoice.Text = Convert.ToString(sum) + " PLN";
                } */
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT Id as Numer, InvoiceDate as Data_Faktury, Type as Rodzaj_Sprzetu, InvoiceNumber as Numer_Faktury, MoneyValue as Kwota FROM InvoiceData", connection))
                {
                    connection.Open();
                    sqladapter.Fill(MainParameters.dataTable);
                    invoiceGrid.ItemsSource = MainParameters.dataTable.DefaultView;
                    invoiceGrid.ColumnWidth = 155;
                    LocalParameters.sumInvoice = Convert.ToDouble(MainParameters.dataTable.Compute("SUM(Kwota)", string.Empty));
                    txtsumInvoice.Text = Convert.ToString(LocalParameters.sumInvoice) + " PLN";
                }
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
            var Id = invoiceGrid.SelectedIndex+1;
            using (connection = new SqlConnection(LocalParameters.localSqlConnect))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"Id", Id);
                cmd.ExecuteNonQuery();
                Id = invoiceGrid.SelectedIndex;
            }
            using (connection = new SqlConnection(LocalParameters.localSqlConnect))
            using (SqlCommand cmd = new SqlCommand(queryRefreshRow, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"Id", Id);
                cmd.ExecuteNonQuery();
            }
        }
        private void buttonExitInvoice_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void buttonPrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT Id as Numer, InvoiceDate as Data_Faktury, Type as Rodzaj_Sprzetu, InvoiceNumber as Numer_Faktury, MoneyValue as Kwota FROM InvoiceData", connection))
            {
                connection.Open();
                invoiceGrid.ItemsSource = null;
                MainParameters.dataTable.Clear();
                sqladapter.Fill(MainParameters.dataTable);
                invoiceGrid.ItemsSource = MainParameters.dataTable.DefaultView;
                invoiceGrid.ColumnWidth = 155;
                LocalParameters.sumInvoice = Convert.ToDouble(MainParameters.dataTable.Compute("SUM(Kwota)", string.Empty));
                txtsumInvoice.Text = Convert.ToString(LocalParameters.sumInvoice + " PLN");
                PDFPrinter print = new PDFPrinter();
                print.CreatePDF();
                print.PrintPDF();
            }
        }

        private void buttonHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto:" + "hubert.kuszynski@go.policja.gov.pl" + "?subject=" + "Problem z programem Faktura 2019" + "&body="
                + "Proszę o załączenie loga programu z katalogu Logs/ProgramLogs.");
        }
    }
}
