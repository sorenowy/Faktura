using System;
using System.Windows;
using System.Diagnostics;
using Faktura.Connection;
using Faktura.Output;
using Faktura.Configuration;
using Faktura.Logs;

namespace Faktura.Data
{
    /// <summary>
    /// Logika interakcji dla klasy InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        public InvoiceWindow()
        {
            InitializeComponent();
            try
            {
                invoiceGrid.ItemsSource = MainParameters.dataTable.DefaultView;
                invoiceGrid.ColumnWidth = 155;
                txtsumInvoice.Text = Convert.ToString(LocalParameters.sumInvoice) + " PLN";
                LogWriter.LogWrite("Wyświetlono tabelę bazy danych faktur");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                LogWriter.LogWrite(e.ToString());
            }
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            InvoiceAddWindow adwin = new InvoiceAddWindow();
            adwin.ShowDialog();
        }
        private void butttonRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LocalParameters.idNumber = invoiceGrid.SelectedIndex + 1;
                if (LocalParameters.netconnection == true)
                {
                    ServerSQLConnection connection = new ServerSQLConnection();
                    connection.DeleteRecord();
                    LogWriter.LogWrite($"Usunięto rekord o ID={LocalParameters.idNumber} z bazy SQLServer");
                }
                else if (LocalParameters.netconnection == false)
                {
                    LocalSQLConnection connection = new LocalSQLConnection();
                    connection.LocalDeleteRecord();
                    LogWriter.LogWrite($"Usunięto rekord o ID={LocalParameters.idNumber} z bazy Lokalnej");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogWriter.LogWrite(ex.ToString());
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
            try
            {
                PDFPrinter print = new PDFPrinter();
                print.CreatePDF();
                print.PrintPDF();
                LogWriter.LogWrite("Wygenerowano wydruk wyjściowy faktury.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogWriter.LogWrite("Błąd wydruku!" + ex.ToString());
            }

        }

        private void buttonHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto:" + "hubert.kuszynski@go.policja.gov.pl" + "?subject=" + "Problem z programem Faktura 2019" + "&body="
                + "Proszę o załączenie loga programu z katalogu Logs/ProgramLogs.");
        }
    }
}
