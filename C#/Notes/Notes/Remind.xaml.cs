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
using System.Windows.Shapes;

namespace Notes
{
    /// <summary>
    /// Logika interakcji dla klasy Remind.xaml
    /// </summary>
    public partial class Remind : UserControl
    {
        private bool flag = false;
        private string date;
        private string time;
        private bool ready;

        public Remind()
        {
            InitializeComponent();
            //Thread thread = new Thread(new ThreadStart(Worker));
            //thread.Start();

        }
        // nie mam pomyslu..............
        public void Worker()
        {
            while (flag)
            {
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                {
                    string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
                    string currentTime = DateTime.Now.ToString("HH:mm tt");
                    Console.WriteLine(currentDate + ", " + currentTime + "\n" + date + ", " + time);
                    if (currentDate.Equals(date) && currentTime.Equals(time))
                    {
                        ready = true;
                        StopWork();
                    }
                });
                Thread.Sleep(60000);
            }
        }

        public void showRemind()
        {
            remindPopup.IsOpen = true;
        }

        private void StopWork()
        {
            flag = false;
        }

        public void SetReminder(string dateTime_Date, string dateTime_Time, Grid grid, string title, string text)
        {
            if (!flag)
                flag = true;
            date = dateTime_Date;
            time = dateTime_Time;
            remindTitle.Text = title;
            if (text.Length > 50)
                remindText.Text = String.Format(text.Substring(0, 50) + "...");
            else
                remindText.Text = String.Format(text.Substring(0, text.Length) + "...");

            remindPopup.PlacementTarget = grid;
        }
    }
}
