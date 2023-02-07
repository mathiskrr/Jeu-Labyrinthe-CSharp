using System;

namespace Maze.Cmd
{
    public class LevelUpEventArgs : EventArgs
    {
        public int Level { get; set; }
        public string PlayerName { get; set; }
    }
    public class ItemFoundEventArgs<T> : EventArgs
    {
        public T? Item { get; set; }
        public string PlayerName { get; set; }
    }
}