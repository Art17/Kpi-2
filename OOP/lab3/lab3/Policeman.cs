using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lab2
{
    class Policeman
    {
        private int badge;
        private Queue<string> suspicious;

        public Policeman(int badge)
        {
            this.badge = badge;
            suspicious = new Queue<string>();
            System.Timers.Timer aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(check);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 2000;
            aTimer.Enabled = true;

        }
        private void check(object source, ElapsedEventArgs e)
        {
            if (suspicious.Count != 0)
            {
                string name = suspicious.Dequeue();
                Console.WriteLine("Policeman " + badge + " checked " + name);
            }
        }

        public void subscribe_on_shop_weapon_bought(Shop shop)
        {
            shop.WeaponBought += delegate (object sender, WeaponBoughtEventArgs args)
            {
                Console.WriteLine(String.Format("Policeman with badge {0} received that {1} bought weapon", badge, args.Name));
                suspicious.Enqueue(args.Name);
            };
        }
    }
}
