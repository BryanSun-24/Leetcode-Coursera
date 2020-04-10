using System;

namespace Leetcode {
    public class before_record {

        public int solution(int N, int K) {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int n = 0;
            while(true){
                if (N == 1){
                    return n;
                }
                if ((N % 2 == 0)&(K >= 1)){
                    N = N / 2;
                    K -= 1;
                    n += 1;
                }else {
                    N -= 1;
                    n += 1;
                }
            }
        }
        public int solution(int[] A) {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int count = 0;
            int small_pos = 1;
            int [] new_arr = new int[275665];
            count = A.Length;
            for (int i = 0;i < count; ++i){
                if(A[i] > 0){
                   new_arr[A[i]] += 1;  
                }
            }
            for (int j = 1;j < Int32.MaxValue; j++){
                if (new_arr[j] == 0){
                    small_pos = j;
                    break;
                }
            }
            return small_pos;
        }

        public int[] TwoSum(int[] nums, int target) {
        int[] sum = new int[2];
           for(int i = 0;i < nums.Length; ++i){
               if(target >= 0){
                   if(nums[i] <= target){
                       for(int j = 0;j < nums.Length; ++j){
                           if (i != j){
                               if((nums[i] + nums[j]) == target){
                               sum[0] = i;
                               sum[1] = j;
                              return sum;
                             } 
                           }
                       }
                   }
               }
               if (target < 0){
                   if(nums[i] >= target){
                       for(int j = 0;j < nums.Length; ++j){
                           if (i != j){
                               if((nums[i] + nums[j]) == target){
                               sum[0] = i;
                               sum[1] = j;
                              return sum;
                             } 
                           }
                       }
                   }
               }
           }
        return null;
    }

                // static void Main(string[] args){

                //     ListNode l1 = new ListNode(2);
                //     //l1.next = new ListNode(4);
                //     ListNode l1_2 = new ListNode(4);
                //     l1.next = l1_2;
                //     l1_2.next = new ListNode(3);

                //     ListNode l5 = new ListNode(1);
                //     ListNode l6 = l5;
                //     for(int i = 0; i < 20; ++i)
                //     {
                //         l5.next = new ListNode(0);
                //         l5 = l5.next;
                //     }
                //         l5.next = new ListNode(1);

                //     ListNode l2 = new ListNode(5);
                //     l2.next = new ListNode(6);
                //     l2.next.next = new ListNode(4);

                //     Console.WriteLine("Display list1 : {0} -> {1} -> {2}",l1.val,l1.next.val,l1.next.next.val);
                //     Console.WriteLine("Display list1 : {0} -> {1} -> {2}",l2.val,l2.next.val,l2.next.next.val);
                //     var x = AddTwoNumbers(l6,l2);
                //     for (int i = 0; i<= 30; ++i){
                //         if(x == null){
                //             break;
                //         }else{
                //             Console.WriteLine($"{x.val}");
                //             x = x.next;
                //         }
                //     }
                // }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int tenth = 0;
            if((l1.val + l2.val) >= 10)
            {
                tenth = (l1.val + l2.val) / 10;
            }
            ListNode l3 = new ListNode(l1.val+l2.val);
            if ((l1.val+l2.val) >= 10)
            {   
                l3.val = (l1.val + l2.val) % 10; 
                tenth = (l1.val + l2.val) / 10;
            }
            ListNode direction = l3;
            l1 = l1.next;
            l2 = l2.next;

            for(int i = 0; i < int.MaxValue; ++i)
            {
                if (l1 == null && l2 == null){
                    break;
                } else if (l1 == null && l2 != null){
                    l3.next = new ListNode(l2.val + tenth);
                    if((l2.val + tenth) >= 10){
                            l3.next.val = (l2.val + tenth) % 10;
                            tenth = (l2.val + tenth) / 10;
                    } else{
                        tenth = 0;
                    }
                    l3 = l3.next;
                    l2 = l2.next;                
                } else if(l1 != null && l2 == null){
                        l3.next = new ListNode(l1.val + tenth);
                    if((l1.val + tenth) >= 10){
                            l3.next.val = (l1.val + tenth) % 10;
                            tenth = (l1.val + tenth) / 10;
                    } else{
                        tenth = 0;
                    }
                    l3 = l3.next;
                    l1 = l1.next;                
                }else{
                    l3.next = new ListNode(l1.val + l2.val + tenth);
                    if((l1.val + l2.val + tenth) >= 10){
                        l3.next.val = (l1.val + l2.val + tenth) % 10;
                        tenth = (l1.val + l2.val + tenth) / 10;
                    } else{
                        tenth = 0;
                    }
                    l3 = l3.next;
                    l1 = l1.next;
                    l2 = l2.next;
                }   
            }
        return direction;
        }

        public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
        }
        static int Maxprofit(int[] prices)
        {
            var MaxPro = -1;
            for (int i = 0; i < prices.Length; i++)
            {
                for (int j = i + 1; j < prices.Length; j++)
                {
                    var pro = prices[i] - prices[j];
                    if((pro * -1) >=  MaxPro)
                    {
                        MaxPro = pro * -1;
                    }
                }
            }
            return MaxPro;
        }
    }
}