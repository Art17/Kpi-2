using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Grenade : Weapon, IExplodable, ITimed
    {
        bool isExploded = false;   // Используйте содержательные имена
        public override void getInfo()
        {
            Console.WriteLine("Timed gronate");
        }

        public bool explode()
        {
            if (!isExploded)
            {
                isExploded = true;
                return true;
            }
            return false;
        }
        public void setTime(int secs)
        {
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                this.explode();
                timer.Dispose();
            },
                        null, 1000*secs, System.Threading.Timeout.Infinite);
        }
    }
}
