using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KardanoSquare
{
    class StencilHandler
    {
        const double gridLenght = 30;
        object container;
        public StencilHandler(object container)
        {
            this.container = container;
        }

        public void Draw(int size, RoutedEventHandler eventHandler)
        {
            Grid grid = (Grid)container;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {                    
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

                    Button button = new Button();
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Height = gridLenght;
                    button.Width = gridLenght;
                    button.Click += eventHandler;
                    TextBlock textBlock = new TextBlock();
                    textBlock.FontSize = 24;
                    textBlock.Padding = new Thickness(1);
                    textBlock.Text = "0";
                    button.Content = textBlock.Text;

                    grid.Children.Add(button);
                }
            }
            //((Grid)container).Children.Add(grid);
        }

        public void Clean()
        {
            ((Grid)container).Children.Clear();
        }


    }
}
