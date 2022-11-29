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
        public PracticeEntities database { get; set; }
        List<Services_> servicesList;
        public MainWindow()
        {
            InitializeComponent();

            database = new PracticeEntities();
            ReloadList();
        }


        void ReloadList()
        {
            servicesList = database.Services_.ToList();
            ListBoxServices.ItemsSource = servicesList;

            TextBoxSeach.Text = null;
            ButtonResetSearch.Visibility = Visibility.Collapsed;
            ListBoxServices.ItemsSource = database.Services_.ToList();
            Title = $"Подай на 16 - Главное окно | Результатов: {servicesList.Count}";
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

        //public Services_ selectedService { get; set; }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow auth = new AuthWindow();
            if (auth.ShowDialog() == true)
            {
                if (auth.Password == "1111")
                {
                    var selectedService = ((sender as Button).DataContext as Services_);
                    EditWindow editWindow = new EditWindow(selectedService, this);
                    editWindow.Show();
                    Hide();
                }
                else
                    MessageBox.Show($"Неверный код доступа. Доступ воспрещён", "Доступ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow auth = new AuthWindow();
            if (auth.ShowDialog() == true)
            {
                if (auth.Password == "1111")
                {
                    var selectedService = ((sender as Button).DataContext as Services_);
                    if (MessageBox.Show($"Вы уверены, что хотите удалить\n[{selectedService.ServiceName}]?",
                        "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        database.Services_.Remove(selectedService);
                        database.SaveChanges();
                        //ReloadList();
                    }
                }
                else
                    MessageBox.Show($"Неверный код доступа. Доступ воспрещён", "Доступ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow auth = new AuthWindow();
            if (auth.ShowDialog() == true)
            {
                if (auth.Password == "0000")
                {
                    EditWindow editWindow = new EditWindow(this);
                    editWindow.Show();
                    Hide();
                }
                else
                    MessageBox.Show($"Неверный код доступа. Доступ воспрещён", "Доступ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void TextBoxSeach_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            if (!(String.IsNullOrEmpty(input)))
            {
                ButtonResetSearch.Visibility = Visibility.Visible;
                var searchResult = servicesList;
                int resultCount = searchResult.Count(services => services.ServiceName.Contains(input));
                ListBoxServices.ItemsSource = searchResult.Where(services => services.ServiceName.Contains(input)).ToList();
                Title = $"Подай на 16 - Главное окно | Поиск: {input} | Результатов: {resultCount} из {servicesList.Count}";
            }
            else
                ReloadList();
        }

        private void ButtonResetSearch_Click(object sender, RoutedEventArgs e)
        {
            ReloadList();
        }
    }
}
