using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    enum GunState { Working = 1, Charged = 2, SaffetyOn = 4};  // Используйте перечисления вместо констант
    //Имена классов должны представлять собой существительные и их комбинации
    //Компактные функции, длина наибольше функции 15 строк
    //Использование имен из пространства задач: safetyOff, safetyOn, fire, recharge
    class Gun : Weapon, IFireable
    {
        private int bullets;
        private const int MAX_BULLTETS = 8; //имена удобные для поиска
        private DateTime noAmmoTime; //Имена дожны передавать намерения программиста
        private double precision = 1;
        private string label;
        GunState gunState;   //Используйте содержательные имена

        public Gun(string str = "Hello")   // Не используйте перенные l, o так как их легко перепутать с 1 и 0
        {
            bullets = MAX_BULLTETS;
            gunState = GunState.Working;
            gunState |= GunState.Charged;
            gunState |= GunState.SaffetyOn;
            label = str;
        }

        ~Gun()
        {
            Console.WriteLine("Gun " + label + " destroyed");
        }
        //Имена методов дожны представлять собой глаголы и их комбинации
        public override void getInfo()
        {
            Console.WriteLine("Simpe gun");
        }
        public void safetyOff()
        {
            gunState &= ~GunState.SaffetyOn;
        }
        public void safetyOn()
        {
            gunState &= GunState.SaffetyOn;
        }
        public bool isSafetyState()
        {
            return (gunState & GunState.SaffetyOn) == GunState.SaffetyOn;
        }
        public void fire()
        {
            if (isSafetyState())  //Инкапсулируйте сложные выражения
            {
                return;
            }
            if (bullets == 1)
            {
                noAmmoTime = DateTime.Now;
                gunState &= ~GunState.Charged;
            }
            if (bullets > 0)
                makeShot();  // Блоки и отступы: блоки if, else, switch должны состоять из одной строки
            else
            {
                NoAmmoExceptionArgs args = new NoAmmoExceptionArgs(noAmmoTime);   // Используйте исключения, вместо кодов ошибок
                throw new NoAmmoException(args);
            }
        }
        private void makeShot()   // правило одной опреации, функция должна делать толко одно действие
        {
            bullets--;
            precision -= 0.001;
            if (precision < 0.5)
                gunState &= ~GunState.Working;
        }
        public double getPrecision()
        {
            return precision;
        }
        public bool recharge()
        {
            gunState &= GunState.Charged;
            this.bullets = MAX_BULLTETS;
            return true;
        }

    }
}
