using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KardanoSquare
{
    class stencilHandler
    {
        const int gridLenght = 20;
        public static void Draw(StackPanel stackPanel, int size)
        {
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(gridLenght);
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(gridLenght);

                    
                    grid.RowDefinitions.Add(row);
                    grid.ColumnDefinitions.Add(column);

                    Button button = new Button();
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Click += MainWindow.Button_Click;
                    TextBlock text = new TextBlock();
                    text.FontSize = 16;
                    text.Padding = new Thickness(10);
                    text.Text = "0";
                    button.Content = text;

                    grid.Children.Add(button);
                }
            }
            stackPanel.Children.Add(grid);
        }


    }
}
