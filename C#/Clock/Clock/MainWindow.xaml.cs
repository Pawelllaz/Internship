using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Clock
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        private bool flag = true;
        private bool Hflag = true;
        public double currentMin = 0;
        private double currentH = 0;

        public double HfromAngle { get; set; }
        public double MinuteAngle { get; set; }
        public double SecondAngle { get; set; }
        public double HAngle { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
            SetMinAngle();
            SetHAngle();
            SetSecAngle();
            DataContext = this;

            Thread thread = new Thread(new ThreadStart(Worker));
            thread.Start();
        }

        private void SetSecAngle()
        {
            SecondAngle = DateTime.Now.Second*6;
            SecondAngle += (double)DateTime.Now.Millisecond/174;
        }
        
        private void SetHAngle()
        {
            currentH = DateTime.Now.Hour;
            if (DateTime.Now.Hour > 11)
                currentH -= 12;
            HAngle = currentH * 30;
            HAngle += DateTime.Now.Minute / 2;
            Console.WriteLine(HAngle);
        }

        private void SetMinAngle()
        {
            currentMin = DateTime.Now.Minute;
            MinuteAngle = currentMin * 6;
        }

        private void Worker()
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
            {
                SetMinAngle();
            });

            while (flag)
            {
                
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                {
                    SetSecAngle();
                    secPointer.Angle = SecondAngle;

                    if(DateTime.Now.Second%10 == 0)
                    {
                        var resourceBigSprocket = myWindow.Resources["biSprocketAnimation"] as Storyboard;
                        resourceBigSprocket?.Begin();
                    }
                    if(DateTime.Now.Second%15 == 0)
                    {
                        var resourceRightSprocket = myWindow.Resources["rightSprocketAnimation"] as Storyboard;
                        resourceRightSprocket?.Begin();
                    }

                    if (DateTime.Now.Minute != currentMin)
                    { 
                        SetMinAngle();
                        var resourceMinute = myWindow.Resources["minutePointerAnimation"] as Storyboard;
                        resourceMinute?.Begin();
                    }
                    if (DateTime.Now.Minute%10 == 0)
                    {
                        if (Hflag)
                        {
                            HfromAngle = HAngle;
                            SetHAngle();
                            var resourceH = myWindow.Resources["hPointerAnimation"] as Storyboard;
                            resourceH?.Begin();
                            Hflag = false;
                        }
                    }
                    else
                    {
                        Hflag = true;
                    }
                });
                Thread.Sleep(150);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            flag = false;
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
                Application.Current.Shutdown();
        }
        
    }
}
