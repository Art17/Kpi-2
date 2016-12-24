/*Чистий код (правила)

1. Функції - повинні мати невелику кількість аргуменів. Максимум 2 аргументі
2.Баласт - відсутність невикористаних функцій, коментарів, змінних
3.Зрозумілі наміри - зрозумілість коду
4. Інкапсуляція - доступ до змінної напряму заборонений
5. Відмова від від'ємних (негативних) значень - формулювання за допомогою позитивних
6. Одна функція - одна операція
7. Вертикальне розміщення (читання коду зверху вниз)
8. Ізолювання try/catch
9.Не поврюються частини коду ( нічого не можна винести в окрему функцію)
11. Видалено закоментований код
12. Видалено мертві фунції
*/

/* Рефакторинг
1. Ізольовані блоки try/catch: функція fireGun
2.Перейменовано назви класів - іменники, відповідає сущності : Gun, Grenade, GrenadeGun
3.Перейменовано назви методів - метод описує дію, що відбувається в ньому(дієслово або дієслово+іменник : safetyOn, safetyOff
4. Назви змінних - із простору задач : darrelGun, bullets
5. Одна функція - одна операція: makeShot
6. Блоки та відступи. Максимум 2 відступи. Великі блоки винесені у функції, наприклад makeShot
7. Складні операції у if винесено в окремі функції: isSafetyState
*/


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
        //Все функции принимают 1, 2 аргумента
        static void Main(string[] args)
        {
            Shop mainShop = new Shop();
            Policeman Alex = new Policeman(1);
            Policeman John = new Policeman(2);
            Policeman Rick = new Policeman(3);

            Alex.subscribeOnWeaponBought(mainShop);
            John.subscribeOnWeaponBought(mainShop);

            Gun darrelGun = mainShop.getGun("Darrel");   // Содержательные имена
            Gun peterGun = mainShop.getGun("Peter");

            darrelGun.safetyOff();
            peterGun.safetyOff();

            Gun[] testGuns = { darrelGun, peterGun };

            for (int i = 0; i < 16; i++)
                fireGun(darrelGun);

            Action<Gun>[] actions = new Action<Gun>[3] { recharge, fire, fire };
            foreach (Action<Gun> act in actions)
                act(darrelGun);
            Func<Gun, bool> isGood = ((gun) => {return gun.getPrecision() > 0.999; });
            foreach (Gun g in testGuns)  // Длина области видимости определяется длиной области видимости
                checkGun(g, isGood);   // Блоки и отсупы

            Console.WriteLine("prisoners test");
            Prison blackstorm = createSimplePrison();

            Console.WriteLine("Prisoner from B1");
            Prisoner p1 = blackstorm["B1"];
            Console.WriteLine(p1.FirstName + " " + p1.LastName);

            Console.WriteLine("-------Iterate prison-------");
            foreach(Prisoner p in blackstorm)
                Console.WriteLine(p.FirstName + " " + p.LastName + " " + p.Dangerous);

            Console.WriteLine("Total dangerous: " + blackstorm.getTotalDangerous());
            Console.WriteLine("Total size before garbage collection: " + GC.GetTotalMemory(false));
            GC.Collect();
            Console.WriteLine("Total size after garbage collection: " + GC.GetTotalMemory(true));
            Gun g1 = new Gun("1 gun");
            Gun g2 = new Gun("2 gun");
            Gun g3 = new Gun("3 gun");
            WeakReference wr = new WeakReference(new Gun("4 gun"));

            g1 = null;
            g2 = null;

            Console.WriteLine("Total size before garbage collection: " + GC.GetTotalMemory(false));
            GC.Collect();
            Console.WriteLine("Total size after garbage collection: " + GC.GetTotalMemory(true));

            //Увидев закомментированный код, удалите его

            saveBinary(blackstorm);
            saveJson(blackstorm);

            Console.In.Read();
            Alex.Dispose();
            John.Dispose();
            Rick.Dispose();
        }

        static void checkGun(Gun g, Func<Gun, bool> isGood)
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

        // Изолируйте блоки try/catch
        static void fireGun(Gun my_gun)
        {
            try
            {
                my_gun.fire();
            }
            catch (NoAmmoException e)
            {
                Console.WriteLine("NoAmmoException exception caught");
                Console.WriteLine(String.Format("Bullets ended on " + e.Args.Time.ToString()));
                my_gun.recharge();
            }
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

        static Prison loadBinary()
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream stream = new FileStream("blackstorm.bin", FileMode.Open))
            {
                Prison blackstorm = (Prison)binFormat.Deserialize(stream);
                return blackstorm;
            }
        }

        static Prison loadJson()
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

        static void saveBinary(Prison p)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream("blackstorm.bin",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, p);
            }
        }

        static void saveJson(Prison p)
        {
            using (Stream stream = new FileStream("blackstorm.json",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Prison));
                ser.WriteObject(stream, p);
            }
        }

        static Prison createSimplePrison()
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
        public static int getTotalDangerous(this Prison prison)   // Имена методов дожны состоять из глаголов
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
