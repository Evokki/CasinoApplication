using GambleAssetsLibrary;
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
    /// Interaction logic for SlotsView.xaml
    /// </summary>
    public partial class SlotsView : Window
    {
        public Slots SlotsGame;
        public SlotsView()
        {
            InitializeComponent();

            SlotsGame= new Slots(); 
            SlotsViewModel slotsViewModel = new SlotsViewModel(SlotsGame);
            this.DataContext = slotsViewModel;
        }
    }
}
