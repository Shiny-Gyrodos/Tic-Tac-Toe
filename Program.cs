using Microsoft.VisualBasic.FileIO;

namespace MyApp
{
    internal class Program
    {
        static char winner;
        static bool gameActive = true;
        static char[,] grid = {{' ', ' ', ' '},
                               {' ', ' ', ' '},
                               {' ', ' ', ' '}};
        static void Main(string[] args)
        {
            bool player1turn = true;

            UpdateDisplay();

            while (gameActive)
            {
                GetPlayerInput(player1turn);
                UpdateDisplay();

                player1turn = player1turn == true ? false : true; // Swaps the symbol being used.

                CheckForWinner();
            }
            Console.Write($"\n{winner} wins!\n\nPress any key to close the window.");
            Console.ReadKey();
        }



        static void CheckForWinner() // This method needs a total rewrite.
        {
            int gridSpace1;
            int gridSpace2;
            char[] symbols = new char[3];

            for (int i = 0; i < 6; i++) // Tests horizontal and vertical lines.
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i >= 3)
                    {
                        gridSpace1 = i - 3;
                        gridSpace2 = j;
                    }
                    else
                    {
                        gridSpace1 = j;
                        gridSpace2 = i;
                    }

                    symbols[j] = grid[gridSpace1, gridSpace2];
                }

                if (symbols[0] == symbols[1] && symbols[1] == symbols[2] && symbols[0] != ' ')
                {
                    gameActive = false;
                    winner = symbols[0];
                    i = 6; // Breaks out of for loop early.
                }
            }

            // Tests diagonal lines.
            if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[1, 1] != ' ' || grid[2, 0] == grid[1, 1] && grid[1, 1] == grid[0, 2] && grid[1, 1] != ' ')
            {
                gameActive = false;
                winner = grid[1, 1]; // Both diagonals share this point.
            }
        }



        static void GetPlayerInput(bool isPlayer1) // Add testing for already filled grids.
        {
            char[] gridOptions = [grid[2, 0], grid[2, 1], grid[2, 2], grid[1, 0], grid[1, 1], grid[1, 2], grid[0, 0], grid[0, 1], grid[0, 2]];
            char symbol = isPlayer1 ? 'X' : 'O';
            
            bool validChoiceMade = false;

            while (!validChoiceMade)
            {
                try
                {
                    int playerInput = int.Parse(Console.ReadKey().KeyChar.ToString());

                    if (gridOptions[playerInput - 1] == ' ')
                    {
                        gridOptions[playerInput - 1] = symbol; // Needs to modify the actual grid.
                        validChoiceMade = true;
                    }
                }
                catch{}
            }
        }



        static void UpdateDisplay()
        {
            Console.Clear();
            Console.WriteLine($"{grid[0, 0]} | {grid[0, 1]} | {grid[0, 2]}\n---------\n{grid[1, 0]} | {grid[1, 1]} | {grid[1, 2]}\n---------\n{grid[2, 0]} | {grid[2, 1]} | {grid[2, 2]}");
        }
    }
}