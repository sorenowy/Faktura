using System;
using System.Data.SqlClient;
using Faktura.Configuration;

namespace Faktura.Connection
{
    public class ServerSQLConnection
    {
        SqlCredential credential;
        public ServerSQLConnection()
        {
        }
        public void InitializeConnection()
        {
            var securePassword = new System.Security.SecureString();
            foreach (var character in LocalParameters.password)
            {
                securePassword.AppendChar(character);
            }
            securePassword.MakeReadOnly();
            credential = new SqlCredential(LocalParameters.username, securePassword);
            using (var connection = new SqlConnection(LocalParameters.serverSqlPath, credential))
            using (SqlDataAdapter sqladapter = new SqlDataAdapter(LocalParameters.serverStoredProcedureSelect, connection))
            {
                connection.Open();
                sqladapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqladapter.Fill(MainParameters.dataTable);
                LocalParameters.sumInvoice = Convert.ToDouble(MainParameters.dataTable.Compute("SUM(Kwota)", string.Empty));
            }
        }
        public void RefreshView()
        {
            MainParameters.dataTable.Clear();
            this.InitializeConnection();
        }
        public void AddRecord()
        {
            var securePassword = new System.Security.SecureString();
            foreach (var character in LocalParameters.password)
            {
                securePassword.AppendChar(character);
            }
            securePassword.MakeReadOnly();
            credential = new SqlCredential(LocalParameters.username, securePassword);
            using (var connection = new SqlConnection(LocalParameters.serverSqlPath, credential))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.serverStoredProcedureInsert, connection))
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceDate",LocalParameters.invoiceDate);
                cmd.Parameters.AddWithValue("@InvoiceType", LocalParameters.invoiceProductType);
                cmd.Parameters.AddWithValue("@InvoiceNumber", LocalParameters.invoiceNumber);
                cmd.Parameters.AddWithValue("@InvoiceMoney", LocalParameters.invoiceMoney);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteRecord()
        {
            var securePassword = new System.Security.SecureString();
            foreach (var character in LocalParameters.password)
            {
                securePassword.AppendChar(character);
            }
            securePassword.MakeReadOnly();
            credential = new SqlCredential(LocalParameters.username, securePassword);
            using (var connection = new SqlConnection(LocalParameters.serverSqlPath, credential))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.serverStoredProcedureDelete, connection))
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(@"Id", LocalParameters.idNumber);
                cmd.ExecuteNonQuery();
                LocalParameters.idNumber=LocalParameters.idNumber-1;

            }
            using (var connection = new SqlConnection(LocalParameters.serverSqlPath, credential))
            using (SqlCommand cmd = new SqlCommand(LocalParameters.serverStoredProcedureIDCheck, connection))
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(@"Id", LocalParameters.idNumber-1);
                cmd.ExecuteNonQuery();
            }
            this.RefreshView();
        }
    } 
}
