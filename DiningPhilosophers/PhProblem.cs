using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    public class Philosopher
    {
        public Thread Thread;
        public object LeftFork;
        public object RightFork;

        public Philosopher(object leftFork, object rightFork, int index)
        {
            LeftFork = leftFork;
            RightFork = rightFork;

            Thread = new Thread(Philosophize) { Name = $"{index}" };
        }

        public void Philosophize()
        {
            while (true)
            {
                Think();
                //lock(PhProblem.TakeForksLocker)
                lock (LeftFork)
                {
                    //Console.WriteLine($"{Thread.Name} locks left fork {LeftFork.GetHashCode()}");
                    lock (RightFork)
                    {
                        //Console.WriteLine($"{Thread.Name} locks right fork {RightFork.GetHashCode()}");
                        Eat();
                    }
                }
            }
        }

        public void Think()
        {
            Console.WriteLine($"Ph {Thread.Name} is thinking");
            Thread.Sleep(1000);
        }

        public void Eat()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ph {Thread.Name} is eating");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
    }

    public class PhProblem
    {
        public static object TakeForksLocker = new object();

        public List<Philosopher> Philosophers { get; set; } = new List<Philosopher>();
        public List<object> Forks { get; set; } = new List<object>();

        public PhProblem(int phCount)
        {
            Enumerable.Range(0, phCount).ToList().ForEach(i => Forks.Add(new object()));
            for (int i = 0; i < phCount; ++i)
            {
                Philosophers.Add(new Philosopher(Forks[i], Forks[i == 0 ? phCount - 1 : i - 1], i));
            }
        }

        public void Start()
        {
            Philosophers.ForEach(ph => ph.Thread.Start());
        }

        public void Stop()
        {
            Philosophers.ForEach(ph => ph.Thread.Join());
        }
    }
}
