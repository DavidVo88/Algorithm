using System;
using System.Diagnostics;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("There are 3 modes to calculate Fibonacci number at a given index");
            Console.WriteLine("1. Recursion: (R or r)");
            Console.WriteLine("2. Momoization Recursion: (M or m)");
            Console.WriteLine("3. Bottom Up Recursion: (B or b)");
            Console.WriteLine();
            Console.WriteLine("===========For Example=============");
            Console.WriteLine("To calculate the fibonacci number at the index of 50 with RECURSION mode  ");
            Console.WriteLine("Input: R20");            
            Console.WriteLine("=================================");

            Console.WriteLine();


            var fibo = new Fibo();
            while (true)
            {
                Console.Write("Input: ");
                var input = Console.ReadLine();

                var mode = input.Substring(0, 1);

                var position = input.Substring(1);
                int.TryParse(position, out var n);

                var stopwatch = Stopwatch.StartNew();
                ulong?[] arr = new ulong?[n];
                ulong result = 0;
                switch (mode)
                {
                    case "R":
                    case "r":
                        result = fibo.RecuriveCalc(n);
                        break;
                    case "M":
                    case "m":                        
                        result = fibo.MemoizeRecursiveCalc(n - 1, arr);
                        break;
                    case "B":
                    case "b":                        
                        result = fibo.BottomUpCalc(n - 1, arr);
                        break;
                    case "E":
                    case "e":
                        return;
                }

                stopwatch.Stop();

                Console.WriteLine($"Result: {result} - Time: {stopwatch.Elapsed}");
                Console.WriteLine("--------------------------------------------");
            }
        }
    }

    public class Fibo
    {
        public ulong RecuriveCalc(int n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }

            return RecuriveCalc(n - 1) + RecuriveCalc(n - 2);
        }

        public ulong MemoizeRecursiveCalc(int n, ulong?[] arr)
        {
            if (arr[n] != null)
            {
                return arr[n].Value;
            }

            if (n == 0 || n == 1)
            {
                return 1;
            }

            var i = MemoizeRecursiveCalc(n - 1, arr) + MemoizeRecursiveCalc(n - 2, arr);
            arr[n] = i;

            return i;
        }

        public ulong BottomUpCalc(int index, ulong?[] arr)
        {
            if (arr[index] != null)
            {
                return arr[index].Value;
            }

            arr[0] = 1;
            arr[1] = 1;

            for (int i = 2; i <= index; i++)
            {
                arr[i] = this.BottomUpCalc(i - 1, arr) + this.BottomUpCalc(i - 2, arr);
            }

            return arr[index].Value;
        }
    }
}
