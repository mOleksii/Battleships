using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleships
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            for(int i = 0; i<100; i++)
            {
                Button btn = new Button();
                Button btnn = new Button();
                btn.Opacity = 0.7;
                btnn.Opacity = 0.7;
                userBoard.Children.Add(btn);
                opponentBoard.Children.Add(btnn);
            }
        }
    }
}
