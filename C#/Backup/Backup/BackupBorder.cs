﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Backup
{
    class BackupBorder
    {
        private Border border;

        public BackupBorder()
        {
            border = new Border();
            border.Height = 50;
            border.Background = (Brush)new BrushConverter().ConvertFrom("#CAD7E6");
            border.Padding = new Thickness(10, 3, 10, 3);
            border.Margin = new Thickness(0, 0, 0, 10);
            border.CornerRadius = new CornerRadius(10);
        }

        public void SetText(string text)
        {
            // do something with that texts

            //Grid grid = new Grid();
            //border.Child = grid;
            
            TextBlock textBlock = new TextBlock();
            textBlock.FontFamily = new FontFamily("Comic Sans MS, Verdana");
            textBlock.FontStyle = FontStyles.Italic;
            //textBlock.Margin = new Thickness(0, 0, 80, 0);
            textBlock.FontSize = 18;
            textBlock.Text = text;
            textBlock.Foreground = (Brush)new BrushConverter().ConvertFrom("#003776");
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;

            border.Child = textBlock;
            //grid.Children.Add(textBlock);

            //Button button = new Button();
            //button.Width = 70;
            //button.Height = 35;
            //button.Padding = new Thickness(0);
            //button.VerticalAlignment = VerticalAlignment.Center;
            //button.HorizontalAlignment = HorizontalAlignment.Right;
            //button.Content = "Wykonaj";

            //grid.Children.Add(button);
        }

        public Border GetBorder()
        {
            return border;
        }
    }
}
