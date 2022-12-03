using System;
using System.Diagnostics;

namespace SharpSweeperV2
{
    class MinesweeperGame
    {
        const int AsciiOffset = 65;
        private MinesweeperGameGrid Grid;
        private bool GameRunning;

        public MinesweeperGame(MinesweeperGameGrid Grid)
        {
            this.Grid = Grid;
            GameRunning = false;
        }

        public void PlayGame()
        {
            GameRunning = true;
            int[] SelectedTile;
            int ClearedTiles = 0;
            bool ValidInput;
            string Prompt;
            while (GameRunning)
            {
                Console.Clear();
                Grid.DisplayGrid(0);
                Prompt = "Please enter a square or 'E' to exit to menu";
                do
                {
                    Console.Write(Prompt + ":");
                    SelectedTile = GetInput();
                    if (SelectedTile == null) { ValidInput = false; }
                    else if(SelectedTile[0] == -1) 
                    {
                        ValidInput = false;
                        switch(SelectedTile[1])
                        {
                            case -1:
                                Prompt = "Square not on grid please enter a valid square";
                                break;
                            case -2:
                                Console.Clear();
                                Grid.DisplayGrid(2);
                                Console.Write("Press any key to exit to menu");
                                Console.ReadLine();
                                return;
                        }
                    }
                    else { ValidInput = true; Debug.Write("Valid Input Entered\n"); }
                    
                } while (!ValidInput);
                
                if (Grid.Grid[SelectedTile[0], SelectedTile[1]].HasMine)
                {
                    GameOver();
                }
                ClearSquare(SelectedTile[0], SelectedTile[1]);
                if(ClearedTiles >= (Grid.Height * Grid.Width) - Grid.MineCount)
                {
                    GameWon();
                }
            }
            Console.Read();
            void GameWon()
            {
                GameRunning = false;
                Console.Clear();
                Grid.DisplayGrid(2);
                Console.Write("Congratulations!\nYou succesfully found all of the mines!\nPress any key to return to main menu");
            }
            void GameOver()
            {
                GameRunning = false;
                Console.Clear();
                Grid.DisplayGrid(1);
                Console.Write($"Game Over!\nYou it a mine at {(char)(SelectedTile[0] + AsciiOffset)}{SelectedTile[1] + 1}\nPress any key to return to main menu");
            }
            void ClearSquare(int h, int w)
            {
                if (h < 0 || h >= Grid.Height || w < 0 || w >= Grid.Width || Grid.Grid[h,w].Revealed) { return; }
                Debug.Write($"Clearing Square: h{h} w{w}\n");
                ClearedTiles++;
                if (Grid.Grid[h,w].NearbyMines == 0)
                {
                    Grid.Grid[h, w].Reveal();
                    for (int h2 = -1; h2 <= 1; h2++)
                    {
                        for (int w2 = -1; w2 <= 1; w2++)
                        {
                            ClearSquare(h + h2, w + w2);
                        }
                    }
                    return;
                }
                else
                {
                    Grid.Grid[h, w].Reveal();
                    return;
                }
            }
            int[] GetInput()
            {
                try
                {
                    string input = Console.ReadLine().ToUpper().Trim();
                    if (input.Length != 2)
                    {
                        if (input.Length == 0)
                        {
                            Debug.Write("Null Input Entered\n");
                            return null;
                        }
                        else
                        {
                            switch (input)
                            {
                                case "E":
                                    return new int[] { -1, -2 };
                            }
                        }
                    }
                    int h = Convert.ToInt32(input.Substring(1)) - 1;
                    int w = (int)(input[0] - AsciiOffset);
                    Debug.Write($"Selected square: h:{h} w:{w}\n");
                    if (h < 0 || h >= Grid.Height || w < 0 || w >= Grid.Width)
                    {
                        return new int[] { -1 ,-1 };
                    }
                    else
                    {
                        return new int[] { h, w };
                    }
                }
                catch (FormatException)
                {
                    Debug.Write("Invalid input\n");
                    return null;
                }
            }
        }
    }
}