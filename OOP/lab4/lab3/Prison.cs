using System;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    [Serializable]
    [DataContract]
    public class Prison : IEnumerable
    {
        [DataMember]
        private const int max_prisoners = 10;
        [DataMember]
        private Prisoner[] cells;
        [DataMember]
        private int count;

        public Prison()
        {
            cells = new Prisoner[max_prisoners];
            count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PrisonEnum GetEnumerator()
        {
            return new PrisonEnum(cells.Take(count).ToArray());
        }

        public Prisoner this[string index]
        {
            get
            {
                int b = (int)(index[0] - 'A');
                int pos = int.Parse(index.Substring(1));
                return cells[b * 4 + pos];
            }

            set
            {
                int b = (int)(index[0] - 'A');
                int pos = int.Parse(index.Substring(1));
                cells[b * 4 + pos] = value;
                count++;
            }
        }
    }
    public class PrisonEnum : IEnumerator
    {
        public Prisoner[] prisoners;

        int position = -1;

        public PrisonEnum(Prisoner[] list)
        {
            prisoners = list;
        }

        public bool MoveNext()
        {
            do
            {
                position++;
            } while (position < prisoners.Length && prisoners[position] == null);

            return (position < prisoners.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Prisoner Current
        {
            get
            {
                try
                {
                    return prisoners[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
