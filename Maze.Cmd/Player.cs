using System;

namespace Maze.Cmd
{
    public class Player
    {
        private static int GetXp(int level)
        {
            return (int)(Math.Exp((level - 1) * 1.5) - 1);
        }

        private static int GetHp(int level)
        {
            return (int)(Math.Exp(level / 10.0) * 100);
        }

        private int maxHP;
        private int xp = 0;
        private int nextLevelXp;
        private int hp;
        private readonly List<Tools> tools = new List<Tools>();
        private readonly List<Potion> lifepotion = new List<Potion>();
        private readonly List<ExpPotion> exppotion = new List<ExpPotion>();

        /* Délégués standards du C#
        * Action -> void ()
        * Action<T> -> void (T)
        * Func<T< -> T ()
        * Func<T,U> -> U (T)
        EventHandler -> void (object sender, EventArgs args)*/
        
        public event EventHandler<LevelUpEventArgs> LevelUp;
        public event EventHandler<ItemFoundEventArgs<Tools>> ToolFound;
        public event EventHandler<ItemFoundEventArgs<Potion>> PotionFound;
        public event EventHandler<ItemFoundEventArgs<Trap>> TrapAvoided;
        public event EventHandler<ItemFoundEventArgs<Trap>> Trapped;
        public event EventHandler<ItemFoundEventArgs<Monster>> MonsterFound;

        public string Name;
        public int Level { get; private set; }

        public int XP
        {
            get
            {
                return xp;
            }
            set
            {
                xp = value;
                if (xp >= nextLevelXp)
                {
                    Level++;
                    nextLevelXp = GetXp(Level + 1);
                    maxHP = GetHp(Level);
                    hp = maxHP;
                    LevelUp?.Invoke(this, new LevelUpEventArgs()
                    {
                        Level = Level,
                        PlayerName = Name
                    });
                }
            }
        }

        public int HP
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
                if (hp > maxHP)
                    hp = maxHP;
            }
        }

        //Constructeur de la classe
        public Player(string name)
        {
            Name = name;

            maxHP = GetHp(Level);
            HP = maxHP;
            nextLevelXp = GetXp(Level + 1);
        }

        public void Fall(Trap? trap)
        {
            if (trap == null)
                throw new ArgumentNullException(nameof(trap));

            Tools? tool = tools.FirstOrDefault(x => x.Trap == trap.Type);

            if (tool != null)
            {
                if (tool.DestroyedOnUse)
                    tools.Remove(tool);
                    
                TrapAvoided?.Invoke(this, new ItemFoundEventArgs<Trap>() { Item = trap, PlayerName = Name });
            }
            else
            {
                HP -= trap.Damage;
                Trapped?.Invoke(this, new ItemFoundEventArgs<Trap>() { Item = trap, PlayerName = Name });
            }
        }

        public void MonsterAttack(Monster? monster)
        {
            if (monster == null)
                throw new ArgumentNullException(nameof(monster));
        }

        public void Fight(Monster? monster)
        {
            if (monster == null)
            return;

            while(monster.HP > 0)
            {
                monster.HP -= 10;
                HP -= 10;
                MonsterFound?.Invoke(this, new ItemFoundEventArgs<Monster>()
                {
                    Item = monster,
                    PlayerName = Name
                });
            }
        }

        public void PickUp(Tools? tool)
        {
            if (tool == null)
                return;

            tools.Add(tool);
            ToolFound?.Invoke(this, new ItemFoundEventArgs<Tools>()
            {
                Item = tool,
                PlayerName = Name
            });


        }

        public void PickUp(Potion potion)
        {
            if (potion == null)
                return;

            //potions.Add(potion);
            potion.Affect(this);
            PotionFound?.Invoke(this, new ItemFoundEventArgs<Potion>
            {
                Item = potion,
                PlayerName = Name
            }) ;
        }

        public void Walk()
        {
            XP += 10;
            //Console.WriteLine("Vous gagnez 10 points d'expériences !");
        }

        public override string ToString()
        {
            return $"{XP} {Level}";
        }
    }
}