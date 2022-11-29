using Microsoft.Win32;
using Practice.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        MainWindow MainWindow;
        Services_ currietnService;
        public EditWindow(Services_ service, MainWindow mainWindow)
        {
            InitializeComponent();
            currietnService = service;
            DataContext = currietnService;            
            MainWindow = mainWindow;
        }

        public EditWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            currietnService = new Services_();
            DataContext = currietnService;
            MainWindow = mainWindow;
            LabelId.Visibility = Visibility.Collapsed;
        }

        //private void LoadInputs(Services_ service)
        //{
        //    //ImageBox.Source = GetImageSource(service.ServicesImages.ImagePath);
        //    LabelId.Content = service.Id;
        //    TextBoxName.Text = service.ServiceName.ToString();
        //    TextBoxDuration.Text = service.Duration.ToString();
        //    TextBoxDiscond.Text = service.Discond.ToString();
        //    TextBoxCost.Text = service.Cost.ToString();
        //}

        //private ImageSource GetImageSource(string imagePath)
        //{
        //    BitmapImage bitmap = new BitmapImage();
        //    bitmap.BeginInit();
        //    bitmap.UriSource = new Uri(imagePath, UriKind.Relative);
        //    bitmap.EndInit();
        //    return bitmap;
        //}

        private void Window_Closed(object sender, EventArgs e)
        {
            CloseEditingWindow();
        }

        private void CloseEditingWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            CloseEditingWindow();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            
            if(currietnService.Id == 0)
                MainWindow.database.Services_.Add(currietnService);

            MainWindow.database.SaveChanges();
            MessageBox.Show("Операция выполнена успешно");
            CloseEditingWindow();
        }

        //private void UpdateService()
        //{
        //    Services_ service = new Services_();
        //    service.Id = currietnService.Id;
        //    service.ServiceName = TextBoxName.Text;
        //    service.Cost = Decimal.Parse(TextBoxCost.Text);
        //    service.Duration = int.Parse(TextBoxDuration.Text);
        //    service.Discond = Double.Parse(TextBoxDiscond.Text);
        //    //service.ServicesImages.ImagePath = service.
        //    MainWindow.database.Services_.Add(service);
        //    MainWindow.database.SaveChanges();
        //    CloseEditingWindow();
        //}

        private void ImageBrowseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
