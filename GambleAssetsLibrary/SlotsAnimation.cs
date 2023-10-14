using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GambleAssetsLibrary
{
    public class SlotsAnimation
    {
        List<ListView> Rolls = new List<ListView>();
        DoubleAnimation animation;
        public event Action OnAnimationEnded;
        public SlotsAnimation(List<ListView> rolls)
        {
            Rolls = rolls;
            
            CreateAnimation();
        }
        public void BeginAnimation()
        {
            var timer = new DispatcherTimer();
            int index1 = 0;
            bool reverse = false;
            int spinCOunt = 20;
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                foreach(ListView roll in Rolls)
                {
                    roll.SelectedIndex = index1;
                    roll.ScrollIntoView(roll.SelectedItem);
                }
                if (reverse)
                {
                    index1--;
                    if (index1 == 0)
                    {
                        reverse = false;
                    }
                }
                else
                {
                    index1++;
                    if (index1 == 4)
                    {
                        reverse = true;
                    }

                }
                spinCOunt--;
                if(spinCOunt <= 0)
                {
                    timer.Stop();
                    OnAnimationEnd();
                }
            };
            
        }
        public void OnAnimationEnd()
        {
            foreach (ListView roll in Rolls)
            {
                roll.SelectedIndex = 2;
                roll.ScrollIntoView(roll.Items[3]);
            }
            OnAnimationEnded?.Invoke();
        }
        private void CreateAnimation()
        {
            animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 4;
            animation.Duration = TimeSpan.FromMilliseconds(100);
            animation.RepeatBehavior = new RepeatBehavior(3);
            //animation.Completed += OnAnimationEnd;
        }
    }
}
