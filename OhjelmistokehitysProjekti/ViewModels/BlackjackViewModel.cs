using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public class BlackjackViewModel : BaseGameViewModel
    {
        private Blackjack Game;
        public ICommand HitCommand { get; set; }
        public ICommand StandCommand { get; set; }

        public BlackjackViewModel(Game game) : base(game)
        {
            Game = (Blackjack)game;
            HitCommand = new RelayCommand(HandleHitCommand, CanExecute);
            StandCommand = new RelayCommand(HandleStandCommand, CanExecute);
        }

        private void HandleHitCommand(object obj)
        {
            Console.WriteLine("hit cmd");
            Game.HandleHitOrStand(true);
        }
        private void HandleStandCommand(object obj)
        {
            Console.WriteLine("stand cmd");
            Game.HandleHitOrStand(false);
        }
    }
}
