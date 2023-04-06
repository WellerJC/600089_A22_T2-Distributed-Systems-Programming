using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threads
{
    class ThreadRunner
    {
        private readonly int numberToGenerate = 100;
        private int index = 0;
        private int[] orderedNumbers;
        private Random rng = new Random();
        public void Run()
        {
            orderedNumbers = new int[numberToGenerate];
            Thread[] threads = new Thread[numberToGenerate];

            for(int i = 0; i < numberToGenerate; i++)
            {
                ParameterizedThreadStart threadStart = new ParameterizedThreadStart(AddValue);
                Thread thread = new Thread(threadStart);
                threads[i] = thread;
            }

            for(int i = 0;i < numberToGenerate; i++)
            {
                threads[i].Start(i+1);
            }

            while (true)
            {
                foreach(Thread thread in threads)
                {
                    if (thread.IsAlive)
                    {
                        continue;
                    }
                }
                break;
            }

            foreach(int i in orderedNumbers)
            {
                Console.WriteLine(i);
            }

        }
        public void AddValue(object value)
        {
            orderedNumbers[index] = (int)value;
            Thread.Sleep(rng.Next(6));
            index++;
        }
    }
}
