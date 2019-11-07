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
        int[,] stencilMatrix;
        /// <summary>
        /// матриця, яка показує, чи заповнена клітинка в матриці тексту
        /// </summary>
        bool[,] fillMatrix;
        char[,] textMatrix;
        /// <summary>
        /// розімір матриці
        /// </summary>
        int matrixSize;
        string plainText;
        string encryptedText;
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

                stencilMatrix[row, column] = 1;
                HighlightButton(button);
            }
            else
            {
                button.Content = "0";
                stencilMatrix[row, column] = 0;
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
                plainText = plainTextBox.Text;
                if (sizeTextBox.Text.Length != 0)
                {
                    try
                    {
                        double number;
                        number = Double.Parse(sizeTextBox.Text);
                        if (number % 2 == 0)
                        {
                            matrixSize = Convert.ToInt32(number);
                            if(matrixSize * matrixSize >= plainTextBox.Text.Length)
                            {
                                stencilHandler.Clean();
                                stencilHandler.Draw(matrixSize, Button_Click);
                                stencilMatrix = new int[matrixSize, matrixSize];
                                // перевірити чи ініціалізується масив як false;
                                fillMatrix = new bool[matrixSize, matrixSize];
                                textMatrix = new char[matrixSize, matrixSize];
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
            int textIndex = 0;
            int degree = 0;
            while (degree < 360)
            {
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        if (stencilMatrix[i, j] == 1)
                        {
                            textMatrix[i, j] = plainText[textIndex];
                            fillMatrix[i, j] = true;
                            textIndex++;
                        }
                    }
                }
                // перевернуть трафарет
                TurnStencilRight();
                degree += 90;
            }
            // отримати зашифрований текст
            encryptedText = new string('a', plainText.Length);
            textIndex = 0;
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    if (fillMatrix[i, j] == true)
                    {
                        encryptedText = encryptedText.Remove(textIndex, 1);
                        char character = textMatrix[i, j];
                        encryptedText = encryptedText.Insert(textIndex, character.ToString());
                        textIndex++;
                    }
                }
            }
            encryptedTextBlock.Text = encryptedText;
        }

        private void TurnStencilRight()
        {
            // копія матриці-трафарету, для повертання матриці-трафарету праворуч
            int[,] stencilCopyMatrix;
            stencilCopyMatrix = new int[matrixSize, matrixSize];
            // скопіювати матрицю трафарет у додаткову матрицю
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    stencilCopyMatrix[i, j] = stencilMatrix[i, j];
                }
            }
            int k = matrixSize - 1;
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    int digit = stencilCopyMatrix[i, j];
                    stencilMatrix[j, k] = digit;
                }
                k--;
            }
        }

        private void plainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textLengthTextBlock != null)
            {
                int digit = Int32.Parse(textLengthTextBlock.Text);
                //int mod = digit % 4;
                // при делении округлить в большую сторону
                digit /= 4;
                //digit += mod;
                squereToSelectTextBlock.Text = digit.ToString();
            }
        }
    }
}
