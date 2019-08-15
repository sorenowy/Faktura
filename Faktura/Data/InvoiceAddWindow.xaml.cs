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

namespace Faktura.Data
{
    /// <summary>
    /// Interaction logic for InvoiceAddWindow.xaml
    /// </summary>
    public partial class InvoiceAddWindow : Window
    {
        SqlConnection connection;
        string query = "INSERT INTO InvoiceData VALUES (@InvoiceDate, @Type, @InvoiceNumber, @MoneyValue)";
        public InvoiceAddWindow()
        {
            InitializeComponent();
        }

        private void buttonAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            using (connection = new SqlConnection(LocalParameters.localSqlConnect))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@InvoiceDate", txtInvoiceDate.Text);
                cmd.Parameters.AddWithValue("@Type", txtInvoiceInventory.Text);
                cmd.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNumber.Text);
                cmd.Parameters.AddWithValue("@MoneyValue", txtInvoiceMoneyVal.Text);
                cmd.ExecuteNonQuery();
            }

        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
