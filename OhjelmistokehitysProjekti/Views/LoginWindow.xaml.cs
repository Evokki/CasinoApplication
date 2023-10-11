using OhjelmistokehitysProjekti.ViewModels;
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

namespace OhjelmistokehitysProjekti.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LoginViewModel viewModel = new LoginViewModel();
            viewModel.RequestClose += this.Close;
            this.DataContext = viewModel;

            //Sets the window unmovable
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
        }
        public void TextboxEditor(object o, KeyEventArgs e)
        {
            TextBox t = (TextBox)o;
            if (string.IsNullOrEmpty(t.Text))
            {
                t.Text = "Enter text here...";
            }
        }
        public void CloseWindow(object obj, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Exit Application?",
                "Notification", MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }

        }
    }
}
