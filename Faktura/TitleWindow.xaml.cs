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
using System.Data.SqlClient;
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
                ServerSQLConnection _connection = new ServerSQLConnection();
                _connection.InitializeConnection();
                LocalParameters.netconnection = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się połaczyć z bazą danych!","Błąd");
                LogWriter.LogWrite(ex.ToString());
                LocalSQLConnection _connection = new LocalSQLConnection();
                _connection.InitializeLocalConnection();
                LocalParameters.netconnection = false;
            }
            InvoiceWindow invoiceWin = new InvoiceWindow();
            invoiceWin.ShowDialog();
        }
    }
}
