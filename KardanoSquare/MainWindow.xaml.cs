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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KardanoSquare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] stencil;
        int[,] stencilCopy;
        StencilHandler stencilHandler;
        public MainWindow()
        {
            InitializeComponent();
            stencilHandler = new StencilHandler(stencilContainer);
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            if((string)button.Content == "0")
            {
                button.Content = "1";

                stencil[row, column] = 1;
                HighlightButton(button);
            }
            else
            {
                button.Content = "0";
                stencil[row, column] = 0;
                UnhighlightButton(button);
            }
        }

        private void UnhighlightButton(Button button)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            button.Foreground = new SolidColorBrush(Colors.Black);
        }

        // highlight button with content "1"
        public void HighlightButton(Button button)
        {
            button.Background = new SolidColorBrush(Colors.DarkRed);
            button.Foreground = new SolidColorBrush(Colors.White);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // проверить не пустой ли текст для шифрования
            if (plainTextBox.Text.Length != 0)
            {
                if (sizeTextBox.Text.Length != 0)
                {
                    try
                    {
                        double number;
                        number = Double.Parse(sizeTextBox.Text);
                        if (number % 2 == 0)
                        {
                            int size = Convert.ToInt32(number);
                            if(size * size >= plainTextBox.Text.Length)
                            {
                                stencilHandler.Clean();
                                stencilHandler.Draw(size, Button_Click);
                                stencil = new int[size, size];
                            }
                            else
                            {
                                MessageBox.Show("Матриця мала для цього тексту");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Введіть парне число");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Введіть ціле число");
                    }
                }
                else
                {
                    MessageBox.Show("Введіть число в поле");
                }
            }
            else
            {
                MessageBox.Show("Необхідно ввести відкритий текст");
            }
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
