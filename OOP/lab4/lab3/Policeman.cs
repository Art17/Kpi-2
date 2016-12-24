using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lab2
{
    class Policeman : IDisposable
    {
        private int badge;
        private Queue<string> suspicious;
        private System.IO.StreamWriter _sw = null;
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                Close();
            }
            _disposed = true;
        }

        public void Close()
        {
            _sw.Dispose();
        }

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

            _sw = new System.IO.StreamWriter(badge + ".log", true, Encoding.UTF8);

        }


        private void check(object source, ElapsedEventArgs e)
        {
            if (suspicious.Count != 0)
            {
                string name = suspicious.Dequeue();
                Console.WriteLine("Policeman " + badge + " checked " + name);
                _sw.WriteLine("Policeman " + badge + " checked " + name);
            }
        }

        public void subscribeOnWeaponBought(Shop shop)
        {
            shop.WeaponBought += delegate (object sender, WeaponBoughtEventArgs args)
            {
                Console.WriteLine(String.Format("Policeman with badge {0} received that {1} bought weapon", badge, args.Name));
                suspicious.Enqueue(args.Name);
                _sw.WriteLine("Policeman " + badge + " added " + args.Name + " to queue ");
            };
        }
    }
}
