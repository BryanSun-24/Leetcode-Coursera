using System;
using System.Collections.Generic;
using System.IO;


namespace Coursera{

/*
The file contains an adjacency list representation of an undirected weighted graph with 200 vertices labeled 1 to 200.  
Each row consists of the node tuples that are adjacent to that particular vertex along with the length of that edge. 
For example, the 6th row has 6 as the first entry indicating that this row corresponds to the vertex labeled 6. 
The next entry of this row "141,8200" indicates that there is an edge between vertex 6 and vertex 141 that has length 8200.  
The rest of the pairs of this row indicate the other vertices adjacent to vertex 6 and the lengths of the corresponding edges.

Your task is to run Dijkstra's shortest-path algorithm on this graph, using 1 (the first vertex) as the source vertex, and to compute the shortest-path distances between 1 and every other vertex of the graph.
If there is no path between a vertex v and vertex 1, we'll define the shortest-path distance between 1 and v to be 1000000. 
*/
    public class Dijkstra{
        int INF = Int32.MaxValue;
        public int dijkstra_shortest(){ // process data
            var textFile = "./dijkstraData.txt";
            string[] lines = File.ReadAllLines(textFile);

            // Read files and convert to dictionary object
            List<Dictionary<int, int>> adj = new List<Dictionary<int, int>>(); 
            for (int i = 0;i < lines.Length; ++i){
                var new_arr = lines[i].Split('\t'); // split will leave the last element of array a space string " " because of the txt format of input file, there is a extra space in the end of each line
                var d = new Dictionary<int, int>();
                for(int j = 1;j < new_arr.Length-1; ++j){
                    var split = new_arr[j].Split(',');
                    d.Add(Int32.Parse(split[0]),Int32.Parse(split[1]));
                }
                adj.Add(d);
            }
            int[] distance = algorithm(adj);
            return 0;
            // by now, i have store all edges for each vertex into a list of Dictionary 
        }

        public int[] algorithm(List<Dictionary<int, int>> adj){
            int[] explore = new int[200];
            int[] distance = new int[200];
            int start = 0;
            explore[start] = 1;
            // First we need to update our distance array before we run algorithm, we store all 起点可以到达的顶点的距离
            for(int i = 0;i < distance.Length; ++i){
                if(adj[start].ContainsKey(i+1)){
                    int value;
                    if(adj[start].TryGetValue(i+1, out value)){
                        distance[i] = value;
                    }
                } else{
                    distance[i] = INF; //如果起点到不了那个点，那么我们就把到那个点的距离设置为infinite
                }
            }
            distance[start] = 0; // 起点到自己的距离是0
            
            for(int i = 0;i < adj.Count; ++i){ // 开始循环，每一次循环增加一个点explored
                int mindistance = INF;
                for(int j = 0;j < distance.Length; ++j){ // 循环里面第一个loop, 我们选择一个未被explore的并且在所有顶点中，到达它的距离最短的点 当作新的起点
                    if(explore[j] == 0 && distance[j] < mindistance){
                        mindistance = distance[j];
                        start = j;
                    }
                }
                explore[start] = 1; // loop结束我们把新的起点 改为explored

                for(int j = 0;j < distance.Length; ++j){ // 第二个loop，因为我们更新了起点，那么因为新的起点有它自己的edge， 所以我们可以通过这些edge更新到达其他顶点的最短距离， 举个例子。 如果原来的起点为s, 新的起点是v，然后v->w 是一条edge长度为10， 那么在原来的起点是s时，还无法到达点w，所以distance[w] = infinite， 现在更新起点到了v， 所以w可以被reach到，所以distance[w] = distance[v] + vw 
                    if(adj[start].ContainsKey(j+1)){
                        int value;
                        if(adj[start].TryGetValue(j+1, out value)){
                            if(explore[j] == 0 && distance[j] > distance[start]+value){ //因为已经走过的顶点肯定不会再走一次，所以不考虑explored过的顶点，所以如果对于未explore的顶点，如果新的起点到达它的距离比原先到达它的距离更短，那么更新distance
                                distance[j] = distance[start]+value;
                            }
                        }
                    }
                }

            }
            return distance;
        }

    }
}