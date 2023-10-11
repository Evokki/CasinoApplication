using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WheelOfFortune
{
    
    public partial class MainWindow : Window
    {
        private bool isSpinning = false;
        private Random random = new Random();
        
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        

        private void OnStartSpinningButtonClick(object sender, RoutedEventArgs e)
        {
            if (isSpinning)
            {
                StopSpinning();
                isSpinning = false;
            }
            else
            {
                StartSpinning();
                isSpinning = true;
            }
        }

        private void StartSpinning()
        {
            double rotationAngle = random.Next(2000, 4000);
            

            

            RotateTransform rotateTransform = new RotateTransform();
            canvas.RenderTransform = rotateTransform;
            
            DoubleAnimation line1RotationAnimation = new DoubleAnimation
            {
                From = rotationAngle,
                
            Duration = TimeSpan.FromSeconds(7), 
                RepeatBehavior = new RepeatBehavior(1),
                EasingFunction = new QuadraticEase()
            };
            line1.BeginAnimation(RotateTransform.AngleProperty, line1RotationAnimation);
            line1RotationAnimation.Completed += lineRotationAnimation_Completed;
            


            DoubleAnimation line2RotationAnimation = new DoubleAnimation
            {
                From = rotationAngle,
                
                Duration = TimeSpan.FromSeconds(7), 
                RepeatBehavior = new RepeatBehavior(1),
                EasingFunction = new QuadraticEase()
            };
            line2.BeginAnimation(RotateTransform.AngleProperty, line2RotationAnimation);
            line2RotationAnimation.Completed += lineRotationAnimation_Completed;
            

            DoubleAnimation line3RotationAnimation = new DoubleAnimation
            {
                From = rotationAngle,
                
                Duration = TimeSpan.FromSeconds(7),
                RepeatBehavior = new RepeatBehavior(1),
                EasingFunction = new QuadraticEase()
            };
            line3.BeginAnimation(RotateTransform.AngleProperty, line2RotationAnimation);
            line3RotationAnimation.Completed += lineRotationAnimation_Completed;
            

            DoubleAnimation line4RotationAnimation = new DoubleAnimation
            {
                From = rotationAngle,
               
                Duration = TimeSpan.FromSeconds(7),
                RepeatBehavior = new RepeatBehavior(1),
                EasingFunction = new QuadraticEase()
            };
            line4.BeginAnimation(RotateTransform.AngleProperty, line2RotationAnimation);
            line4RotationAnimation.Completed += lineRotationAnimation_Completed;
            

        }

        private void lineRotationAnimation_Completed(object sender, EventArgs e)
        {
            
            RotateTransform rotateTransform = (RotateTransform)canvas.RenderTransform;
            double angle = rotateTransform.Angle % 360;


            
            string selectedOption = GetSelectedOption(angle);
            MessageBox.Show("Voittosi on: " + selectedOption, "Tulos");
        }
        private string GetSelectedOption(double angle)
        {
           /// kesken, pitäis jotenki saada lohkot vastaa tulosta *thinking emoji*
            if (angle < 0 || angle >= 90)
                return "1";
            else if (angle >= 90 && angle < 180)
                return "2";
            else if (angle >= 180 && angle < 270)
                return "3";
            else
                return "4";
        }
        private void StopSpinning()
        {
            line1.BeginAnimation(RotateTransform.AngleProperty, null);
            line2.BeginAnimation(RotateTransform.AngleProperty, null);
            line3.BeginAnimation(RotateTransform.AngleProperty, null);
            line4.BeginAnimation(RotateTransform.AngleProperty, null);

            
        }
    }
}





