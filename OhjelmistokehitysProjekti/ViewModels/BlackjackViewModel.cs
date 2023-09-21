using GambleAssetsLibrary;
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
        public ICommand HitCommand { get; set; }
        public ICommand StandCommand { get; set; }
        public BlackjackViewModel(Game game) : base(game)
        {

        }

        private void HandleHitCommand(object obj)
        {

        }
        private void HandleStandCommand(object obj)
        {

        }
    }
}
