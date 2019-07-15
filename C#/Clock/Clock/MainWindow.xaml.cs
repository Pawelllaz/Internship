using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clock
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool flag = true;

        public MainWindow()
        {
            InitializeComponent();

            line1.Angle = 180;

            Thread thread = new Thread(new ThreadStart(worker));
            thread.Start();


        }

        private double setAngle()
        {
            double angle = 0;

            angle = DateTime.Now.Second*6;
            angle += (double)DateTime.Now.Millisecond/175;
            //Console.WriteLine(angle);
            return angle;
        }

        private void worker()
        {
            while(flag)
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate {
                    line1.Angle = setAngle();
                    Thread.Sleep(30);
                });


                /*Dispatcher.Invoke(() =>
                {
                    line1.Angle = DateTime.Now.Second;
                });*/

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            flag = false;
        }
    }
}
