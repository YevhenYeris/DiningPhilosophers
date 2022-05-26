using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Philosophers
{
    public class DiningProblem
    {
        public int PhNumber { get; }
        private object locker = new object();

        public List<Fork> Forks = new List<Fork>();
        public List<Thread> Philosophers = new List<Thread>();

        public List<LogRecord> Logs = new List<LogRecord>();

        public bool Stop { get; set; }

        Mutex mutex = new Mutex();

        private int Time { get; set; }

        public DiningProblem(int N)
        {
            PhNumber = N;
        }

        public void StartDining()
        {
            Init();
            Philosophers.ForEach(t => t.Start());
        }

        private void Init()
        {
            for (int i = 1; i <= PhNumber; ++i)
            {
                Forks.Add(new Fork(i));
                Logs.Add(new LogRecord());
                int n = i;
                Philosophers.Add(new Thread(() => Philosophize(n)) { Name = $"Philosopher {i}" });
            }
        }

        public void Philosophize(int phInd)
        {
            while (!Stop)
            {
                Think(phInd);
                if (TakeForks(phInd))
                {
                    Eat(phInd);
                    ReleaseForks(phInd);
                }
            }
        }

        private void Think(int phId)
        {
            SetTime();
            Logs[phId - 1].ThinkTime += Time;
            Thread.Sleep(Time);
        }

        private bool TakeForks(int phInd)
        {
            Fork leftFork = Forks[phInd-1];
            Fork rightFork = Forks[phInd == 1 ? PhNumber - 1 : phInd - 2];

            //mutex.WaitOne();
            //lock (locker)
            //{
                Logs[phInd - 1].Attempts += 1;

                Console.WriteLine($"{Thread.CurrentThread.Name} is taking forks");
                Forks.ForEach(f => Console.Write($"{f.Index}-{f.Taken} "));
            
                leftFork.Take(phInd);
                rightFork.Take(phInd);

            bool taken = leftFork.TakenBy == phInd && leftFork.TakenBy == phInd;


                if (taken)
                {
                    SetTime();
                    Logs[phInd - 1].Eatings += 1;
                    Logs[phInd - 1].EatTime += Time;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{Thread.CurrentThread.Name} is eating for {Time}");
                    Forks.ForEach(f => Console.Write($"{f.Index}-{f.Taken} "));
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ResetColor();
            //}
            //mutex.ReleaseMutex();

            return taken;
        }

        private void ReleaseForks(int phInd)
        {
            Fork leftFork = Forks[phInd-1];
            Fork rightFork = Forks[phInd == 1 ? PhNumber - 1 : phInd - 2];

            leftFork.Release(phInd);
            rightFork.Release(phInd);
        }

        private void Eat(int phId)
        {
            Logs[phId - 1].EatTime += Time;
            Thread.Sleep(Time);
        }

        private void SetTime()
        {
            Random rand = new Random();
            Time = rand.Next(1000, 3000);
            Time = 1000;
        }
    }
}
