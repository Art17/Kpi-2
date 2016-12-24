using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class GrenadeGun : Weapon, IFireable
    {
        private Grenade[] grenades;
        private int current;
        private const int MAX_GRENADES = 5;

        public GrenadeGun()
        {
            grenades = new Grenade[MAX_GRENADES];
            current = 0;
        }

        public override void getInfo()
        {
            Console.WriteLine("Simpe granatome");
        }

        public void fire()
        {
            if (current < MAX_GRENADES)
            {
                (grenades[current] as ITimed).setTime(3);
                current++;
            }
        }
        public bool recharge()
        {
            grenades = new Grenade[MAX_GRENADES];
            current = 0;
            return true;
        }
    }
}
