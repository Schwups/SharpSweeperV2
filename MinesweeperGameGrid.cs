using System;

namespace SharpSweeperV2
{
    public class Tile
    {
        //Properties
        public bool Revealed { get; protected set; }
        public bool HasMine { get; protected set; }
        public int NearbyMines { get; protected set; }
        //Constructor
        public Tile()
        {
            Revealed = false;
            HasMine = false;
            NearbyMines = 0;
        }
        //Methods
        public void Reveal() => Revealed = true;
        public void AddMine() => HasMine = true;
        public void AddNearbyMine() => NearbyMines++;
    }
    public class MinesweeperGameGrid
    {
        //Properties
        private const char HiddenSpace = '█';
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        public int MineCount { get; protected set; }
        public int RandomSeed { get; protected set; }
        public Tile[,] Grid { get; protected set; }
        //Constructors
        public MinesweeperGameGrid(int MineCount, int Width, int Height, int RandomSeed)
        {
            this.Height = Height;
            this.Width = Width;
            this.MineCount = MineCount;
            this.RandomSeed = RandomSeed;
            Grid = CreateGameGrid();
        }

        //Methods
        private Tile[,] CreateGameGrid()
        {
            Random Rnd;
            if (RandomSeed != -1)
            {
                Rnd = new Random(RandomSeed);
            }
            else
            {
                Rnd = new Random();
            }

            Tile[,] grid = new Tile[Height, Width];
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    grid[h, w] = new Tile();
                }
            }

            int Mines = 0;
            while (Mines < MineCount)
            {
                int h = Rnd.Next(0, Height);
                int w = Rnd.Next(0, Width);
                if (grid[h, w].HasMine == true)
                {
                    continue;
                }
                grid[h, w].AddMine();
                for (int h2 = h - 1; h2 <= h + 1; h2++)
                {
                    for (int w2 = w - 1; w2 <= w + 1; w2++)
                    {
                        if (0 <= h2 && h2 <= Height - 1 && 0 <= w2 && w2 <= Width - 1)
                        {
                            grid[h2, w2].AddNearbyMine();
                        }
                    }
                }
                Mines++;
            }
            return grid;
        }
        public void DisplayGrid(int DisplayMode)
        {
            //DisplayModes: 0 = normal, 1 = normal + all mines, 2 = force show everything
            if (DisplayMode < 0) { DisplayMode = 0; }
            PrintTopLetters();
            for (int h = 0; h < Height; h++)
            {
                PrintDividingLine();
                PrintNextLine(h);
            }
            PrintDividingLine();

            void PrintTopLetters()
            {
                Console.Write("    "); // 3 spaces
                for (int w = 0; w < Width; w++)
                {
                    Console.Write((char)(w + 65) + " ");
                }
                Console.Write("\n");
            }
            void PrintDividingLine()
            {
                Console.Write("   "/*3 spaces*/ + "+");
                for (int w = 0; w < Width; w++)
                {
                    Console.Write("-+");
                }
                Console.Write("\n");
            }
            void PrintNextLine(int h)
            {
                Console.Write($"{h + 1}".PadRight(3) + "|");
                for (int w = 0; w < Width; w++)
                {
                    if (Grid[h, w].Revealed == false && DisplayMode == 0)
                    {
                        Console.Write(HiddenSpace);
                    }
                    else
                    {
                        if (Grid[h, w].HasMine == true)
                        {
                            Console.Write("*");
                        }
                        else if (DisplayMode == 2 || Grid[h, w].Revealed)
                        {
                            Console.Write(Grid[h, w].NearbyMines);
                        }
                        else
                        {
                            Console.Write(HiddenSpace);
                        }
                    }
                    Console.Write("|");
                }
                Console.Write("\n");
            }
        }
    }
}
