using Practice.Database;
using System;
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

namespace Practice.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        MainWindow MainWindow;
        Services_ currietnService;
        public RegistrationWindow(Services_ service, MainWindow mainWindow)
        {
            InitializeComponent();
            currietnService = service;
            MainWindow = mainWindow;
            LabelServiceName.Content = service.ServiceName.ToString();
            ComboBoxClient.ItemsSource = mainWindow.database.Clients.ToList();
        }

        private void ButtonCansel_Click(object sender, RoutedEventArgs e)
        {
            CloseEditingWindow();
        }

        private void CloseEditingWindow()
        {
            Hide();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
