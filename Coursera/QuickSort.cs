using System;
using System.Collections.Generic;
using System.IO;

namespace Coursera
{
    public class QuickSort
    {
        public int QuickSorts(){
            var textFile = "./QuickSortArray.txt";
            string[] lines = File.ReadAllLines(textFile);
            var copy = new List<int>();
            foreach(var line in lines){
                copy.Add(Int32.Parse(line));
            }
            var arr = copy.ToArray();
            var total_comparsion = first_pivot(arr, 0, arr.Length - 1);
            return total_comparsion;
        }
        public int first_pivot(int[] array, int left, int right){
            if (right == left){
                return 0;
            }
            var pivot = array[left];
            var i = left + 1;
            var comparsion = 0;
            for (int j = left + 1;j <= right; ++j){
                if (array[j] < pivot){
                    var small = array[j];
                    array[j] = array[i];
                    array[i] = small;
                    i++;
                }
                comparsion++;
            }
            i--; // important here ,remember we want to exclude pivot in following recursion
            array[left] = array[i]; 
            array[i] = pivot;
            if(i == left){
                var right_comp = first_pivot(array, i + 1, right);
                return right_comp + comparsion;
            } else if(i == right){
                var left_comp = first_pivot(array, left, i - 1);
                return left_comp + comparsion;
            } else {
                var left_comp = first_pivot(array, left, i - 1);
                var right_comp = first_pivot(array, i + 1, right);
                return left_comp + right_comp + comparsion;
            }
        }
    }
}