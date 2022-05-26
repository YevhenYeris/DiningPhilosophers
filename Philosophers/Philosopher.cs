using System;
using System.Threading;

namespace Philosophers
{
    public enum State
    {
        Thinking,
        Hungry,
        Eating
    }

    public class Thinker
    {
        public State State { get; set; }

        private Thread _thread;

        public Thinker(Thread thread)
        {
            _thread = thread;
        }

        public void Think()
        {
        }

        private void ThinkThread()
        {
            Thread.Sleep(1000);
        }

        public void Eat()
        {

        }
    }
}
