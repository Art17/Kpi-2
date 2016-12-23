using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            Shop mainShop = new Shop();
            Policeman Alex = new Policeman(1);
            Policeman John = new Policeman(2);
            Policeman Rick = new Policeman(3);

            Alex.subscribe_on_shop_weapon_bought(mainShop);
            John.subscribe_on_shop_weapon_bought(mainShop);

            Gun my_gun = mainShop.get_gun("Darrel");
            Gun new_gun = mainShop.get_gun("Peter");

            my_gun.safety_off();
            new_gun.safety_off();

            Gun[] guns = { my_gun, new_gun };
            
            for (int i = 0; i < 16; i++)
            {
                try
                {
                    my_gun.fire();
                }
                catch(NoAmmoException e)
                {
                    Console.WriteLine("NoAmmoException exception caught");
                    Console.WriteLine(String.Format("Bullets ended on " + e.Args.Time.ToString()));
                    my_gun.recharge();
                }

            }

            Action<Gun>[] actions = new Action<Gun>[3] { recharge, fire, fire };
            foreach (Action<Gun> act in actions)
                act(my_gun);
            Func<Gun, bool> isGood = ((gun) => {return gun.get_precision() > 0.999; });
            foreach (Gun g in guns)
            {
                if (isGood(g))
                {
                    Console.WriteLine("Good gun");
                }
                else
                {
                    Console.WriteLine("Bad gun");
                }
            }
            Console.WriteLine("prisoners test");
            Prison blackstorm = create_simple_prison();

            Console.WriteLine("Prisoner from B1");
            Prisoner p1 = blackstorm["B1"];
            Console.WriteLine(p1.FirstName + " " + p1.LastName);

            Console.WriteLine("-------Iterate prison-------");
            foreach(Prisoner p in blackstorm)
            {
                Console.WriteLine(p.FirstName + " " + p.LastName + " " + p.Dangerous);
            }
            Console.WriteLine("Total dangerous: " + blackstorm.total_dangerous());
            Gun g1 = new Gun("1 gun");
            Gun g2 = new Gun("2 gun");
            Gun g3 = new Lab2.Gun("3 gun");
            WeakReference wr = new WeakReference(new Gun("4 gun"));

            g1 = null;
            g2 = null;

            GC.Collect();

            //(wr.Target as Gun).fire();

            save_binary(blackstorm);
            save_json(blackstorm);

            Console.In.Read();
            Alex.Dispose();
            John.Dispose();
            Rick.Dispose();
        }


        static void recharge (Gun g)
        {
            Console.Out.WriteLine("Gun recharged");
            g.recharge();
        }

        static void fire (Gun g)
        {
            Console.Out.WriteLine("Gun fired");
            g.fire();
        }

        static Prison load_binary()
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream stream = new FileStream("blackstorm.bin", FileMode.Open))
            {
                Prison blackstorm = (Prison)binFormat.Deserialize(stream);
                return blackstorm;
            }
        }

        static Prison load_json()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Prison));
            using (Stream stream = new FileStream("blackstorm.json",
            FileMode.Create, FileAccess.Write, FileShare.None))
            {
                stream.Position = 0;
                StreamReader streamReader = new StreamReader(stream);
                Console.WriteLine(streamReader.ReadToEnd());
                stream.Position = 0;
                Prison result = (Prison)ser.ReadObject(stream);
                return result;
            }
        }

        static void save_binary(Prison p)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream("blackstorm.bin",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, p);
            }
        }

        static void save_json(Prison p)
        {
            using (Stream stream = new FileStream("blackstorm.json",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Prison));
                ser.WriteObject(stream, p);
            }
        }

        static Prison create_simple_prison()
        {
            Prisoner Haskell = new Prisoner("Haskell", "Johnson", 24, "Murder", 7);
            Prisoner CPP = new Prisoner("Cpp", "Anderson", 44, "Thief", 4);
            Prisoner Scala = new Prisoner("Scala", "Jason", 36, "Slayer", 9);
            Prison blackstorm = new Prison();


            Console.WriteLine("Put Haskell to A1");
            Console.WriteLine("Put Scala to B1");
            Console.WriteLine("Put CPP to A2");
            blackstorm["A1"] = Haskell;
            blackstorm["B1"] = Scala;
            blackstorm["A2"] = CPP;

            return blackstorm;
        }
    }

    static class ExtensionClass
    {
        public static int total_dangerous(this Prison prison)
        {
            int sum = 0;
            foreach (Prisoner p in prison)
            {
                sum += p.Dangerous;
            }
            return sum;
        }
    }
}
