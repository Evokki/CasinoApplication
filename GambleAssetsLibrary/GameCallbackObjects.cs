using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public abstract class GameCallback : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _GameName { get; set; }
        public string GameName
        {
            get { return _GameName; }
            set { _GameName = value; OnPropertyChanged("GameName"); }
        }

        public GameCallback(string gameName)
        {
            GameName = gameName;
        }

        public virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public class GameResult : GameCallback
    {
        private decimal _WinAmount;
        public decimal WinAmount
        {
            get { return _WinAmount; }
            set { _WinAmount = value; OnPropertyChanged("WinAmount"); }
        }
        private decimal _UsedBet;
        public decimal UsedBet
        {
            get { return _UsedBet; }
            set { _UsedBet = value; OnPropertyChanged("UsedBet"); }
        }
        public bool userWon { get; set; }

        //Constructor
        public GameResult(string s, decimal w, decimal b, bool userWon) : base(s)
        {
            this.userWon = userWon;
            this.WinAmount = w;
            this.UsedBet = b;
        }
    }
    public class GameStatus : GameCallback
    {
        public GameStatus(string gameName) : base(gameName)
        {
        }
    }

    public class BlackjackGameStatus : GameStatus
    {
        private ObservableCollection<Card> _HouseCards;
        public ObservableCollection<Card> HouseCards
        {
            get { return _HouseCards; }
            set { _HouseCards = value; OnPropertyChanged("HouseCards"); }
        }
        private ObservableCollection<Card> _UserCards;
        public ObservableCollection<Card> UserCards
        {
            get { return _UserCards; }
            set { _UserCards = value; OnPropertyChanged("UserCards"); }
        }

        public BlackjackGameStatus(string gameName, List<Card> hH, List<Card> uH) : base(gameName)
        {
            HouseCards = new ObservableCollection<Card>(hH);
            UserCards = new ObservableCollection<Card>(uH);
        }
    }
}
