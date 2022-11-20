using Practice.Database;
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
        DatabaseEntities database;
        public MainWindow()
        {
            InitializeComponent();

            database = new DatabaseEntities();
            ServicesList.ItemsSource = database.Services_.ToList();

            LoadImages();
        }

        ObservableCollection<Services_> services { get; set; }

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
            ServicesList.ItemsSource = null;
            List<Services_> data = database.Services_.ToList(); ;
            switch (DiscondComboBox.SelectedIndex)
            {
                default:
                    break;
                case -1:
                    data = database.Services_.ToList();
                    break;
                case 0:
                    data = database.Services_.Where(x => x.Discond.Value < 0.05d).ToList();
                    break;
                case 1:
                    data = database.Services_.Where(x => x.Discond.Value > 0.05d && x.Discond.Value < 0.15d).ToList();
                    break;
                case 2:
                    data = database.Services_.Where(x => x.Discond.Value > 0.15d && x.Discond.Value < 0.30d).ToList();
                    break;
                case 3:
                    data = database.Services_.Where(x => x.Discond.Value > 0.30d && x.Discond.Value < 0.70d).ToList();
                    break;
                case 4:
                    data = database.Services_.Where(x => x.Discond.Value > 0.70d && x.Discond.Value < 1d).ToList();
                    break;
            }
            ServicesList.ItemsSource = data;
        }
    }
}
