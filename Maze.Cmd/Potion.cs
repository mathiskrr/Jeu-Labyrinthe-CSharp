namespace Maze.Cmd
{
    public class Potion
    {
        public static Potion Generate()
        {
            int amplitude =
                new Random().Next(1, 3 + 1) == 3
               ? 10
               : 5;

            return
                new Random().Next(0, 2) == 0
               ? new LifePotion() { Amplitude = amplitude }
               : new ExpPotion() { Amplitude = amplitude };
                
        }

        public int Amplitude { get; private set; }

        public virtual void Affect(Player player)
        { }
    }

    public class ExpPotion : Potion
    {
        public override void Affect(Player player)
        {
            player.XP += Amplitude;
        }

        public override string ToString()
        {
            return $"Potion d'expérience [{Amplitude}]";
        }
    }

    public class LifePotion : Potion
    {
        public override void Affect(Player player)
        {
            player.HP += Amplitude;
        }

        public override string ToString()
        {
            return $"Potion de vie [{Amplitude}]";
        }
    }
}