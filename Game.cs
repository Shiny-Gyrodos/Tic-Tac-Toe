using System.Diagnostics.CodeAnalysis;

namespace MyApp;

internal class Game
{
    private SquareType? winner = null;
    private bool player1turn = true;
    private SquareType[] grid;
    private static int[][] victoryPatterns = new[]
    {
        // rows
        new[] { 0, 1, 2 },
        new[] { 3, 4, 5 },
        new[] { 6, 7, 8 },
        // columns
        new[] { 0, 3, 6 },
        new[] { 1, 4, 7 },
        new[] { 2, 5, 8 },
        // diagonals
        new[] { 0, 4, 8 },
        new[] { 2, 4, 6 },
    };

    public void Start()
    {
        grid = new SquareType[9];
        player1turn = true;

        UpdateDisplay();
    }

    public bool PlayTurn()
    {
        SetPlayerInput(player1turn ? SquareType.PlayerOne : SquareType.PlayerTwo);
        var hasEnded = TryGetWinner(out winner);
        UpdateDisplay();
        return !hasEnded;
    }

    private bool TryGetWinner([NotNullWhen(true)] out SquareType? winner)
    {
        foreach (var pattern in victoryPatterns)
        {
            if (grid[pattern[0]] == grid[pattern[1]]
                && grid[pattern[1]] == grid[pattern[2]]
                && grid[pattern[0]] != SquareType.Empty
               )
            {
                winner = grid[pattern[0]];
                return true;
            }
        }

        winner = null;
        return false;
    }

    private void SetPlayerInput(SquareType player)
    {
        while (true)
        {
            // This is little trick to convert a char to an int.
            // A char is in fact an int (the corresponding ASCII index)
            // but the index and the represented digit isn't aligned
            // thus, we need to align the two by subtracting the input with the index of the 0 digit
            // We could use other utility methods such as int.TryParse, but it requires a string
            int input = Console.ReadKey().KeyChar - '0';

            if (
                // this line is using pattern matching, and it is the equivalent of: input >= 1 && input <= 9, it's only shorter
                input is >= 1 and <= 9
                && grid[input - 1] == SquareType.Empty)
            {
                grid[input - 1] = player;
                player1turn = player1turn == true ? false : true;
                // return instruction can also return no value
                // and it will end the method execution, thus breaking the while loop
                return;
            }

            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        Console.Clear();

        // the three quotes notation is calls raw string litterals
        // it will interprets code breaking lines and will remove leading indentation
        // to the position of the first ending quotes.
        var output = $"""
            Place X/O's with the numeric keypad.

            {grid[6].Display()} | {grid[7].Display()} | {grid[8].Display()}
            ---------
            {grid[3].Display()} | {grid[4].Display()} | {grid[5].Display()}
            ---------
            {grid[0].Display()} | {grid[1].Display()} | {grid[2].Display()}
            """;
        Console.WriteLine(output);
    }
}