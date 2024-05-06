namespace MyApp;

internal static class Program
{
    static readonly char[,] grid = {{' ', ' ', ' '},
                                    {' ', ' ', ' '},
                                    {' ', ' ', ' '}};
    static bool isPlayer1Turn = true;
    static void Main()
    {
        string winner = "";

        Console.Write("Ready to play some Tic-Tac-Toe?\n\nPress any key to continue.");
        Console.ReadKey();
        Console.Clear();

        UpdateDisplay();

        for (int i = 0; i < 9; i++)
        {
            GetPlayerInput();
            winner = CheckForWinner();
        }
        Console.Write($"\n{winner} wins!\n\nPress any key to close the window.");
        Console.ReadKey();
    }

    static string CheckForWinner() // Returns winner symbol, or "The cat" if the game is a draw.
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
                return symbols[0].ToString();
            }
        }

        // Tests diagonal lines.
        if ((grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[1, 1] != ' ') ||
            (grid[2, 0] == grid[1, 1] && grid[1, 1] == grid[0, 2] && grid[1, 1] != ' '))
        {
            return grid[1, 1].ToString(); // Both diagonals share this point.
        }
        return "The cat";
    }

    static void GetPlayerInput()
    {
        int[] gridCoordinates = [2, 2, 2, 1, 1, 1, 0, 0, 0, 0, 1, 2, 0, 1, 2, 0, 1, 2];
        char symbol = isPlayer1Turn ? 'X' : 'O'; // Swaps the symbol used depending on who's turn it is.            
        bool validChoiceMade = false;

        while (!validChoiceMade)
        {
            if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int playerInput) &&
                grid[gridCoordinates[playerInput - 1], gridCoordinates[playerInput + 8]] == ' ')
            {
                grid[gridCoordinates[playerInput - 1], gridCoordinates[playerInput + 8]] = symbol;
                validChoiceMade = true;
            }

            UpdateDisplay();
        }
        isPlayer1Turn = !isPlayer1Turn;
    }

    static void UpdateDisplay()
    {
        Console.Clear();
        Console.WriteLine("Place X/O's with the numeric keypad.\n\n" +
                     $"{grid[0, 0]} | {grid[0, 1]} | {grid[0, 2]}\n" +
                     "---------\n"                                   +
                     $"{grid[1, 0]} | {grid[1, 1]} | {grid[1, 2]}\n" +
                     "---------\n"                                   +
                     $"{grid[2, 0]} | {grid[2, 1]} | {grid[2, 2]}");
    }
}