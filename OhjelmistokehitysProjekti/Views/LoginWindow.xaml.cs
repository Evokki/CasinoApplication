using OhjelmistokehitysProjekti.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
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
        LoginViewModel viewModel;
        string UNplaceholder = "Enter username here...";
        string PWplaceholder = "Enter password here...";
        public LoginWindow()
        {
            InitializeComponent();

            viewModel = new LoginViewModel();
            viewModel.RequestClose += this.Close;
            this.DataContext = viewModel;
            UsernameTB.GotKeyboardFocus += OnFocused;
            PasswordTB.GotKeyboardFocus += OnFocused;
            UsernameTB.LostKeyboardFocus += OnLostKBFocus;
            PasswordTB.LostKeyboardFocus += OnLostKBFocus;
            UsernameTB.Text = UNplaceholder;
            PasswordTB.Text = PWplaceholder;
            //Sets the window unmovable
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
        }
        public void OnFocused(object o, KeyboardEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.Username))
            {
                UsernameTB.Text = "";
            }
            if (string.IsNullOrEmpty(viewModel.Password))
            {
                PasswordTB.Text = "";
            }
        }
        public void OnLostKBFocus(object o, KeyboardEventArgs e)
        {
            if (!string.IsNullOrEmpty(UsernameTB.Text) && UsernameTB.Text != UNplaceholder)
            {
                viewModel.UpdateUsername(UsernameTB.Text);
            }
            if (!string.IsNullOrEmpty(PasswordTB.Text) && PasswordTB.Text != PWplaceholder)
            {
                viewModel.UpdatePassword(PasswordTB.Text);
            }
            if (string.IsNullOrEmpty(viewModel.Username))
            {
                UsernameTB.Text = UNplaceholder;
            }
            if (string.IsNullOrEmpty(viewModel.Password))
            {
                PasswordTB.Text = PWplaceholder;
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
