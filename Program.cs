namespace MyApp;

internal class Program
{
    static void Main(string[] args)
    {
        var game = new Game();

        Console.WriteLine("Ready to play some Tic-Tac-Toe?");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();

        game.Start();
        RefreshDisplay(game);
        while (game.PlayTurn(GetPlayerInput()))
        {
            RefreshDisplay(game);
        }

        Console.Clear();
        DrawBoard(game);
        if (game.winner is not null)
        {
            Console.WriteLine($"{game.winner.Value.Display()} wins!");
        }
        else
        {
            Console.WriteLine($"It's a draw, nobody wins!");
        }


        Console.WriteLine("Press any key to close the window.");
        Console.ReadKey();
    }

    private static void RefreshDisplay(Game game)
    {
        Console.Clear();
        Console.WriteLine("Place X/O's with the numeric keypad.");
        DrawBoard(game);
    }

    private static void DrawBoard(Game game)
    {
        Console.WriteLine($"""
            {game.grid[6].Display()} | {game.grid[7].Display()} | {game.grid[8].Display()}
            ---------
            {game.grid[3].Display()} | {game.grid[4].Display()} | {game.grid[5].Display()}
            ---------
            {game.grid[0].Display()} | {game.grid[1].Display()} | {game.grid[2].Display()}
            """);
    }

    private static int GetPlayerInput()
    {
        while (true)
        {
            int input = Console.ReadKey().KeyChar - '0';

            if (input is >= 1 and <= 9)
            {
                return input - 1;
            }
        }
    }
}