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
        private List<RotateTransform> WheelList = new List<RotateTransform>();
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
            CreateAnimList();
            wheel = new Wheel(WheelLB, WheelRotation, WheelList);
            wheel.WheelStopped += OnWheelResult;
            Wheel.Visibility = Visibility.Collapsed;

        }
        private void CreateAnimList() //Populates lists with animated elements
        {
            WheelList.Add(Rot0);
            WheelList.Add(Rot1);
            WheelList.Add(Rot2);
            WheelList.Add(Rot3);
            WheelList.Add(Rot4);
            WheelList.Add(Rot5);
            WheelList.Add(Rot6);
            WheelList.Add(Rot7);
            WheelList.Add(Rot8);
            WheelList.Add(Rot9);
        }

        #region Wheel Animation functions
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
        #endregion

        #region UserViewModel Functions
        public void OnDeposit(decimal amount)
        {
            MainWindow._UserViewModel.ChangeUserMoney(true, amount);
        }

        public void OnWithdraw(decimal amount)
        {
            MainWindow._UserViewModel.ChangeUserMoney(false, amount);
        }
        #endregion

        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}