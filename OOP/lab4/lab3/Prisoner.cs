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
        private string firstName;
        [DataMember]
        private string lastName;
        [DataMember]
        private int age;
        [DataMember]
        private string description;
        [DataMember]
        private int dangerous;

        public Prisoner(string f, string l, int a, string d, int dan)
        {
            firstName = f;
            lastName = l;
            age = a;
            description = d;
            dangerous = dan;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;  // Использование положительных условий

            Prisoner otherPrisoner = obj as Prisoner;
            if (otherPrisoner != null)
                return this.dangerous.CompareTo(otherPrisoner.dangerous);
            else
                throw new ArgumentException("Object is not a Prisoner");
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
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
