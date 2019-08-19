using System;
using System.Data;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Faktura.Configuration
{
    internal class MainParameters
    {
        internal static DataTable dataTable = new DataTable();
        internal static BitmapImage menuImage = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Images\image.jpg"));
        internal static void Welcome()
        {
            MessageBox.Show("Witaj w programie FAKTURA 2019, W celu poprawnej pracy programu, upewnij się że posiadasz zainstalowane oprogramowanie Adobe Reader minimalnie w wersji 11.0.","Uwaga",MessageBoxButton.OK, MessageBoxImage.Information);
            //Welcome message + notification about having Adobe Reader for print purpose.
        }
    }
}
