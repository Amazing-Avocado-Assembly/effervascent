enum Direction
{
    Left = -1,
    None = 0,
    Right = 1
}

static class DirectionExtensions
{
    public static Direction Invert(this Direction direction)
    {
        return (Direction)(-(int)direction);
    }
}