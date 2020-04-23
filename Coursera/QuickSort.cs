using System;
using System.Collections.Generic;
using System.IO;

namespace Coursera
{
    /*
    The file contains all of the integers between 1 and 10,000 (inclusive, with no repeats) in unsorted order. The integer in the ith row of the file gives you the ith entry of an input array.
    Your task is to compute the total number of comparisons used to sort the given input file by QuickSort. As you know, the number of comparisons depends on which elements are chosen as pivots, so we'll ask you to explore three different pivoting rules.
    You should not count comparisons one-by-one. Rather, when there is a recursive call on a subarray of length m, you should simply add m−1 to your running total of comparisons. (This is because the pivot element is compared to each of the other 
    m−1 elements in the subarray in this recursive call.)

    Q1. 
        For the first part of the programming assignment, you should always use the first element of the array as the pivot element.
    
    Q2.
        Compute the number of comparisons (as in Problem 1), always using the final element of the given array as the pivot element. Again, be sure to implement the Partition subroutine exactly as it is described in the video lectures.
        Recall from the lectures that, just before the main Partition subroutine, you should exchange the pivot element (i.e., the last element) with the first element.

    Q3.
        Compute the number of comparisons (as in Problem 1), using the "median-of-three" pivot rule. [The primary motivation behind this rule is to do a little bit of extra work to get much better performance on input arrays that are nearly sorted or reverse sorted.] In more detail, you should choose the pivot as follows. 
        Consider the first, middle, and final elements of the given array. (If the array has odd length it should be clear what the "middle" element is; for an array with even length 2k, use the kth
        element as the "middle" element. So for the array 4 5 6 7, the "middle" element is the second one ---- 5 and not 6!) 
        Identify which of these three elements is the median (i.e., the one whose value is in between the other two), and use this as your pivot. 
        As discussed in the first and second parts of this programming assignment, be sure to implement Partition exactly as described in the video lectures (including exchanging the pivot element with the first element just before the main Partition subroutine).

            EXAMPLE: For the input array 8 2 4 5 7 1 you would consider the first (8), middle (4), and last (1) elements; since 4 is the median of the set {1,4,8}, you would use 4 as your pivot element.
    */
    public class QuickSort
    {
        public int QuickSorts(){
            var textFile = "./QuickSortArray.txt";
            string[] lines = File.ReadAllLines(textFile);
            var copy = new List<int>();
            foreach(var line in lines){
                copy.Add(Int32.Parse(line));
            }
            var arr_q1 = copy.ToArray();
            var arr_q2 = copy.ToArray();
            var arr_q3 = copy.ToArray();
            var q1 = first_pivot(arr_q1, 0, arr_q1.Length - 1);
            var q2 = last_pivot(arr_q2, 0, arr_q2.Length - 1);
            var q3 = middle_pivot(arr_q3, 0, arr_q3.Length - 1);
            return 0;
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

        public int last_pivot(int[] array, int left, int right){
            if (right == left){
                return 0;
            }
            var pivot = array[right];
            array[right] = array[left];
            array[left]= pivot;
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
                var right_comp = last_pivot(array, i + 1, right);
                return right_comp + comparsion;
            } else if(i == right){
                var left_comp = last_pivot(array, left, i - 1);
                return left_comp + comparsion;
            } else {
                var left_comp = last_pivot(array, left, i - 1);
                var right_comp = last_pivot(array, i + 1, right);
                return left_comp + right_comp + comparsion;
            }
        }

        public int middle_pivot(int[] array, int left, int right){
            if (right == left){
                return 0;
            }
            var pivot_index = 0;
            var middle_index = 0;
            int length = right - left;
            if(length % 2 == 0){
                middle_index = (length / 2) + left; // remember add left to its index, because we want to find the middle element's index within right and left.
            } else{
                middle_index = ((length - 1) / 2) + left;
            }

            if((array[left] < array[middle_index] && array[middle_index] < array[right]) || (array[right] < array[middle_index] && array[middle_index] < array[left])){
                pivot_index = middle_index;
            } else if((array[middle_index] < array[left] && array[left] < array[right]) || (array[right] < array[left] && array[left] < array[middle_index])){
                pivot_index = left;
            } else{
                pivot_index = right;
            }

            var pivot = array[pivot_index];
            array[pivot_index] = array[left];
            array[left]= pivot;
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
                var right_comp = middle_pivot(array, i + 1, right);
                return right_comp + comparsion;
            } else if(i == right){
                var left_comp = middle_pivot(array, left, i - 1);
                return left_comp + comparsion;
            } else {
                var left_comp = middle_pivot(array, left, i - 1);
                var right_comp = middle_pivot(array, i + 1, right);
                return left_comp + right_comp + comparsion;
            }
        }
    }
}