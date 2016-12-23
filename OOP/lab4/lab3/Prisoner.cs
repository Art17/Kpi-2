using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Lab2
{
    [Serializable]
    [DataContract]
    public class Prisoner : IComparable
    {
        [DataMember]
        private string first_name;
        [DataMember]
        private string last_name;
        [DataMember]
        private int age;
        [DataMember]
        private string description;
        [DataMember]
        private int dangerous;

        public Prisoner(string f, string l, int a, string d, int dan)
        {
            first_name = f;
            last_name = l;
            age = a;
            description = d;
            dangerous = dan;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Prisoner otherPrisoner = obj as Prisoner;
            if (otherPrisoner != null)
                return this.dangerous.CompareTo(otherPrisoner.dangerous);
            else
                throw new ArgumentException("Object is not a Prisoner");
        }

        public string FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }
        public string LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int Dangerous
        {
            get { return dangerous; }
            set { dangerous = value; }
        }
    }
}
