using System;
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
            
            var ans = DFS_Loop_reverse(list_arr,list_arr_reverse,explore, finishing_time, max);
            return 0;
                
        }

        public int DFS_Loop_reverse(List<Dictionary<string,List<string>>> list_arr, List<Dictionary<string,List<string>>> list_arr_reverse, List<int> explore, List<int> finishing_time, int max){

            // i will push index to finishing_time , this case, the one with lowest finishing will be positioned at the back of the finishing_time
            for (int i = max-1;i > -1; --i){
                if(explore[i] == 0){
                    DFS(list_arr,list_arr_reverse,explore,finishing_time,i);
                }
            }

            return 0;
        }
        public void DFS(List<Dictionary<string,List<string>>> list_arr, List<Dictionary<string,List<string>>> list_arr_reverse, List<int> explore, List<int> finishing_time, int node_mum){
            explore[node_mum] = 1;
            for(int j = 0;j < list_arr_reverse[node_mum]["edges"].Count; ++j){
                if(explore[int.Parse(list_arr_reverse[node_mum]["edges"][j]) - 1] == 0){
                    DFS(list_arr,list_arr_reverse,explore,finishing_time, int.Parse(list_arr_reverse[node_mum]["edges"][j]) - 1);
                }
            }
            finishing_time.Add(node_mum); 
        }
    }
}




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
