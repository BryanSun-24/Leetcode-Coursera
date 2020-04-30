using System;
using System.Collections.Generic;
using System.IO;

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
            string[][] new_lines = new string[200][];
            List<Dictionary<string,List<string>>> list_arr = new List<Dictionary<string,List<string>>>(); 
            for (int i = 0;i < new_lines.Length; ++i){
                new_lines[i] = new string[90];
            }

            for (int i = 0;i < lines.Length; ++i){
                var new_arr = lines[i].Split('\t');
                List<string> vertex = new List<string>();
                List<string> edges = new List<string>();
                for(int j = 0;j < new_arr.Length; ++j){
                    if(j == 0){
                        vertex.Add(new_arr[j]);
                    } else{
                        new_lines[i][j] = new_arr[j];
                        edges.Add(new_arr[j]);
                    }
                }
                Dictionary<string, List<string>> v = new Dictionary<string, List<string>>() {
                    {"vertex", vertex},
                    {"edges", edges}
                    };
                list_arr.Add(v);
            }
            
            return 0;
        }
    }
}
