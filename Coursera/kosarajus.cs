using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coursera{
    public class kosarajus {

        /*
        
        The file contains the edges of a directed graph. Vertices are labeled as positive integers from 1 to 875714. 
        Every row indicates an edge, the vertex label in first column is the tail and the vertex label in second column is the head (recall the graph is directed, and the edges are directed from the first column vertex to the second column vertex). So for example, the 11th
        row looks liks : "2 47646". This just means that the vertex with label 2 has an outgoing edge to the vertex with label 47646
        Your task is to code up the algorithm from the video lectures for computing strongly connected components (SCCs), and to run this algorithm on the given graph.
        
        Output Format: You should output the sizes of the 5 largest SCCs in the given graph, in decreasing order of sizes, separated by commas (avoid any spaces). 
        So if your algorithm computes the sizes of the five largest SCCs to be 500, 400, 300, 200 and 100, then your answer should be "500,400,300,200,100"


        WARNING: This is the most challenging programming assignment of the course. Because of the size of the graph you may have to manage memory carefully. The best way to do this depends on your programming language and environment,
         and we strongly suggest that you exchange tips for doing this on the discussion forums.
        */
        
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
            //I am using Stack data structure to run DFS because if i do recursion instead of Stack, program will run into stackoverflow due to high number of recursion occuring during the executaiton
            // Stackoverflow is an error using too much memory, like too many recursion, taking up stack (memory) size, there could be a way in C# to increase the stack size, but using while loop is better 
            // way to avoid extra memory and run faster, so i stop to use stack data sctructure 
            // We dont need to worry about the finsihing time  order, beacuse we are finding SCC group, no matter what's the order inside a SCC, the largest finishing time in a SCC is always the same value, so order does not matter,
            // Tha's why we dont have to use recursion dfs to implement our question

            DFS_Loop_reverse(list_arr,list_arr_reverse,explore, finishing_time, max);
            for(int i = 0;i < max; ++i){
                explore[i] = 0;
            }

            var ans = DFS_Loop(list_arr, explore, finishing_time, max);
            ans.Sort(); // sort the list of size of SCC


            int one = 0;
            int two = 0;
            int three = 0;
            int four = 0;
            int five = 0;
            for(int j = 0;j < ans.Count;++j){
                if(ans[j] >= one){
                    five = four;
                    four = three;
                    three = two;
                    two = one;
                    one = ans[j];
                }
            }
            return 0;
                
        }

         //Stack DFS on reverse order graph
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

        // DFS: first time running through the graph, assigning finsih time to each vertex, since it is using stack the first one been vistied will always has to large finsihing time.
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

            var visit_count = visited.Count; // must have this line, C# for loop 中间的condiction 是 dynamic的， 如果下面的forloop 我写j < visited.Count， 因为随着我们每次visited的pop
            // 会减小visited的size ， 因为C#的 for loop 每一遍都会重新运行 一次visited.count 所以到最后的结局就是， visited.count每一次都会自动减小，然后我们就read不全我们的data， 我他妈刚才在这儿 一脸懵逼找bug找了1个多小时
            // ！！！ 艹！！！！！！！

            for(int j = 0;j < visit_count; ++j){
                var pop = visited.Pop();
                finishing_time.Add(pop);
            }
        }

        // Stack DFS on normal directrion graph
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

        // DFS_Count(): Returns a list of size of SCC from VIsited.Count
        // Visited stores all vertex in one SCC
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




        // Recursion is causing stackoverflow, so i abondoned it
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
