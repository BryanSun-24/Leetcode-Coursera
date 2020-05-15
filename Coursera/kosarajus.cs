using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Coursera{
    public class kosarajus {

        public int kosarajus_twopass(){
            var textFile = "./SCC.txt";
            string[] lines = File.ReadAllLines(textFile);
            //// Read files and convert to dictionary object ----------------------------------------------------------------------------
            List<Dictionary<string,List<string>>> list_arr = new List<Dictionary<string,List<string>>>(); 
            List<Dictionary<string,List<string>>> list_arr_reverse = new List<Dictionary<string,List<string>>>(); 
            // Create a list of integer which represents explored vertex
            List<int> explore = new List<int>();
            List<int> finishing_time = new List<int>();

            //Create two empty list which each index represents vertex
            List<List<string>> list_edges = new List<List<string>>();
            List<List<string>> list_edges_reverse = new List<List<string>>();
            var max = 0;
            for(int i = 0;i < lines.Length; ++i){
                var vertex = lines[i].Split(' ');
                if (int.Parse(vertex[0]) > max){
                    max = int.Parse(vertex[0]);
                }
            }

            // adding create enough memory space for the list 
            for(int i = 1;i < max + 1; ++i){
                List<string> vertex = new List<string>();
                List<string> list_e = new List<string>();
                // each declare of List<string> is a individual pointer to certain address, we can't assign list_e to both list_edges and list_edges_reverse, because they will share the same address, share same chanegs to data stored in memory
                List<string> list_r = new List<string>(); 
                vertex.Add(i.ToString());
                Dictionary<string, List<string>> v = new Dictionary<string, List<string>>() {
                     {"vertex", vertex}
                };
                // each Dictionary is a individual pointer to certain address, we can't assign v to both list_arr and list_arr_reverse, because they will share the same address, share same chanegs to data stored in memory
                 Dictionary<string, List<string>> v2 = new Dictionary<string, List<string>>() {
                     {"vertex", vertex}
                };
                list_arr.Add(v); 
                list_arr_reverse.Add(v2);
                list_edges.Add(list_e);
                list_edges_reverse.Add(list_r);
                explore.Add(0);
            }

            // adding edges for each vertex to the list, either reverse case or normal case
            for(int i = 0;i < lines.Length; ++i){
                var vertex = lines[i].Split(' ');
                list_edges[int.Parse(vertex[0]) - 1].Add(vertex[1]);
            }

            for(int i = 0;i < max; ++i){
                list_arr[i].Add("edges", list_edges[i]);
            }

            list_edges = null;
            GC.Collect();
            
            for(int i = 0;i < lines.Length; ++i){
                var vertex = lines[i].Split(' ');
                list_edges_reverse[int.Parse(vertex[1]) - 1].Add(vertex[0]);
            }

             for(int i = 0;i < max; ++i){
                list_arr_reverse[i].Add("edges", list_edges_reverse[i]);
            }

            list_edges_reverse = null;
            GC.Collect();

            //------------------------------------------------------------------------------------------------------------------------------------------------
            
            DFS_Loop_reverse(list_arr,list_arr_reverse,explore, finishing_time, max);
            for(int i = 0;i < max; ++i){
                explore[i] = 0;
            }

            for(int i = 0;i < finishing_time.Count; ++i){
                if (finishing_time[i] == 875709){
                    continue;
                }
            }
            var ans = DFS_Loop(list_arr, explore, finishing_time, max);
            return 0;
                
        }

        public void DFS_Loop_reverse(List<Dictionary<string,List<string>>> list_arr,List<Dictionary<string,List<string>>> list_arr_reverse,List<int> explore, List<int> finishing_time, int max){
            for(int i = max-1;i > -1; --i){
                if(explore[i] == 0){
                    Stack<int> stack = new Stack<int>();
                    stack.Push(i+1);
                    explore[i] = 1;
                    DFS(list_arr, list_arr_reverse, explore, finishing_time, i, stack);
                }
            }
        }

        public void DFS(List<Dictionary<string,List<string>>> list_arr,List<Dictionary<string,List<string>>> list_arr_reverse,List<int> explore, List<int> finishing_time, int node_num, Stack<int> stack){
            Stack<int> visited = new Stack<int>();
            int stack_num = 1;
            int visit_num = 0;
            while(true){
                if(stack.Count == 0){
                    break;
                }
                var pop = stack.Pop();
                var length = list_arr_reverse[pop - 1]["edges"].Count;
                for(int i = 0;i < length; ++i){
                    if(explore[int.Parse(list_arr_reverse[pop - 1]["edges"][i]) - 1] == 0){
                        stack.Push(int.Parse(list_arr_reverse[pop - 1]["edges"][i]));
                        stack_num++;
                        explore[int.Parse(list_arr_reverse[pop - 1]["edges"][i]) - 1] = 1;
                    }
                }
                visited.Push(pop);
                visit_num ++;
            }

            if(stack_num != visit_num){
                var x = 0;
            }
            var visit_count = visited.Count; // must have this line, C# for loop 中间的condiction 是 dynamic的， 如果下面的forloop 我写j < visited.Count， 因为随着我们每次visited的pop
            // 会减小visited的size ， 因为C#的 for loop 每一遍都会重新运行 一次visited.count 所以到最后的结局就是， visited.count每一次都会自动减小，然后我们就read不全我们的data， 我他妈刚才在这儿 一脸懵逼找bug找了1个多小时
            // ！！！ 艹！！！！！！！
            for(int j = 0;j < visit_count; ++j){
                var pop = visited.Pop();
                finishing_time.Add(pop);
            }
        }

        public List<int> DFS_Loop(List<Dictionary<string,List<string>>> list_arr, List<int> explore, List<int> finishing_time, int max){
            List<int> SCC = new List<int>();
            for(int i = max-1;i > -1; --i){
                if(explore[finishing_time[i] - 1] == 0){
                    Stack<int> stack = new Stack<int>();
                    stack.Push(finishing_time[i]);
                    explore[finishing_time[i] - 1] = 1;
                    var num = DFS_Count(list_arr, explore, finishing_time, i, stack);
                    SCC.Add(num);
                }
            }
            return SCC;
        }

         public int DFS_Count(List<Dictionary<string,List<string>>> list_arr, List<int> explore, List<int> finishing_time, int node_num, Stack<int> stack){
            Stack<int> visited = new Stack<int>();
            while(true){
                if(stack.Count == 0){
                    break;
                }
                var pop = stack.Pop();
                var length = list_arr[pop - 1]["edges"].Count;
                for(int i = 0;i < length; ++i){
                    if(explore[int.Parse(list_arr[pop - 1]["edges"][i]) - 1] == 0){
                        stack.Push(int.Parse(list_arr[pop - 1]["edges"][i]));
                        explore[int.Parse(list_arr[pop - 1]["edges"][i]) - 1] = 1;
                    }
                }
                visited.Push(pop);
            }
            return visited.Count;
        }

    }
}




// Recursion is causing stackoverflow
        // public int DFS_Loop_reverse(List<Dictionary<string,List<string>>> list_arr, List<Dictionary<string,List<string>>> list_arr_reverse, List<int> explore, List<int> finishing_time, int max){
        //     // i will push index to finishing_time , this case, the one with lowest finishing will be positioned at the back of the finishing_time
        //     for (int i = max-1;i > -1; --i){
        //         if(explore[i] == 0){
        //             DFS(list_arr,list_arr_reverse,explore,finishing_time,i);
        //         }
        //     }

        //     return 0;
        // }
        
        // public void DFS(List<Dictionary<string,List<string>>> list_arr, List<Dictionary<string,List<string>>> list_arr_reverse, List<int> explore, List<int> finishing_time, int node_mum){
        //     explore[node_mum] = 1;
        //     for(int j = 0;j < list_arr_reverse[node_mum]["edges"].Count; ++j){
        //         if(explore[int.Parse(list_arr_reverse[node_mum]["edges"][j]) - 1] == 0){
        //             DFS(list_arr,list_arr_reverse,explore,finishing_time, int.Parse(list_arr_reverse[node_mum]["edges"][j]) - 1);
        //         }
        //     }
        //     finishing_time.Add(node_mum); 
        // }




// Read files and convert to dictionary object
            // List<Dictionary<string,List<string>>> list_arr = new List<Dictionary<string,List<string>>>(); 
            // int local = -1;
            // for (int i = 0;i < lines.Length; ++i){
            //     var new_arr = lines[i].Split(' '); // split will leave the last element of array a space string " " because of the txt format of input file, there is a extra space in the end of each line
            //     List<string> vertex = new List<string>();
            //     List<string> edges = new List<string>();
            //     int first_index = int.Parse(new_arr[0]);
            //     local = first_index;
            //     int j = i;
            //     vertex.Add(new_arr[0]);
            //     // go through the following line which has same vertex
            //     while(j < lines.Length){
            //         var new_arrr = lines[j].Split(' ');
            //         if(int.Parse(new_arrr[0]) != local){
            //             // stop when encounter a different vertex, mutate i position to new position, so that in next for loop, i will start at new vertex
            //             i = j - 1;
            //             break;
            //         } else {
            //             edges.Add(new_arrr[1]);
            //             j++;
            //         }
            //     }
            //     Dictionary<string, List<string>> v = new Dictionary<string, List<string>>() {
            //         {"vertex", vertex},
            //         {"edges", edges}
            //     };
            //     list_arr.Add(v);        
            // }
