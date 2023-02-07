namespace Maze.Cmd
{
    public static class Dice
    {
        private static Random rand = new Random();

        public static int Roll(int min = 1, int max = 7)
        {
            return rand.Next(min, max);
        }
    }
}