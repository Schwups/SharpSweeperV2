using System;

namespace SharpSweeperV2
{
    class MineSweeper
    {
        private int GridHeight, GridWidth, MineCount, RandomSeed;

        public MineSweeper()
        {
            GridHeight = GridWidth = 16;
            MineCount = 40;
            RandomSeed = -1;
            MainMenu();
        }
        private void MainMenu()
        {
            string prompt = "Please enter a selection";
            while (true)
            {
                Console.Clear();
                ShowMenuDialogue(); ;
                switch (GetMenuInput())
                {
                    case 'P':
                        new MinesweeperGame(new MinesweeperGameGrid(MineCount, GridWidth, GridHeight, RandomSeed)).PlayGame();
                        prompt = "Please enter a selection";
                        break;
                    case 'D':
                        DifficultyMenu();
                        prompt = "Please enter a selection";
                        break;
                    case 'E':
                        Console.Clear();
                        Console.Write("Press any key to exit");
                        Console.ReadLine();
                        return;
                    case '!':
                        prompt = "Invalid selection";
                        break;
                    default:
                        prompt = "Invalid selection";
                        break;
                }
            }
            void ShowMenuDialogue()
            {
                Console.Write(" __           _   _  __         _  _  _   _  _ \n" +
                             @"(_  |_|  /\  |_) |_)(_  \    / |_ |_ |_) |_ |_)" + "\n" +
                             @"__) | | /--\ | \ |  __)  \/\/  |_ |_ |   |_ | \ v2.0" +
                             $"\n\nP: Play game\nD: Difficulty\nE: Exit\n{prompt}:");

            }
        }

        private void DifficultyMenu()
        {
            string prompt = "Please enter a selection";
            while (true)
            {
                Console.Clear();
                Console.Write($"Difficulty menu\nCurrent grid size is {GridHeight} by {GridWidth} with {MineCount} mines\nC: change difficulty\nE: return to main menu\n{prompt}:");
                switch (GetMenuInput())
                {
                    case 'C':
                        ChangeDifficulty();
                        prompt = "Please enter a selection";
                        break;
                    case 'E':
                        return;
                    case '!':
                        prompt = "Invalid selection";
                        break;
                    default:
                        prompt = "Invalid selection";
                        break;
                }
            }
            void ChangeDifficulty()
            {
                prompt = "Please enter a selection";
                while (true)
                {
                    Console.Clear();
                    Console.Write($"Change difficulty\nB: Beginner (9 by 9 with 10 mines)\nI: Intermediate (16 by 16 with 40 mines)\nE: Expert (30 by 16 with 99 mines)\nC: Custom size\nS: Change generation seed\n{prompt}:");
                    switch (GetMenuInput())
                    {
                        case 'B':
                            GridHeight = GridWidth = 9;
                            MineCount = 10;
                            return;
                        case 'I':
                            GridHeight = GridWidth = 16;
                            MineCount = 40;
                            return;
                        case 'E':
                            GridHeight = 30;
                            GridWidth = 16;
                            MineCount = 99;
                            return;
                        case 'C':
                            CustomDifficulty();
                            return;
                        case 'S':
                            RandomSeedMenu();
                            return;
                        case '!':
                            prompt = "Invalid selection";
                            break;
                        default:
                            prompt = "Invalid selection";
                            break;
                    }
                }
            }
            void CustomDifficulty()
            {
                bool validInput = false;
                Console.Clear();
                Console.Write("Custom difficulty\n");
                while (!validInput)
                {
                    Console.Write("Please enter a height greater than 1 (recommended to keep hight less than 37):");
                    try
                    {
                        int input = Convert.ToInt32(Console.ReadLine());
                        if (input > 1)
                        {
                            GridHeight = input;
                            validInput = true;
                        }
                        else
                        {
                            Console.Write("Height must be greater than 1\n");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.Write("Invalid input please enter a whole number\n");
                    }
                } //Get height
                validInput = false;
                while (!validInput)
                {
                    Console.Write("Please enter a width greater than 1 and less than 27:");
                    try
                    {
                        int input = Convert.ToInt32(Console.ReadLine());
                        if (input > 1 && input <= 26)
                        {
                            GridWidth = input;
                            validInput = true;
                        }
                        else
                        {
                            Console.Write("Width must be between 1 and 27\n");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.Write("Invalid input please enter a whole number");
                    }
                } //Get width
                validInput = false;
                while (!validInput) //Get minecount
                {
                    Console.Write($"Please enter number of mines, must be less than {GridHeight * GridWidth - 1} (Recomended number {(int)((GridHeight * GridWidth) * 0.15)}):");
                    try
                    {
                        int input = Convert.ToInt32(Console.ReadLine());
                        if (input > 0 && input <= GridHeight * GridWidth - 1)
                        {
                            MineCount = input;
                            validInput = true;
                        }
                        else
                        {
                            Console.Write($"Number of mines must be greater than zero and less than {GridHeight * GridWidth - 1}\n");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.Write("Invalid input please enter a whole number");
                    }
                } //Get number of mines
            }
            void RandomSeedMenu()
            {
                prompt = "Please enter a selection";
                string randomSeed;
                while (true)
                {
                    if (RandomSeed == -1)
                    {
                        randomSeed = "Random";
                    }
                    else
                    {
                        randomSeed = $"{RandomSeed}";
                    }
                    Console.Clear();
                    Console.WriteLine($"Random seed menu\nCurrent seed is {randomSeed}\nC: Change seed\nE: Return to difficulty menu\n{prompt}:");
                    switch (GetMenuInput())
                    {
                        case 'C':
                            ChangeSeed();
                            prompt = "Please enter a selection";
                            break;
                        case 'E':
                            return;
                        case '!':
                            prompt = "Invalid selection";
                            break;
                        default:
                            prompt = "Invalid selection";
                            break;
                    }
                }
                void ChangeSeed()
                {
                    prompt = "";
                    while (true)
                    {
                        Console.Clear();
                        Console.Write($"{prompt}\nPlease enter a seed or '-1' to use a random seed:");
                        try
                        {
                            RandomSeed = Convert.ToInt32(Console.ReadLine());
                            return;
                        }
                        catch (FormatException)
                        {
                            prompt = "please enter a whole number";
                        }
                    }
                }
            }
        }
        private char GetMenuInput()
        {
            try
            {
                return Convert.ToChar(Console.ReadLine().ToUpper());
            }
            catch (FormatException)
            {
                return '!';
            }
        }
    }
}
