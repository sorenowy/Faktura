using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Faktura.Configuration
{
    internal class LocalParameters
    {
        internal static bool netconnection = true;
        internal static SqlConnection sqlConnect = null;
        internal static string serverSqlPath = "Data Source=DESKTOP-9BU76HS\\SQLEXPRESS;Initial Catalog=Faktura;Integrated Security=False"; 
        internal static string localSqlPath = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dzik\source\repos\Faktura\Faktura\Data\Invoice.mdf;Integrated Security=True";
        internal static string localSqlSelectQuery = "SELECT Id as Numer, InvoiceDate as Data_Faktury, Type as Rodzaj_Sprzetu, InvoiceNumber as Numer_Faktury, MoneyValue as Kwota FROM InvoiceData";
        internal static string localSqlInsertQuery = "INSERT INTO InvoiceData VALUES (@InvoiceDate, @Type, @InvoiceNumber, @MoneyValue)";
        internal static string localSqlDeleteQuery = "DELETE FROM InvoiceData WHERE Id=@Id";
        internal static string localSqlIDCheckQuery = "DBCC CHECKIDENT(InvoiceData, RESEED, @Id)";
        internal static string username = string.Empty;
        internal static string password = string.Empty;
        internal static string serverStoredProcedureSelect = "ShowMeAll";
        internal static string serverStoredProcedureInsert = "InsertNewOne";
        internal static string serverStoredProcedureDelete = "DeleteThatOne";
        internal static string serverStoredProcedureIDCheck = "CheckAndReseed";
        internal static string invoiceDate = string.Empty;
        internal static string invoiceProductType = string.Empty;
        internal static string invoiceNumber = string.Empty;
        internal static string invoiceMoney = string.Empty;
        internal static string loggingPath = Environment.CurrentDirectory + "\\Logs\\ProgramLogs\\";
        internal static string printPDFPath = Environment.CurrentDirectory + "\\Logs\\PDFPrints\\";
        internal static string pdfFile = Environment.CurrentDirectory + "\\Logs\\PDFPrints\\" + DateTime.Now.ToShortDateString() + ".pdf";
        internal static string logoPath = @"C:\Users\Dzik\source\repos\Faktura\Faktura\Images\image.jpg";
        internal static double sumInvoice = 0;
        internal static int idNumber = 0;
    }
}
