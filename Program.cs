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

            //var _quicksort = new QuickSort();
            //var sol = _quicksort.QuickSorts();
            
            //var _karminCut = new karget_MinCut();
            //var sol = _karminCut.karget_MinCuts();
            
            //var _kosarajust = new kosarajus();
            //var sol = _kosarajust.kosarajus_twopass();

            //var _dijkstra = new Dijkstra{};
            //var sol = _dijkstra.dijkstra_shortest();
            
            var _median = new median{};
            var sol = _median.process();
            Console.WriteLine(sol);
        }
    }
}