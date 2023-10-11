using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OhjelmistokehitysProjekti.Views
{
    /// <summary>
    /// Interaction logic for SlotsView.xaml
    /// </summary>
    public partial class SlotsView : Window, IPopUpHelper
    {
        public Slots SlotsGame;
        private SlotsViewModel slotsViewModel;
        private PopupHandler _PopupHandler;
        private Wheel wheel;
        private List<RotateTransform> list = new List<RotateTransform>();
        public SlotsView()
        {
            InitializeComponent();

            SlotsGame= new Slots(); 
            slotsViewModel = new SlotsViewModel(SlotsGame);
            this.DataContext = slotsViewModel;

            _PopupHandler = new PopupHandler(this, UserBalancePopup, PopupTextBox, PopupTitle);
            MainWindow._UserViewModel.OnDeposit += _PopupHandler.SetDepositHandler;
            MainWindow._UserViewModel.OnWithdraw += _PopupHandler.SetWithdrawHandler;

            UserPanel.DataContext = MainWindow._UserViewModel;
            CreateRotList();
            wheel = new Wheel(WheelLB, WheelRotation, list);
            wheel.WheelStopped += OnWheelResult;
            Wheel.Visibility = Visibility.Collapsed;
        }
        private void CreateRotList()
        {
            list.Add(Rot0);
            list.Add(Rot1);
            list.Add(Rot2);
            list.Add(Rot3);
            list.Add(Rot4);
            list.Add(Rot5);
            list.Add(Rot6);
            list.Add(Rot7);
            list.Add(Rot8);
            list.Add(Rot9);
        }
        public void OnWheelResult()
        {
            int T = wheel.GetMultiplier();
            GameResult g = slotsViewModel.gameResult;
            if (T > 0)
            {
                g.WinAmount = g.WinAmount * T;
            }
            else
            {
                g.userWon = false;
                g.WinAmount = 0;

                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                timer.Start();
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    HideWheel(null, null);
                };
            }
            slotsViewModel.HandleGameResult(g);


        }
        public void HideWheel(object o, RoutedEventArgs e)
        {
               Wheel.Visibility = Visibility.Collapsed;
        }
        private void ShowWheel(object sender, RoutedEventArgs e)
        {
            Wheel.Visibility = Visibility.Visible;
            wheel.Begin();
        }

        public void OnDeposit(decimal amount)
        {
            MainWindow._UserViewModel.ChangeUserMoney(true, amount);
        }

        public void OnWithdraw(decimal amount)
        {
            MainWindow._UserViewModel.ChangeUserMoney(false, amount);
        }

        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
