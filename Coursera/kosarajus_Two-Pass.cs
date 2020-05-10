using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coursera{
    public class kosarajus {

        public int kosarajus_twopass(){
            var textFile = "./SCC.txt";
            string[] lines = File.ReadAllLines(textFile);

            // Read files and convert to dictionary object
            List<Dictionary<string,List<string>>> list_arr = new List<Dictionary<string,List<string>>>(); 
            int local = -1;
            for (int i = 0;i < lines.Length; ++i){
                var new_arr = lines[i].Split(' '); // split will leave the last element of array a space string " " because of the txt format of input file, there is a extra space in the end of each line
                List<string> vertex = new List<string>();
                List<string> edges = new List<string>();
                int first_index = int.Parse(new_arr[0]);
                local = first_index;
                int j = i;
                vertex.Add(new_arr[0]);
                // go through the following line which has same vertex
                while(j < lines.Length){
                    var new_arrr = lines[j].Split(' ');
                    if(int.Parse(new_arrr[0]) != local){
                        // stop when encounter a different vertex, mutate i position to new position, so that in next for loop, i will start at new vertex
                        i = j - 1;
                        break;
                    } else {
                        edges.Add(new_arrr[1]);
                        j++;
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