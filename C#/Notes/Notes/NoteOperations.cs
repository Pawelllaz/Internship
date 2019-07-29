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
using System.Windows.Controls.Primitives;

namespace Notes
{
    class NoteOperations
    {
        public Grid CreateNewNoteGrid(string title, string textReaded)
        {
            Grid grid = new Grid();
            grid.Margin = new Thickness(5);
            grid.MouseRightButtonDown += MouseClicked;

            TextBlock titleBlock = new TextBlock();
            titleBlock.FontSize = 18;
            titleBlock.FontWeight = FontWeights.Medium;
            titleBlock.Text = title;
            titleBlock.Padding = new Thickness(10,5,5,5);
            titleBlock.Background = Brushes.Yellow;

            TextBlock textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Margin = new Thickness(15,35,10,10);
            textBlock.Text = textReaded;
            textBlock.Background = Brushes.Yellow;

            Popup popup = new Popup();
            popup.Name = "popup";
            popup.AllowsTransparency = true;
            popup.Placement = PlacementMode.Bottom;
           // popup.PopupAnimation = PopupAnimation.Fade;
            //popup.StaysOpen = true;
            
            grid.Children.Add(titleBlock);
            grid.Children.Add(textBlock);
            grid.Children.Add(popup);
            return grid;
        }

        private void MouseClicked(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
