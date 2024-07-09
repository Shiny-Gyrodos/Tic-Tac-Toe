// By default, enums are backed by integer (32-bit)
// I decided to explicitly change de default backed type to byte (8-bit integer) 
// and explicit set value same as the default ones
enum SquareType : byte
{
    Empty = 0,
    PlayerOne = 1,
    PlayerTwo = 2
}

static class SquarTypeExtensions
{
    public static string Display(this SquareType type)
    {
        return type switch
        {
            SquareType.Empty => " ",
            SquareType.PlayerOne => "X",
            SquareType.PlayerTwo => "O",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }
}