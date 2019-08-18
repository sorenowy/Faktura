using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faktura.Configuration;
using System.Data.SqlClient;
using Faktura.Data;

namespace Faktura.Connection
{
    public class LocalSQLConnection
    {
        public LocalSQLConnection()
        {
            new SqlConnection();
        }
        public void InitializeLocalConnection()
        {
            using (var _connection = new SqlConnection(LocalParameters.localSqlPath))
            using (SqlDataAdapter sqladapter = new SqlDataAdapter(LocalParameters.localSqlSelectQuery, _connection))
            {
                _connection.Open();
                sqladapter.Fill(MainParameters.dataTable);
                LocalParameters.sumInvoice = Convert.ToDouble(MainParameters.dataTable.Compute("SUM(Kwota)", string.Empty));
            }
        }
        public void LocalRefreshView()
        {
            MainParameters.dataTable.Clear();
            this.InitializeLocalConnection();
        }
        public void LocalAddRecord()
        {
            using (var _connection = new SqlConnection(LocalParameters.localSqlPath))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.localSqlInsertQuery, _connection))
            {
                _connection.Open();
                cmd.Parameters.AddWithValue("@InvoiceDate", LocalParameters.invoiceDate);
                cmd.Parameters.AddWithValue("@Type", LocalParameters.invoiceProductType);
                cmd.Parameters.AddWithValue("@InvoiceNumber", LocalParameters.invoiceNumber);
                cmd.Parameters.AddWithValue("@MoneyValue", LocalParameters.invoiceMoney);
                cmd.ExecuteNonQuery();
            }
        }
        public void LocalDeleteRecord()
        {
            using (var _connection = new SqlConnection(LocalParameters.localSqlPath))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.localSqlDeleteQuery, _connection))
            {
                _connection.Open();
                cmd.Parameters.AddWithValue(@"Id", LocalParameters.idNumber);
                cmd.ExecuteNonQuery();
                LocalParameters.idNumber = LocalParameters.idNumber - 1;

            }
            using (var _connection = new SqlConnection(LocalParameters.localSqlPath))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.localSqlIDCheckQuery, _connection))
            {
                _connection.Open();
                cmd.Parameters.AddWithValue(@"Id", LocalParameters.idNumber);
                cmd.ExecuteNonQuery();
            }
            this.LocalRefreshView();
        }
    }
}
