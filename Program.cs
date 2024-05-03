using Microsoft.VisualBasic.FileIO;

namespace MyApp
{
    internal class Program
    {
        static bool gameActive = true;
        static char[,] grid = {{' ', ' ', ' '},
                               {' ', ' ', ' '},
                               {' ', ' ', ' '}};
        static void Main(string[] args)
        {
            string winner = "";
            bool player1turn = true;
            int spacesFilled = 0;

            UpdateDisplay();

            while (gameActive && spacesFilled < 9)
            {
                spacesFilled += GetPlayerInput(player1turn);
                UpdateDisplay();

                player1turn = player1turn == true ? false : true; // Swaps the symbol being used.

                winner = CheckForWinner();
            }
            Console.Write($"\n{winner} wins!\n\nPress any key to close the window.");
            Console.ReadKey();
        }



        static string CheckForWinner()
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
                    return symbols[0].ToString();
                }
            }

            // Tests diagonal lines.
            if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[1, 1] != ' ' || grid[2, 0] == grid[1, 1] && grid[1, 1] == grid[0, 2] && grid[1, 1] != ' ')
            {
                gameActive = false;
                return grid[1, 1].ToString(); // Both diagonals share this point.
            }
            return "The cat";
        }



        static int GetPlayerInput(bool isPlayer1)
        {
            int[] gridCoordinates = [2, 2, 2, 1, 1, 1, 0, 0, 0, 0, 1, 2, 0, 1, 2, 0, 1, 2];
            char symbol = isPlayer1 ? 'X' : 'O'; // Swaps the symbol used depending on who's turn it is.
            
            bool validChoiceMade = false;

            while (!validChoiceMade)
            {
                try
                {
                    int playerInput = int.Parse(Console.ReadKey().KeyChar.ToString());

                    if (grid[gridCoordinates[playerInput - 1], gridCoordinates[playerInput + 8]] == ' ')
                    {
                        grid[gridCoordinates[playerInput - 1], gridCoordinates[playerInput + 8]] = symbol;
                        validChoiceMade = true;
                    }
                    else
                    {
                        UpdateDisplay();
                    }
                }
                catch{ UpdateDisplay(); }
            }
            return 1;
        }



        static void UpdateDisplay()
        {
            Console.Clear();
            Console.WriteLine($"{grid[0, 0]} | {grid[0, 1]} | {grid[0, 2]}\n---------\n{grid[1, 0]} | {grid[1, 1]} | {grid[1, 2]}\n---------\n{grid[2, 0]} | {grid[2, 1]} | {grid[2, 2]}");
        }
    }
}