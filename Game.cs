using System.Diagnostics.CodeAnalysis;

namespace MyApp;

internal class Game
{
    public SquareType? winner = null;
    private bool player1turn = true;
    public SquareType[] grid;
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

    private int turnsPlayed = 0;

    public void Start()
    {
        grid = new SquareType[9];
        player1turn = true;
    }

    public bool PlayTurn(int squareIndex)
    {
        SetPlayerInput(squareIndex);
        return !TryGetWinner(out winner) && turnsPlayed < 9;
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

    private void SetPlayerInput(int squareIndex)
    {
        if (grid[squareIndex] != SquareType.Empty)
        {
            return;
        }

        grid[squareIndex] = player1turn ? SquareType.PlayerOne : SquareType.PlayerTwo;
        player1turn = !player1turn;
        turnsPlayed++;
    }
}