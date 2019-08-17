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
        internal static bool netconnection = false;
        internal static SqlConnection sqlConnect = null;
        internal static string localSqlConnect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dzik\source\repos\Faktura\Faktura\Data\Invoice.mdf;Integrated Security=True";
        internal static string username = string.Empty;
        internal static string password = string.Empty;
        internal static string loggingPath = Environment.CurrentDirectory + "\\Logs\\ProgramLogs\\";
        internal static string printPDFPath = Environment.CurrentDirectory + "\\Logs\\PDFPrints\\";
        internal static string pdfFile = Environment.CurrentDirectory + "\\Logs\\PDFPrints\\" + DateTime.Now.ToShortDateString() + ".pdf";
        internal static string logoPath = @"C:\Users\Dzik\source\repos\Faktura\Faktura\Images\image.jpg";
        internal static double sumInvoice = 0;
    }
}
