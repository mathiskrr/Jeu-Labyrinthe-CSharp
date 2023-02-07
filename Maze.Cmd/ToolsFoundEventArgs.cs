namespace Maze.Cmd
{
    public class ToolsFoundEventArgs : EventArgs
    {
        public Tools Tools { get; set; }
        public string PlayerName { get; set; }
    }
}