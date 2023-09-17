using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Game> Games = new ObservableCollection<Game>();
        public ICommand myCommand { get; set; }

        public MainViewModel() {
            myCommand = new CloseWindowCommand(CloseWindow, CanShowWindow);
        }
        public void CloseWindow(object obj)
        {
            
        }
        public bool CanShowWindow(object obj)
        {
            return true;
        }
    }
}
