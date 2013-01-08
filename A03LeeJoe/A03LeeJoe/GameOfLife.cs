// Joe Lee

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace A03LeeJoe
{
    class GameOfLife
    {
        private int[, ,] grid;
        private int displayedGrid = 0;
        private int tempGrid = 1;

        private const int DEAD = 0;
        private const int ALIVE = 1;
        private const int TOP = 5;
        private const int LEFT = 20;

        //constructor
        public GameOfLife()
        {
            grid = CreateGrid();
        }

        //methods
        private int[, ,] CreateGrid()
        {
            int[, ,] newGrid = new int[25, 40, 2];
            for (int col = 14; col < 27; col++)
            {
                newGrid[13, col, 0] = ALIVE;
            }
            for (int row = 8; row < 11; row++)
            {
                newGrid[row, 12, 0] = ALIVE;
                newGrid[row, 20, 0] = ALIVE;
                newGrid[row, 28, 0] = ALIVE;
            }
            return newGrid;
        }

        public void Start()
        {
            //Display Grid
            Console.SetWindowSize(80, 40);
            Setup();
            //loop until user presses a key
            while (!Console.KeyAvailable)
            {
                Thread.Sleep(1000);
                //perform next move
                NextMove();

                DisplayGrid();

                SetColor(1);
                Console.WriteLine();
                Console.CursorLeft = LEFT;
                Console.WriteLine("         Press any key to end          ", displayedGrid);
            }
        }

        private void Setup()
        {
            CreateHeader();
            DisplayGrid();
            //prompt user to press enter
            Console.WriteLine();
            Console.CursorLeft = LEFT;
            SetColor(1);
            Console.WriteLine("       Please press enter to begin      ");
            Console.ReadLine();
        }

        private void CreateHeader()
        {
            Console.SetCursorPosition(LEFT, TOP);
            SetColor(1);
            Console.WriteLine("{0,40}", " ");
            Console.CursorLeft = LEFT;
            Console.WriteLine("          G A M E  O F  L I F E         ");
            Console.CursorLeft = LEFT;
            Console.WriteLine("{0,40}", " ");
        }

        private void NextMove()
        {
            //iterate through grid
            for (int row = 0; row <= grid.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= grid.GetUpperBound(1); col++)
                {
                    //each iteration check neighbor count
                    int neighborCount = GetNeighborCount(row, col);
                    //determine new life or death and update on temp grid'
                    grid[row, col, tempGrid] = UpdateCell(grid[row, col, displayedGrid], neighborCount);
                }
            }
            //switch the grid's 3rd dimension to display new grid
            SwitchGrid();
        }

        private void SwitchGrid()
        {
            if (displayedGrid == 0)
            {
                displayedGrid = 1;
                tempGrid = 0;
            }
            else
            {
                displayedGrid = 0;
                tempGrid = 1;
            }
        }

        private int UpdateCell(int stateOfCell, int neighborCount)
        {
            if (stateOfCell == ALIVE)
            {
                switch (neighborCount)
                {
                    case 0:
                    case 1:
                        return DEAD;
                    case 2:
                    case 3:
                        return ALIVE;
                    default:
                        return DEAD;
                }
            }
            else if (neighborCount == 3)
            {
                return ALIVE;
            }
            return stateOfCell;
        }

        private void DisplayGrid()
        {
            Console.SetCursorPosition(LEFT, TOP + 4);
            for (int row = 0; row <= grid.GetUpperBound(0); row++)
            {
                Console.CursorLeft = LEFT;
                for (int col = 0; col <= grid.GetUpperBound(1); col++)
                {

                    SetColor(grid[row, col, displayedGrid]);
                    Console.Write(".");

                }
                Console.WriteLine();
            }
        }

        private void SetColor(int state)
        {
            if (state == 1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        private int GetNeighborCount(int row, int col)
        {
            int neighborCount = 0;
            int rowStart = row - 1;
            int rowStop = row + 1;
            int colStart = col - 1;
            int colStop = col + 1;

            for (int rowIndex = rowStart; rowIndex <= rowStop; rowIndex++)
            {
                for (int colIndex = colStart; colIndex <= colStop; colIndex++)
                {
                    if ((rowIndex == row && colIndex == col) ||
                    (OutOfBounds(rowIndex, colIndex)))
                    {
                        continue;
                    }
                    if (grid[rowIndex, colIndex, displayedGrid] == ALIVE)
                    {
                        neighborCount++;
                    }
                }
            }
            return neighborCount;
        }

        private Boolean OutOfBounds(int row, int col)
        {
            return !((row >= 0 && row <= grid.GetUpperBound(0)) &&
            (col >= 0 && col <= grid.GetUpperBound(1)));
        }
    }
}