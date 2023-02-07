namespace Maze.Cmd
{
    public enum ToolsType
    { Wings, Umbrella, Shield }

    public class Tools
    {
        public static Tools? Generate()
        {
            return new Random().Next(1, 7) switch
            {
                1 or 2 or 3 => new Tools(ToolsType.Umbrella),
                4 or 5 => new Tools(ToolsType.Wings),
                6 => new Tools(ToolsType.Shield),
                _ => null,//TODO Log
            };
        }

        public ToolsType Type { get; }
        public TrapType Trap { get; }
        public bool DestroyedOnUse { get; }

        private Tools(ToolsType type)
        {
            Type = type;
            if (Type == ToolsType.Shield)
            {
                DestroyedOnUse = false;

                Trap = Type switch
                {
                    ToolsType.Wings => TrapType.Hole,
                    ToolsType.Umbrella => TrapType.Stone,
                    ToolsType.Shield => TrapType.Arrow,
                };
            }
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}