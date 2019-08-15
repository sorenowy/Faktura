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
        string queryRefreshRow = "DBCC CHECKIDENT(orders, RESEED)";
        public InvoiceWindow()
        {
            InitializeComponent();
            try
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT * FROM InvoiceData", connection))
                {
                    connection.Open();
                    DataTable dt = new DataTable();
                    sqladapter.Fill(dt);
                    invoiceGrid.ItemsSource = dt.DefaultView;
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
            using (SqlCommand cmd = new SqlCommand("DBCC CHECKIDENT(InvoiceData, RESEED,@Id)", connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"Id", Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
