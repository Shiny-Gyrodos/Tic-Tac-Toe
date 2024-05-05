namespace MyApp;

internal class Program
{
    static void Main(string[] args)
    {
        var game = new Game();

        Console.Write("Ready to play some Tic-Tac-Toe?\n\nPress any key to continue.");
        Console.ReadKey();
        Console.Clear();

        game.Start();
        while (game.PlayTurn())
        { }
    }
}