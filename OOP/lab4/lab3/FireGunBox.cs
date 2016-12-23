using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class FireGunBox<T> where T: IFireable
    {
        T[] fireguns;

        public FireGunBox (T[] fireguns)
        {
            this.fireguns = fireguns;
        }
        public object get_gun(int index)
        {
            if (index < fireguns.Length)
                return fireguns[index];
            else
                return null;
        }
    }
}
