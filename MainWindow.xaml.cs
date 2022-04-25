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
            foreach(Button currentButton in opponentBoard.Children)
            {
                if (currentButton == (Button)sender && currentButton.IsEnabled == true)
                {
                    if(currentButton.Tag == "Ship")
                    {
                        currentButton.Content = "X";
                        currentButton.Background = Brushes.DarkRed;
                        currentButton.Foreground = Brushes.White;
                        //currentButton.IsEnabled = false;
                        currentButton.IsHitTestVisible = false;
                        //currentButton.Tag = "Shot";
                    }
                    else
                    {
                        currentButton.Content = ".";
                        currentButton.Background = Brushes.White;
                        //currentButton.IsEnabled = false;
                        currentButton.IsHitTestVisible = false;
                    }
                }
            }

            bool shotOnce = true;

            do
            {
                int rnd_index = rng.Next(userShips.Count - 1);

                if (userShips[rnd_index].Tag == "Ship")
                {
                    userShips[rnd_index].Content = "X";
                    userShips[rnd_index].Background = Brushes.DarkRed;
                    userShips[rnd_index].Foreground = Brushes.White;
                    //userShips[rnd_index].IsEnabled = false;
                    userShips[rnd_index].IsHitTestVisible = false;
                    userShips[rnd_index].Tag = "Shot";
                    userShips.RemoveAt(rnd_index);
                    shotOnce = true;
                }
                else if (userShips[rnd_index].Tag != "Ship" && userShips[rnd_index].Tag != "Shot")
                {
                    userShips[rnd_index].Content = ".";
                    userShips[rnd_index].Background = Brushes.White;
                    //userShips[rnd_index].IsEnabled = false;
                    userShips[rnd_index].IsHitTestVisible = false;
                    userShips[rnd_index].Tag = "Shot";
                    userShips.RemoveAt(rnd_index);
                    shotOnce = true;
                }
                else
                    shotOnce = false;

            } while (!shotOnce);
        }

        private void CreateBoards()
        {
            Button test = new Button();
            userShips.Add(test);

            for (int i = 0; i < 100; i++)
            {
                Button userButton = new Button();
                Button opponentButton = new Button();
                opponentButton.IsEnabled = false;

                userButton.Opacity = 0.9;
                opponentButton.Opacity = 0.9;

                opponentButton.Click += OpponentButton_Click;
                userButton.Click += UserButton_Click;

                userShips.Add(userButton);
                userBoard.Children.Add(userButton);

                opponentShips.Add(opponentButton);
                opponentBoard.Children.Add(opponentButton);
            }

            InitializeOpponentShips();
            MessageBox.Show("Place your 5* ship on your board!","Place your ships!",MessageBoxButton.OK,MessageBoxImage.Information);

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
                        userButton.Tag = "Ship";
                        size_counter++;

                        if(size_counter == ship_size)
                        {
                            ship_size = 4;
                            size_counter = 0;
                            MessageBox.Show("Place your 4* ship on your board!", "Place your ships!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }    
                    }
                    else if (ship_size == 4)
                    {
                        userButton.Content = "****";
                        userButton.IsEnabled = false;
                        userButton.Tag = "Ship";
                        size_counter++;

                        if (size_counter == ship_size)
                        {
                            ship_size = 3;
                            size_counter = 0;
                            MessageBox.Show("Place your 3* ship (x2) on your board!", "Place your ships!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else if (ship_size == 3)
                    {
                        userButton.Content = "***";
                        userButton.IsEnabled = false;
                        userButton.Tag = "Ship";
                        size_counter++;

                        if (size_counter == ship_size)
                        {
                            repeatingShipCounter++;
                            size_counter = 0;

                            if (repeatingShipCounter == 2)
                            {
                                ship_size = 2;
                                MessageBox.Show("Place your 2* ship on your board!", "Place your ships!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    else if (ship_size == 2)
                    {
                        userButton.Content = "**";
                        userButton.IsEnabled = false;
                        userButton.Tag = "Ship";
                        size_counter++;

                        if(size_counter == ship_size)
                        {
                            foreach (Button currentBtn in userBoard.Children)
                            {
                                currentBtn.IsEnabled = true;
                                currentBtn.IsHitTestVisible = false;
                            }

                            foreach (Button opponentButton in opponentBoard.Children)
                                opponentButton.IsEnabled = true;

                            MessageBox.Show("You are ready to start attacking the opponent's ships! Press any cell on the opponents board to fire your cannons!", "GAME STARTING!", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }

        private void InitializeOpponentShips()
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
                /*userShips[random_index].Content = "*****";
                userShips.RemoveAt(random_index);
                */
                opponentShips[random_index].Tag = "Ship";
                opponentShips.RemoveAt(random_index);
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
                /*userShips[random_index].Content = "*****";
                 userShips.RemoveAt(random_index);
                */
                opponentShips[random_index].Tag = "Ship";
                opponentShips.RemoveAt(random_index);
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
                    /*userShips[random_index].Content = "*****";
                     userShips.RemoveAt(random_index);
                    */
                    opponentShips[random_index].Tag = "Ship";
                    opponentShips.RemoveAt(random_index);
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
                /*userShips[random_index].Content = "*****";
                 userShips.RemoveAt(random_index);
                */
                opponentShips[random_index].Tag = "Ship";
                opponentShips.RemoveAt(random_index);
            }
        }
    }
}
