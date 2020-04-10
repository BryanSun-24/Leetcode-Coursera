using System;
using Coursera;
using Leetcode;

namespace Algorithm
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _inversion = new inversion();
            //int sol = _coursera.kara_mult(55555555, 66666666);
            var sol = _inversion.inversions();
            //var a = 4322 % 100;
            Console.WriteLine(sol);
        }
    }
}