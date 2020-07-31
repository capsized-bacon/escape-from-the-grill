public static class Collision2DSideTypeExtensions {
    public static Collision2DSideType Opposite (this Collision2DSideType sideType) {
        Collision2DSideType opposite = Collision2DSideType.None;
        
        if (sideType == Collision2DSideType.Left) {
            opposite = Collision2DSideType.Right;
        }
        else if (sideType == Collision2DSideType.Right) {
            opposite = Collision2DSideType.Left;
        }
        else if (sideType == Collision2DSideType.Top) {
            opposite = Collision2DSideType.Bottom;
        }
        else if (sideType == Collision2DSideType.Bottom) {
            opposite = Collision2DSideType.Top;
        }

        return opposite;
    }
}