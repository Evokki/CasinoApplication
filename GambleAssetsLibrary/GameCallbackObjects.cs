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

    public class SlotsGameStatus: GameStatus
    {
        public int[] Numbers;
        public SlotsGameStatus(string Name, int[] rolledNums): base(Name) 
        {
            
            Numbers = rolledNums;
        }
    }
    public class PokerGameStatus : GameStatus
    {
        private ObservableCollection<Card> _UserCards;
        public ObservableCollection<Card> UserCards
        {
            get { return _UserCards; }
            set { _UserCards = value; OnPropertyChanged("UserCards"); }
        }
        private ObservableCollection<Card> _Stack1;
        public ObservableCollection<Card> Stack1
        {
            get { return _Stack1; }
            set { _Stack1 = value; OnPropertyChanged("Stack1"); }
        }
        private ObservableCollection<Card> _Stack2;
        public ObservableCollection<Card> Stack2
        {
            get { return _Stack2; }
            set { _Stack2 = value; OnPropertyChanged("Stack2"); }
        }
        private Card _Card1;
        public Card Card1
        {
            get { return _Card1; }
            set { _Card1 = value; OnPropertyChanged("Card1"); }
        }
        private Card _Card2;
        public Card Card2
        {
            get { return _Card2; }
            set { _Card2 = value; OnPropertyChanged("Card2"); }
        }

        public PokerGameStatus(string Name, List<Card> hand, List<Card> one, List<Card> two) : base(Name)
        {
            UserCards = new ObservableCollection<Card>(hand);
            Stack1 = new ObservableCollection<Card>(one);
            Stack2 = new ObservableCollection<Card>(two);
            if(one.Count != 0)
            {
                Card1 = one.First();
            }
            if (two.Count != 0)
            {
                Card2 = two.First();
            }
        }
    }
}
