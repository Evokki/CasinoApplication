using OhjelmistokehitysProjekti.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public abstract class BaseViewModel: INotifyPropertyChanged
    {
        public ICommand CloseWindowCommand { get; set; }
        public event Action RequestClose;
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        public BaseViewModel() {
            CloseWindowCommand = new RelayCommand(CloseWindow, CanExecute);
        }

        public virtual void CloseWindow(object obj)
        {
            if(RequestClose != null)
            {
                RequestClose();
            }
        }

        public virtual bool CanExecute(object obj)
        {
            return true;
        }
    }
}
