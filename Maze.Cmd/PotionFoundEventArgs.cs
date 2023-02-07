namespace Maze.Cmd
{
    public class PotionFoundEventArgs : EventArgs
    {
        public Potion Potion { get; set; }
        public string PlayerName { get; set; }
    }
}