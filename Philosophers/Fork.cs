using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosophers
{
    public class Fork
    {
        public object locker = new object();
        public bool Taken { get; set; }
        public int Index { get;  }
        public int TakenBy { get; set; } = -1;

        public Fork(int index)
        {
            Index = index;
        }

        public void Take(int phInd)
        {
            if (!Taken)
            {
                Taken = true;
                TakenBy = phInd;
            }
        }

        public void Release(int phId)
        {
            if (Taken && TakenBy == phId)
            {
                Taken = false;
                TakenBy = -1;
            }
        }
    }
}
