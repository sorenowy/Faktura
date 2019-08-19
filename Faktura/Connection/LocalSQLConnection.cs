using System;
using Faktura.Configuration;
using System.Windows;
using System.Data.SqlClient;
using Faktura.Logs;


namespace Faktura.Connection
{
    public class LocalSQLConnection
    {
        public LocalSQLConnection()
        {
            if (LocalParameters.username != string.Empty)
            {
                LocalParameters.username = "Ewa Świderska-Kuszyńska"; // Adding username if nothing was passed towards usernamebox
            }
        }
        public void InitializeLocalConnection()
        {
            try
            {
                using (var connection = new SqlConnection(LocalParameters.localSqlPath))
                using (SqlDataAdapter sqladapter = new SqlDataAdapter(LocalParameters.localSqlSelectQuery, connection))
                {
                    connection.Open();
                    sqladapter.Fill(MainParameters.dataTable);
                    LocalParameters.sumInvoice = Convert.ToDouble(MainParameters.dataTable.Compute("SUM(Kwota)", string.Empty));
                }
                LogWriter.LogWrite("Podłaczono do bazy lokalnej.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogWriter.LogWrite(ex.ToString());
            }
        }
        public void LocalRefreshView()
        {
            MainParameters.dataTable.Clear();
            this.InitializeLocalConnection();
        }
        public void LocalAddRecord()
        {
            using (var connection = new SqlConnection(LocalParameters.localSqlPath))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.localSqlInsertQuery, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@InvoiceDate", LocalParameters.invoiceDate);
                cmd.Parameters.AddWithValue("@Type", LocalParameters.invoiceProductType);
                cmd.Parameters.AddWithValue("@InvoiceNumber", LocalParameters.invoiceNumber);
                cmd.Parameters.AddWithValue("@MoneyValue", LocalParameters.invoiceMoney);
                cmd.ExecuteNonQuery();
            }
        }
        public void LocalDeleteRecord()
        {
            using (var connection = new SqlConnection(LocalParameters.localSqlPath))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.localSqlDeleteQuery, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"Id", LocalParameters.idNumber);
                cmd.ExecuteNonQuery();
                LocalParameters.idNumber = LocalParameters.idNumber - 1;

            }
            using (var connection = new SqlConnection(LocalParameters.localSqlPath))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.localSqlIDCheckQuery, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"Id", LocalParameters.idNumber);
                cmd.ExecuteNonQuery();
            }
            this.LocalRefreshView();
        }
    }
}
