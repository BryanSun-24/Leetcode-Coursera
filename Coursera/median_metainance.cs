using System;
using System.Collections.Generic;
using System.IO;


namespace Coursera{
    public class median{
        public int process(){
            var textFile = "./Median.txt";
            string[] lines = File.ReadAllLines(textFile);
            int a = Algorithm(lines);
            return 0;
        }
        public int Algorithm(string[] lines){
            List<int> max_Heap = new List<int>();
            List<int> min_Heap = new List<int>();
            int sum = Int32.Parse(lines[0]);
            max_Heap.Add(Int32.Parse(lines[0]));
            for(int i = 1; i < lines.Length; ++i){
                if(Int32.Parse(lines[i]) <= max_Heap[0]){
                    Insert_maxHeap(max_Heap, Int32.Parse(lines[i]));
                } else{
                    Insert_minHeap(min_Heap, Int32.Parse(lines[i]));
                }
                if(max_Heap.Count - min_Heap.Count > 1){
                    int temp = max_Heap[0];
                    Insert_minHeap(min_Heap, temp);
                    temp = max_Heap[max_Heap.Count-1];
                    max_Heap.RemoveAt(max_Heap.Count-1);
                    max_Heap[0] = temp;
                    int index = 0;
                    int largest = index;
                    while(true){
                        int left_child = 2*(index+1)-1;
                        int right_child = 2*(index+1);
                        if(right_child < max_Heap.Count && max_Heap[right_child] > max_Heap[index]){ //这里为什么我不担心heap buff flow， 为什么不担心max_heap[right_child] access 不到， 因为if condition, 如果right_child < max_Heap.count, 他就会直接= false，不会继续往下运行，所以也就不会acces不存在的那个位置
                            largest = right_child;
                        }
                        if(left_child < max_Heap.Count && max_Heap[left_child] > max_Heap[largest]){ //注意这里是在拿left_child 和largest比较，这样就可以保证如果left child 更大，那就选leftchild 换，不然就还选right child 换。
                            largest = left_child;
                        }
                        if(largest == index){break;}
                        temp = max_Heap[index];
                        max_Heap[index] = max_Heap[largest];
                        max_Heap[largest] = temp; 
                        index = largest;
                    }
                } else if(min_Heap.Count - max_Heap.Count > 1){
                    int temp = min_Heap[0];
                    Insert_maxHeap(max_Heap, temp);
                    temp = min_Heap[min_Heap.Count-1];
                    min_Heap.RemoveAt(min_Heap.Count-1);
                    min_Heap[0] = temp;
                    int index = 0;
                    int smallest = index;
                    while(true){
                        int left_child = 2*(index+1)-1;
                        int right_child = 2*(index+1);
                        if(right_child < min_Heap.Count && min_Heap[right_child] < min_Heap[index]){
                            smallest = right_child;
                        }
                        if(left_child < min_Heap.Count && min_Heap[left_child] < min_Heap[smallest]){
                            smallest = left_child;
                        }
                        if(smallest == index){break;}
                        temp = min_Heap[index];
                        min_Heap[index] = min_Heap[smallest];
                        min_Heap[smallest] = temp; 
                        index = smallest;
                    }
                }

                if(((max_Heap.Count + min_Heap.Count)%2 == 0) || (max_Heap.Count > min_Heap.Count)){
                    sum = sum + max_Heap[0];
                } else {
                    sum = sum + min_Heap[0];
                }
            }
            return sum;
        }
        public void Insert_maxHeap(List<int> max_Heap, int num){
            max_Heap.Add(num);
            int index = max_Heap.Count-1;
            while(max_Heap[index] > max_Heap[(index-1)/2]){
                int temp = max_Heap[index];
                max_Heap[index] = max_Heap[(index-1)/2];
                max_Heap[(index-1)/2] = temp;
                index = (index-1)/2;
            }
        }
        public void Insert_minHeap(List<int> min_Heap, int num){
            min_Heap.Add(num);
            int index = min_Heap.Count-1;
            while(min_Heap[index] < min_Heap[(index-1)/2]){
                int temp = min_Heap[index];
                min_Heap[index] = min_Heap[(index-1)/2];
                min_Heap[(index-1)/2] = temp;
                index = (index-1)/2;
            }
        }
    }
}