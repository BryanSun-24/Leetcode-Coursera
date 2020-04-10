using System;
using System.Collections.Generic;
using System.IO;

namespace Coursera
{
/*
Download the text file(IntegerArray.txt)

This file contains all of the 100,000 integers between 1 and 100,000 (inclusive) in some order, with no integer repeated.

Your task is to compute the number of inversions in the file given, where the ith row of the file indicates the ith entry of an array.

Because of the large size of this array, you should implement the fast divide-and-conquer algorithm covered in the video lectures.

*/
    unsafe class inversion // in able to use pointer, we need to specify class type as unsafe and also change the project property allow unsafe build. i manually add the property in .csproj <AllowUnsafeBlock>
    {
        public string inversions(){
            var textFile = "./integerArray.txt";
            string[] lines = File.ReadAllLines(textFile);
            var copy = new List<int>();
            foreach(var line in lines){
                copy.Add(Int32.Parse(line));
            }
            var arr = copy.ToArray();
            long z = 0;
            long *ptr = &z;
            var sol = algorithm(arr, ptr);
            return z.ToString(); // to be able to see the long-digit answer, i make this return string.
        }

        public int[] algorithm(int[] array, long *k){
            if(array.Length == 1){
                return array;
            }
            int[] combine = new int[array.Length];
            var first_half_len = 0;
            var second_half_len = 0;
            if(array.Length % 2 == 0){
                first_half_len = array.Length/2;
                second_half_len = array.Length/2;
            } else{
                first_half_len = (array.Length/2) + 1;
                second_half_len = array.Length/2;
            }
            int[] first_half = new int[first_half_len];
            int[] second_half = new int[second_half_len];

            for (int i = 0;i < first_half_len;i++){
                first_half[i] = array[i];
            }
            int count = 0;
            for (int j = first_half_len;j < array.Length;j++){
                second_half[count] = array[j];
                count++;
            }

            int[] first_half_sorted = algorithm(first_half, k);
            int[] second_half_sorted = algorithm(second_half, k);

            int first_index = 0;
            int second_index = 0;
            for (int i = 0;i < array.Length; i++){
                if(first_index == first_half_len){
                    combine[i] = second_half_sorted[second_index];
                    second_index++;
                } else if(second_index == second_half_len){
                    combine[i] = first_half_sorted[first_index];
                    first_index++;
                }
                else if(first_half_sorted[first_index] < second_half_sorted[second_index]){
                    combine[i] = first_half_sorted[first_index];
                    first_index++;
                }else{
                    combine[i] = second_half_sorted[second_index];
                    *k = *k + (first_half_len - first_index);
                    second_index++;
                }
            }
            return combine;
        }

    }
}