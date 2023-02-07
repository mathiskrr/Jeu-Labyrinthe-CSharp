namespace Maze.Cmd
{
    public enum TrapType
    { Hole, Stone, Arrow }

    public class Trap
    {
        //Factory Pattern
        public static Trap? Generate()
        {
            switch (new Random().Next(1, 7))
            {
                case 1:
                case 2:
                    return new Trap(TrapType.Stone, 20);

                case 3:
                case 4:
                    return new Trap(TrapType.Arrow, 10);

                case 5:
                case 6:
                    return new Trap(TrapType.Hole, 5);

                default:
                    //TODO Log
                    return null;
            }
        }

        public TrapType Type { get; }
        public int Damage { get; }

        private Trap(TrapType type, int damage)
        {
            Type = type;
            Damage = damage;
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}