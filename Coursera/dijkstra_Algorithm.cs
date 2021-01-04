using System;
using System.Collections.Generic;
using System.IO;


namespace Coursera{
    public class Dijkstra{
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
            return 0;
            // by now, i have store all edges for each vertex into a list of Dictionary 
        }

    }
}