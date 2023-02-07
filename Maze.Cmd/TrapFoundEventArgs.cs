namespace Maze.Cmd
{
    public class TrapFoundEventArgs : EventArgs
    {
        public Trap Trap { get; set; }
        public string PlayerName { get; set; }
    }
}