using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GambleAssetsLibrary
{
    public class Wheel
    {
        private ListBox WheelLB;
        private DoubleAnimation WheelAnim;
        private DoubleAnimation WheelItemAnim;

        public int[] WheelValues = { 0,1,0,2,0,2, 0,3,0,5};

        public List<RotateTransform> WheelItems = new List<RotateTransform>();
        public RotateTransform WheelRot;
        int Target = 0;
        public event Action WheelStopped;
        public Wheel(ListBox lb, RotateTransform t, List<RotateTransform> l) { 
            WheelLB = lb;
            WheelRot = t;
            WheelItems = l;
            SetupMask();
            CreateAnimations();
        }
        public int GetMultiplier()
        {
            return WheelValues[Target];
        }
        private void SetupMask()
        {
            EllipseGeometry ellipseGeometry = new EllipseGeometry(new Point(WheelLB.ActualWidth / 2, WheelLB.ActualHeight / 2), WheelLB.ActualWidth, WheelLB.ActualHeight);
            GeometryDrawing gd = new GeometryDrawing(Brushes.Red, new Pen(Brushes.Black, 3), ellipseGeometry);
            DrawingBrush brushMask = new DrawingBrush(gd);
            WheelLB.OpacityMask = brushMask;
        }
        private void Set(int target)
        {
            WheelLB.SelectedIndex = target;
            string Rep = "2," + target.ToString();
            double D = Convert.ToDouble(Rep);
            WheelAnim.RepeatBehavior = new RepeatBehavior(D);
            WheelItemAnim.RepeatBehavior = new RepeatBehavior(D);
            
        }
        public void Begin()
        {
            Random rng = new Random();
            Target = rng.Next(0, 9);
            Set(Target);
            SpinWheel();
        }
        private void SpinWheel()
        {
            foreach(RotateTransform item in WheelItems)
            {
                item.BeginAnimation(RotateTransform.AngleProperty, WheelItemAnim);
            }
            WheelRot.BeginAnimation(RotateTransform.AngleProperty, WheelAnim);

        }
        private void OnAnimationEnd(object obj, EventArgs e)
        {
            WheelStopped?.Invoke();
        }
        private void CreateAnimations()
        {
            WheelAnim = new DoubleAnimation();
            WheelAnim.From = 0;
            WheelAnim.To = 360;
            WheelAnim.Duration = new Duration(TimeSpan.Parse("0:0:0:1"));
            WheelAnim.Completed += OnAnimationEnd;

            WheelItemAnim = new DoubleAnimation();
            WheelItemAnim.From = 360;
            WheelItemAnim.To = 0;
            WheelItemAnim.Duration = new Duration(TimeSpan.Parse("0:0:0:1"));

        }
    }
}
