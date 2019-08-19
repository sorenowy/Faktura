using System.Windows;
using Faktura.Configuration;
using Faktura.Logs;

namespace Faktura
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            menuImageBox.Source = MainParameters.menuImage;
            MainParameters.Welcome();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            TitleWindow titleWindow = new TitleWindow();
            LocalParameters.username = textboxName.Text;
            LocalParameters.password = passwordBox.Password;
            titleWindow.ShowDialog();
            LogWriter.LogWrite("Zapisano dane logowania do pamięci! " + LocalParameters.username + " / hasło:" + LocalParameters.password);
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LogWriter.LogWrite("Zakończono pracę programu!");
        }
    }
}
