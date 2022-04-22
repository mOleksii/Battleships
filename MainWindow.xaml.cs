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
        List<Button> userShips = new List<Button>();
        List<Button> opponentShips = new List<Button>();
        Random rng = new Random();
        const int TOTAL_AMOUNT_CELLS = 100;
        int ship_size = 5;
        int size_counter = 0, repeatingShipCounter = 0;

        public MainWindow()
        {
            InitializeComponent();

            CreateBoards();
        }

        private void OpponentButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CreateBoards()
        {
            Button test = new Button();
            userShips.Add(test);

            for (int i = 0; i < 100; i++)
            {
                Button userButton = new Button();
                Button opponentButton = new Button();
                userButton.Opacity = 0.7;
                opponentButton.Opacity = 0.7;

                opponentButton.Click += OpponentButton_Click;
                userButton.Click += UserButton_Click;

                userShips.Add(userButton);
                userBoard.Children.Add(userButton);

                opponentShips.Add(opponentButton);
                opponentBoard.Children.Add(opponentButton);
            }

            InitializeShips();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(Button userButton in userBoard.Children)
            {
                if(userButton == (Button)sender)
                {
                    if (ship_size == 5)
                    {
                        userButton.Content = "*****";
                        userButton.IsEnabled = false;
                        size_counter++;

                        if(size_counter == ship_size)
                        {
                            ship_size = 4;
                            size_counter = 0;
                        }    
                    }
                    else if (ship_size == 4)
                    {
                        userButton.Content = "****";
                        userButton.IsEnabled = false;
                        size_counter++;

                        if (size_counter == ship_size)
                        {
                            ship_size = 3;
                            size_counter = 0;
                        }
                    }
                    else if (ship_size == 3)
                    {
                        userButton.Content = "***";
                        userButton.IsEnabled = false;
                        size_counter++;

                        if (size_counter == ship_size)
                        {
                            repeatingShipCounter++;
                            size_counter = 0;

                            if(repeatingShipCounter == 2)
                                ship_size = 2;
                        }
                    }
                    else if (ship_size == 2)
                    {
                        userButton.Content = "**";
                        userButton.IsEnabled = false;
                        size_counter++;

                        if(size_counter == ship_size)
                        {
                            foreach(Button currentBtn in userBoard.Children)
                                currentBtn.IsEnabled = false;
                        }
                    }
                }
            }
        }

        private void InitializeShips()
        {
            bool goodIndex = true;
            int random_index;
            //Carrier ships - 5 cells
            do
            {
                random_index = rng.Next(1, userShips.Count);

                if (random_index % 10 == 5 || random_index % 10 == 6 || random_index % 10 == 7 || random_index % 10 == 8 || random_index % 10 == 9 || random_index % 10 == 0)
                    goodIndex = false;
                else
                    goodIndex = true;

            } while (!goodIndex);

            for (int i = 0; i < 5; i++)
            {
                userShips[random_index].Content = "*****";
                userShips.RemoveAt(random_index);
            }

            do
            {
                random_index = rng.Next(1, userShips.Count);

                if(random_index % 10 == 6 || random_index % 10 == 7 || random_index % 10 == 8 || random_index % 10 == 9 || random_index % 10 == 0)
                    goodIndex=false;
                else
                    goodIndex=true;

            } while (!goodIndex);

            for(int i = 0; i < 4; i++)
            {
                userShips[random_index].Content = "****";
                userShips.RemoveAt(random_index);
            }

            for(int i = 0; i <2; i ++)
            {
                do
                {
                    random_index = rng.Next(1, userShips.Count);

                    if (random_index % 10 == 7 || random_index % 10 == 8 || random_index % 10 == 9 || random_index % 10 == 0)
                        goodIndex = false;
                    else
                        goodIndex = true;

                } while (!goodIndex);

                for(int j = 0; j < 3; j++)
                {
                    userShips[random_index].Content = "***";
                    userShips.RemoveAt(random_index);
                }
            }

            do
            {
                random_index = rng.Next(1, userShips.Count);

                if (random_index % 10 == 8 || random_index % 10 == 9)
                    goodIndex = false;
                else
                    goodIndex = true;

            }while(!goodIndex);

            for(int i = 0; i < 2; i++)
            {
                userShips[random_index].Content = "**";
                userShips.RemoveAt(random_index);
            }
        }
    }
}
