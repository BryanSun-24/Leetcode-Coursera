using System;
using Coursera;
using Leetcode;

namespace Algorithm
{
    public class Program
    {
        static void Main(string[] args)
        {
            //int sol = _coursera.kara_mult(55555555, 66666666);
            //var _inversion = new inversion();
            //var sol = _inversion.inversions();
            //var a = 4322 % 100;

            var _quicksort = new QuickSort();
            var sol = _quicksort.QuickSorts();
            Console.WriteLine(sol);
        }
    }
}