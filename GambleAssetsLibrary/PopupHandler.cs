using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Diagnostics;

namespace GambleAssetsLibrary
{
    public class PopupHandler
    {
        private Popup popup;
        private TextBox textBox;
        private TextBlock textBlock;
        private IPopUpHelper helper;
        public PopupHandler(IPopUpHelper helper, Popup popup, TextBox textBox, TextBlock textBlock)
        {
            this.popup = popup;
            this.textBox = textBox;
            this.textBlock = textBlock;
            this.helper = helper;
        }

        private void ShowPopup()
        {
            popup.StaysOpen = false;
            popup.IsOpen = true;
        }
        public void SetDepositHandler()
        {
            popup.KeyDown -= OnPopupWithdraw;
            popup.KeyDown += OnPopupDeposit;
            textBlock.Text = "Deposit amount?";
            ShowPopup();
        }
        public void SetWithdrawHandler()
        {
            Debug.WriteLine("setting helper W in: " + helper.ToString());
            popup.KeyDown += OnPopupWithdraw;
            popup.KeyDown -= OnPopupDeposit;
            textBlock.Text = "Withdraw amount?";
            ShowPopup();
        }
        public void OnPopupWithdraw(object sender, KeyEventArgs e) {
            Debug.WriteLine("On key click W" + helper.ToString());
            if (e.Key == Key.Enter)
            {
                popup.IsOpen = false;
                if (Decimal.TryParse(textBox.Text, out decimal amount))
                    helper.OnWithdraw(amount);
                textBox.Text = "";
            }
        }
        public void OnPopupDeposit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                popup.IsOpen = false;
                if (Decimal.TryParse(textBox.Text, out decimal amount))
                    helper.OnDeposit(amount);
                textBox.Text = "";
            }
        }
    } 
    public interface IPopUpHelper
    {
        public void OnDeposit(decimal amount);
        public void OnWithdraw(decimal amount);
    }
}
 
