﻿using System;
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
using System.Data.SqlClient;
using Faktura.Configuration;
using Faktura.Data;

namespace Faktura
{
    /// <summary>
    /// Logika interakcji dla klasy TitleWindow.xaml
    /// </summary>
    public partial class TitleWindow : Window
    {
        public TitleWindow()
        {
            InitializeComponent();
        }

        private void buttonFaktura_Click(object sender, RoutedEventArgs e)
        {
            InvoiceWindow invoiceWin = new InvoiceWindow();
            invoiceWin.ShowDialog();
        }
    }
}
