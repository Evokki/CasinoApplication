﻿using System;
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
using OhjelmistokehitysProjekti.ViewModels;
using OhjelmistokehitysProjekti.Views;

namespace OhjelmistokehitysProjekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window? gameView;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            MainViewModel mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;

            Loaded += ValidateCurrentUser;
        }

        public void ClearGameView(object? sender, EventArgs e) {
            gameView.Closed -= ClearGameView;
            gameView = null;
        }

        public void StartBlackjack(object sender, RoutedEventArgs e)
        {
            if(gameView == null)
            {
                gameView = new BlackJackView();
                gameView.Closed += ClearGameView;
            }
            gameView.Show();
        }
        public void StartSlots(object sender, RoutedEventArgs e)
        {
            if (gameView == null)
            {
                gameView = new SlotsView();
                gameView.Closed += ClearGameView;
            }
            gameView.Show();
        }
        private void StartPoker(object sender, RoutedEventArgs e)
        {
            if (gameView == null)
            {
                gameView = new PokerView();
                gameView.Closed += ClearGameView;
            }
            gameView.Show();
        }

        private void ValidateCurrentUser(object obj, RoutedEventArgs e)
        {
            if(UserHandler.GetUser() == null)
            {
                this.IsEnabled = false;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                this.IsEnabled = true;
            }
        }

    }
}
