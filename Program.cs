namespace MyApp;

internal static class Program
{
    static readonly char[,] grid = {{' ', ' ', ' '},
                                    {' ', ' ', ' '},
                                    {' ', ' ', ' '}};
    static bool isPlayer1Turn = true;
    static string winner = "The cat";

    static void Main()
    {
        Console.Write("Ready to play some Tic-Tac-Toe?\n\nPress any key to continue.");
        Console.ReadKey();

        for (int i = 0; i < 9; i++)
        {
            GetPlayerInput();
            if (HasWinner())
                break;
        }
        Console.Write($"\n{winner} wins!\n\nPress any key to close the window.");
        Console.ReadKey();
    }

    static bool HasWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            // Check horizontal and vertical lines
            if ((grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2] && grid[i, 0] != ' ') ||
                (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i] && grid[0, i] != ' '))
            {
                winner = grid[i, i].ToString();
                return true;
            }
        }

        // Check diagonal lines
        if ((grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[1, 1] != ' ') ||
            (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0] && grid[1, 1] != ' '))
        {
            winner = grid[1, 1].ToString();
            return true;
        }

        return false;
    }

    static void GetPlayerInput()
    {
        int[] gridCoordinates = [2, 2, 2, 1, 1, 1, 0, 0, 0, 0, 1, 2, 0, 1, 2, 0, 1, 2];
        char symbol = isPlayer1Turn ? 'X' : 'O'; // Swaps the symbol used depending on who's turn it is.            
        bool isValid = false;

        while (!isValid)
        {
            UpdateDisplay();
            if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int playerInput) &&
                grid[gridCoordinates[playerInput - 1], gridCoordinates[playerInput + 8]] == ' ')
            {
                grid[gridCoordinates[playerInput - 1], gridCoordinates[playerInput + 8]] = symbol;
                isValid = true;
            }
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