using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Cmd
{
    public class Monster
    {
        public enum MonsterType
        { Zombie, Skeletons }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public Monster(int attack, int hp = 25)
        {
            HP = hp;
            Attack = attack;
        }

        public static Monster? Generate()
        {
            switch (new Random().Next(1, 7))
            {
                case 1:
                    return new Monster(MonsterType.Skeletons, 15);

                case 6:
                    return new Monster(MonsterType.Zombie, 10);

                default:
                    return null;
            }
        }

        public MonsterType Type { get; }
        public int Damage { get; }

        private Monster(MonsterType type, int damage)
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
