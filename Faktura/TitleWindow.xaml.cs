using System;
using System.Windows;
using Faktura.Configuration;
using Faktura.Data;
using Faktura.Logs;
using Faktura.Connection;

namespace Faktura
{
    /// <summary>
    /// Logika interakcji dla klasy TitleWindow.xaml
    /// </summary>
    public partial class TitleWindow : Window
    {
        public TitleWindow()
        {
            InitializeComponent();
        }

        private void buttonFaktura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServerSQLConnection connection = new ServerSQLConnection();
                connection.InitializeConnection();
                LocalParameters.netconnection = true;
                LogWriter.LogWrite("Podłączono do bazy FakturaSQL na serwerze KWP");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się połaczyć z bazą danych!","Błąd",MessageBoxButton.OK,MessageBoxImage.Warning);
                LogWriter.LogWrite(ex.ToString());
                LocalSQLConnection connection = new LocalSQLConnection();
                connection.InitializeLocalConnection();
                LocalParameters.netconnection = false;
                LogWriter.LogWrite("Bład podłaczenia do bazy danych.." + ex.ToString());
            }
            InvoiceWindow invoiceWin = new InvoiceWindow();
            invoiceWin.ShowDialog();
        }
    }
}
