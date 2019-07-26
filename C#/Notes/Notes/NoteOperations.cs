using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Notes
{
    class NoteOperations
    {
        public Grid CreateNewNoteGrid(string title, string textReaded)
        {
            Grid grid = new Grid();
            //grid.Width = 300;
            //grid.Height = 300;
            //grid.Background = Brushes.Chocolate;
            grid.Margin = new Thickness(5);
            
            /*WrapPanel grid = new WrapPanel();
            grid.Orientation = Orientation.Vertical;
            grid.Width = 100;
            */
            TextBlock titleBlock = new TextBlock();
            //titleBlock.TextWrapping = TextWrapping.Wrap;
            //titleBlock.Margin = new Thickness(10, 10, 10, 10);
            titleBlock.FontSize = 18;
            titleBlock.FontWeight = FontWeights.Medium;
            //textBlock.Text = GetTitleAndText(title, textReaded);
            titleBlock.Text = title;
            titleBlock.Padding = new Thickness(10,5,5,5);
            titleBlock.Background = Brushes.Yellow;

            TextBlock textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Margin = new Thickness(15,35,10,10);
            //textBlock.Text = GetTitleAndText(title, textReaded);
            textBlock.Text = textReaded;
            textBlock.Background = Brushes.Yellow;
            
           // Console.WriteLine(textBlock.Height);
            grid.Children.Add(titleBlock);
            grid.Children.Add(textBlock);
            return grid;
        }

        private string GetTitleAndText(string title, string text)
        {
            return title + "\n\n" + text;
        }
        
    }
}
