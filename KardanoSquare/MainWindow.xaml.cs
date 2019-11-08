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
        /// Матриця, що показує, чи заповнена клітинка в матриці тексту
        /// </summary>
        bool[,] fillMatrix;

        /// <summary>
        /// Матриця, що містить в собі текст, заповнений через матрицю-трафарет
        /// </summary>
        char[,] textMatrix;

        /// <summary>
        /// розімір матриці
        /// </summary>
        int matrixSize;

        /// <summary>
        /// Мінімальна кількість клітинок, які необхідно виділити в матриці-трафареті
        /// </summary>
        int minSelectedCellCount = 0;

        /// <summary>
        /// Фактична кількість клітинок, які були виділені в матриці-трафареті
        /// </summary>
        int practSelectedCellCount = 0;

        /// <summary>
        /// Відкритий текст
        /// </summary>
        string plainText;

        StencilViewHandler stencilHandler;
        MatrixHandler MatrixHandler;
        EncryptionTextHandler EncryptionTextHandler;
        EncryptionHandler EncryptionHandler;
        
        public MainWindow()
        {
            InitializeComponent();
            stencilHandler = new StencilViewHandler(stencilContainer);
            minSelectedCellCount = Int32.Parse(CellsToSelectTextBlock.Text);

            MatrixHandler = new MatrixHandler();
            EncryptionTextHandler = new EncryptionTextHandler();
            EncryptionHandler = new EncryptionHandler(EncryptionTextHandler, MatrixHandler);
        }

        public void StencilButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            if((string)button.Content == "0")
            {
                button.Content = "1";
                stencilMatrix[row, column] = 1;
                HighlightButton(button);
                practSelectedCellCount++;
            }
            else
            {
                button.Content = "0";
                stencilMatrix[row, column] = 0;
                UnhighlightButton(button);
                practSelectedCellCount--;
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

        private void CreateStencilButton_Click(object sender, RoutedEventArgs e)
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
                                stencilHandler.Draw(matrixSize, StencilButton_Click);
                                stencilMatrix = new int[matrixSize, matrixSize];
                                // перевірити чи ініціалізується масив як false;
                                fillMatrix = new bool[matrixSize, matrixSize];
                                textMatrix = new char[matrixSize, matrixSize];
                                encryptButton.IsEnabled = true;
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
            // Якщо виділена необхідна мінімальна або більше кількість клітинок. Щоб в матрицю влізло все повідомлення
            if (practSelectedCellCount >= minSelectedCellCount)
            {
                // тут вызов EncriptionHandler.Encrypt
                encryptedTextBlock.Text = 
                    EncryptionHandler.Encrypt(plainTextBox.Text, stencilMatrix, textMatrix, fillMatrix, matrixSize);

                descryptButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Виділено замало клітинок на трафареті");
            }
        }

        private void plainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textLengthTextBlock != null)
            {
                int digit = Int32.Parse(textLengthTextBlock.Text);
                // при делении округлить в большую сторону
                int mod = digit % 4;
                digit /= 4;
                if(mod > 0)
                {
                    digit++;
                }
                minSelectedCellCount = digit;
                // Кількість клітинок, які необхідно виділити
                CellsToSelectTextBlock.Text = digit.ToString();
            }
        }

        private void descryptButton_Click(object sender, RoutedEventArgs e)
        {
            descryptedTextBlock.Text = 
                EncryptionHandler.Descrypt(plainText, stencilMatrix, textMatrix, fillMatrix, matrixSize);
        }
    }
}