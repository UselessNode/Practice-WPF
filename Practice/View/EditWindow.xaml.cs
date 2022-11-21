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
        public EditWindow(Services_ service)
        {
            InitializeComponent();
            LoadInputs(service);
        }

        private void LoadInputs(Services_ service)
        {
            ImageBox.Source = GetImageSource(service.ServicesImages.ImagePath);
            LabelId.Content = service.Id;
            TextBoxName.Text = service.ServiceName.ToString();
            TextBoxDuration.Text = service.Duration.ToString();
            TextBoxDiscond.Text = service.Discond.ToString();
            TextBoxCost.Text = service.Cost.ToString();
        }

        private ImageSource GetImageSource(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.Relative);
            bitmap.EndInit();
            return bitmap;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CancelEditing();
        }

        private void CancelEditing()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            CancelEditing();
        }
    }
}
