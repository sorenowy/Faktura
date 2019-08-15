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
        internal static SqlConnection sqlConnect = null;
        internal static string localSqlConnect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dzik\source\repos\Faktura\Faktura\Data\Invoice.mdf;Integrated Security=True";
        internal static string username = string.Empty;
        internal static string password = string.Empty;
    }
}
