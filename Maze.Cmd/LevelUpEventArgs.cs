namespace Maze.Cmd
{
    public class LevelUpEventArgs : EventArgs
    {
        public int Level { get; set; }
        public string Name { get; set; }
    }
}