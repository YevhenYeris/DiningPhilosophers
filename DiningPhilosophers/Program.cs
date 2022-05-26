using Philosophers;
using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            PhProblem problem = new PhProblem(5);
            problem.Start();
            Thread.Sleep(10000);
            problem.Stop();

            /*PhProblem phProblem = new PhProblem(5);
            DiningProblem problem = new DiningProblem(5);
            problem.StartDining();

            Task.Delay(10000).Wait();

            problem.Stop = true;
            foreach (var ph in problem.Philosophers)
                ph.Join();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            foreach (var l in problem.Logs)
                Console.WriteLine($"{l.Attempts} - {l.Eatings}");*/
        }
    }
}
