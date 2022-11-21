using Practice.Database;
using Practice.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DatabaseEntities database { get; set; }
        List<Services_> servicesList;
        public MainWindow()
        {
            InitializeComponent();

            database = new DatabaseEntities();
            servicesList = database.Services_.ToList();
            ListBoxServices.ItemsSource = servicesList;

            LoadImages();
        }


        void LoadImages()
        {
            // Добавление?

            //services = new ObservableCollection<Services_>
            //{
            //    new Services_ { Id = }
            //}
        }

        private void DiscondComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (DiscondComboBox.SelectedIndex)
            {
                default:
                    break;
                case -1:
                    servicesList = database.Services_.ToList();
                    break;
                case 0:
                    servicesList = database.Services_.Where(service => service.Discond.Value < 0.05d).ToList();
                    break;
                case 1:
                    servicesList = database.Services_.Where(service => service.Discond.Value > 0.05d && service.Discond.Value < 0.15d).ToList();
                    break;
                case 2:
                    servicesList = database.Services_.Where(service => service.Discond.Value > 0.15d && service.Discond.Value < 0.30d).ToList();
                    break;
                case 3:
                    servicesList = database.Services_.Where(service => service.Discond.Value > 0.30d && service.Discond.Value < 0.70d).ToList();
                    break;
                case 4:
                    servicesList = database.Services_.Where(service => service.Discond.Value > 0.70d && service.Discond.Value < 1d).ToList();
                    break;
            }
            ListBoxServices.ItemsSource = servicesList;
            DiscondComboBox.Width = Double.NaN;
        }

        private void CheckBoxCostSort_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxCostSort.IsChecked == true)
                servicesList = database.Services_.OrderBy(service => service.Cost).ToList();
            else if (CheckBoxCostSort.IsChecked == false)
                servicesList = database.Services_.OrderByDescending(service => service.Cost).ToList();
            else
                servicesList = database.Services_.ToList();
            ListBoxServices.ItemsSource = servicesList;
        }

        public Services_ selectedService { get; set; }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            selectedService = (Services_)ListBoxServices.SelectedItem;
            EditWindow editWindow = new EditWindow(selectedService);
            editWindow.Show();
            Hide();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListBoxServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
