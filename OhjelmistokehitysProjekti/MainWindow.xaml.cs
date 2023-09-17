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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GambleAssetsLibrary;

namespace OhjelmistokehitysProjekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameWindow _GameWindow = new GameWindow();
        private TestWindow _TestWindow = new TestWindow();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void GameButtonClicked(object sender, RoutedEventArgs e)
        {
            _GameWindow.Show();
        }

        public void InitTestWindow(object sender, RoutedEventArgs e)
        {
            _TestWindow.Show();
        }
    }
}
