using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    enum GunState { Working = 1, Charged = 2, SaffetyOn = 4};
    class Gun : Weapon, IFireable
    {
        private int bullets;
        private const int max_bullets = 8;
        private DateTime noAmmoTime;
        private double precision = 1;
        private string label;
        GunState state;

        public Gun(string l = "Hello")
        {
            bullets = max_bullets;
            state = GunState.Working;
            state |= GunState.Charged;
            state |= GunState.SaffetyOn;
            label = l;
        }

        ~Gun()
        {
            Console.WriteLine("Policeman " + label + " destroyed");
        }

        public override void info()
        {
            Console.WriteLine("Simpe gun");
        }
        public void safety_off()
        {
            state &= ~GunState.SaffetyOn;
        }
        public void safety_on()
        {
            state &= GunState.SaffetyOn;
        }
        public void fire()
        {
            if ((state & GunState.SaffetyOn) == GunState.SaffetyOn)
            {
                return;
            }
            if (bullets == 1)
            {
                noAmmoTime = DateTime.Now;
                state &= ~GunState.Charged;
            }
            if (bullets > 0)
            {
                bullets--;
                precision -= 0.001;
                if (precision < 0.5)
                    state &= ~GunState.Working;
            }
            else
            {
                NoAmmoExceptionArgs args = new NoAmmoExceptionArgs(noAmmoTime);
                throw new NoAmmoException(args);
            }
        }
        public double get_precision()
        {
            return precision;
        }
        public bool recharge()
        {
            state &= GunState.Charged;
            this.bullets = max_bullets;
            return true;
        }

    }
}
