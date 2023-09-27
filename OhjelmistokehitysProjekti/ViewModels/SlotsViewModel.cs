using GambleAssetsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhjelmistokehitysProjekti.ViewModels
{
    internal class SlotsViewModel : BaseGameViewModel
    {
        public Slots CurrentGame;
        public GameStatus CurrentStatus;
        public SlotsViewModel(Game game) : base(game)
        {
            CurrentGame = (Slots)game;
        }

        public override void HandleGameStatus(GameCallback res)
        {
            base.HandleGameStatus(res);
            CurrentStatus = (GameStatus)res;
            ChangeGameState(2);
        }
    }
}
