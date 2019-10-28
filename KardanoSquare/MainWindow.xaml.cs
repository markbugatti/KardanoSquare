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
        static int[,] stencil;
        public MainWindow()
        {
            InitializeComponent();
        }

        public static void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            if((string)button.Content == "0")
            {
                button.Content = "1";
                stencil[row, column] = 1;
            }
            else
            {
                button.Content = "0";
                stencil[row, column] = 0;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (sizeTextBox.Text != "")
            {
                int size = Int32.Parse(sizeTextBox.Text);
                stencilHandler.Draw(stencilStackPanel, size);
                stencil = new int[size, size];
            }
        }
    }
}
