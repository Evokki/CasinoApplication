using GambleAssetsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public class PokerViewModel : BaseGameViewModel
    {
        public Poker CurrentGame;
        public GameStatus CurrentStatus;

        public PokerViewModel(Game game) : base(game)
        {
            CurrentGame = (Poker)game;
        }

        public override void HandleGameStatus(GameCallback res)
        {
            base.HandleGameStatus(res);
            CurrentStatus = (GameStatus)res;
        }
    }
}
