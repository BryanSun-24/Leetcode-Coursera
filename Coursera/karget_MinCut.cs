using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coursera{
    public class karget_MinCut {
        /*
        The file contains the adjacency list representation of a simple undirected graph. There are 200 vertices labeled 1 to 200. The first column in the file represents the vertex label, and the particular row (other entries except the first column) tells all the vertices that the vertex is adjacent to. So for example, the 6th
         row looks like : "6	155	56	52	120	......". This just means that the vertex with label 6 is adjacent to (i.e., shares an edge with) the vertices with labels 155,56,52,120,......,etc
         Your task is to code up and run the randomized contraction algorithm for the min cut problem and use it on the above graph to compute the min cut. 
         (HINT: Note that you'll have to figure out an implementation of edge contractions. Initially, you might want to do this naively, creating a new graph from the old every time there's an edge contraction. But you should also think about more efficient implementations.) 
        
        */
        public int karget_MinCuts(){
            var textFile = "./kargerMinCut.txt";
            string[] lines = File.ReadAllLines(textFile);

            // Read files and convert to dictionary object
            List<Dictionary<string,List<string>>> list_arr = new List<Dictionary<string,List<string>>>(); 
            for (int i = 0;i < lines.Length; ++i){
                var new_arr = lines[i].Split('\t'); // split will leave the last element of array a space string " " because of the txt format of input file, there is a extra space in the end of each line
                List<string> vertex = new List<string>();
                List<string> edges = new List<string>();
                for(int j = 0;j < new_arr.Length - 1; ++j){ // j < length - 1 because i want to ignore the last space string
                    if(j == 0){
                        vertex.Add(new_arr[j]);
                    } else{
                        edges.Add(new_arr[j]);
                    }
                }
                Dictionary<string, List<string>> v = new Dictionary<string, List<string>>() {
                    {"vertex", vertex},
                    {"edges", edges}
                    };
                list_arr.Add(v);
            }

            var min = 10000;
            // in the lecture, if we choose Trial size N = n^2, the fail probability is 1/e, we should choose N = 40000
            // in the lecture, if we choose Trial size N = n^2 * ln(n), the fail probability is 1/n, we should choose N = 200000
            // So i choose 300000, pretty high probability of success
            // the answer is 17

            for(int i = 0; i < 300000; ++i){ 
                var ans = algorithm(list_arr);
                if(ans < min){
                    min = ans;
                }
            }
            return min;
        }

        public int algorithm(List<Dictionary<string,List<string>>> list_arr){
            Random random = new Random();
            
            // Only when contracts to the last two vertices, it stoped. 
            // actually two vertices means two sets of vertices, each vertices are made by contraction from other vertices, 
            // The last two vertices' neighbours are the cuts, and since only two vertices left, all edges of one vertex should point to the other vertex,
            // and for each of them, they should have same number of edges because that's the edge connect themself, last two vertex.
            // We can see the ingradients of each sets by the vertex of last two vertices, i managed vertex name by adding & to each contraction. 
            //For ex, vertex 1 contract with vertex 2, new vertex = 1&2
            
            while(list_arr.Count > 2){
                int dic_length = list_arr.Count;
                int Node_index = random.Next(0,dic_length-1);
                int Neighbour_length = list_arr[Node_index]["edges"].Count;
                int randomNeighbour = random.Next(0, Neighbour_length - 1);
                string Neighbour_vertex = list_arr[Node_index]["edges"][randomNeighbour];


                /// Find node1 and node2
                var node1 = list_arr[Node_index];
                var node2_index = 0;
                for(int j = 0;j < list_arr.Count; ++j){
                    if(list_arr[j]["vertex"][0] == Neighbour_vertex){
                        node2_index = j;
                        break;
                    }
                }

                var node2 = list_arr[node2_index];

                // Create new merged node
                Dictionary<string,List<string>> new_node = new Dictionary<string,List<string>>();
                var new_vertex = new List<string>();
                new_vertex.Add(node1["vertex"][0] + '&' + node2["vertex"][0]);
                var new_edges = new List<string>();

                // remove edges between node1 and node2
                for(int i = 0;i < node1["edges"].Count; ++i){
                    if(node1["edges"][i] != node2["vertex"][0]){
                        new_edges.Add(node1["edges"][i]);
                    }
                }
                for(int i = 0;i < node2["edges"].Count;++i){
                    if(node2["edges"][i] != node1["vertex"][0]){
                        new_edges.Add(node2["edges"][i]);
                    }
                } 
                //
                new_node.Add("vertex", new_vertex);
                new_node.Add("edges", new_edges);

                var node1_neighbour = node1["edges"];
                var node2_neighbour = node2["edges"];

                /// loop through node1's neighbour and change their neighbours' edges to point to new node
                for(int i = 0;i < node1_neighbour.Count; ++i){
                    for(int j = 0;j < list_arr.Count; ++j){
                        if(list_arr[j]["vertex"][0] == node1["edges"][i]){
                            for(int z = 0; z < list_arr[j]["edges"].Count; ++z){
                                if(list_arr[j]["edges"][z] == node1["vertex"][0]){
                                    list_arr[j]["edges"][z] = node1["vertex"][0] + '&' + node2["vertex"][0];
                                    break;
                                }
                            }
                        }
                    }
                }

                /// loop through node2's neighbour and change their neighbours' edges to point to new node
                for(int i = 0;i < node2_neighbour.Count; ++i){
                    for(int j = 0;j < list_arr.Count; ++j){
                        if(list_arr[j]["vertex"][0] == node2["edges"][i]){
                            for(int z = 0; z < list_arr[j]["edges"].Count; ++z){
                                if(list_arr[j]["edges"][z] == node2["vertex"][0]){
                                    list_arr[j]["edges"][z] = node1["vertex"][0] + '&' + node2["vertex"][0];
                                    break;
                                }
                            }
                        }
                    }
                }

                // Remove two old node and then add new created node to list_arr
                list_arr.Remove(node1);
                list_arr.Remove(node2);
                list_arr.Add(new_node);
            }
            /////////
            // Finish remove node1 and node2, add new node to the list
            /////////

            
            return list_arr[0]["edges"].Count;
        }
    }
}
