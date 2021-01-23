using System;
using System.Collections.Generic;
using System.IO;


namespace Coursera{
    public class hash{

        public int Tsum(){
            var textFile = "./2sum.txt";
            string[] lines = File.ReadAllLines(textFile);
            List<long> linees = new List<long>();
            for(int i = 0;i < lines.Length; ++i){
                linees.Add(Int64.Parse(lines[i]));
            }
            
            int a = Algorithm(linees);
            return 0;
        }
        public int Algorithm(List<long> lines){
            const long MIN = -10000;
            const long MAX = 10000;
            HashSet<long> hashT = new HashSet<long>();
            lines.Sort();
            int NUM_ELEMENTS = lines.Count;

            int start = 0;
            int end = NUM_ELEMENTS - 1;
            while (start < end)
            {
                long probe_sum = lines[start] + lines[end];
                if (probe_sum < MIN)
                {
                    // the value is too small, there is just no hope for success, let go the small side
                    start++;
                }
                else if (probe_sum > MAX)
                {
                    // the value is too large, there is just no hope for success, let go the small side
                    end--;
                }
                else
                {
                    if (lines[start] != lines[end])
                    {
                        hashT.Add(probe_sum - MIN);
                    }
                    int current_start = start;
                    int current_end = end;
                    while (true)
                    {
                        // let see if there are any more solution starting with the same end
                        start++;
                        probe_sum = lines[start] + lines[end];
                        if (probe_sum < MIN)
                        {
                            // This is impossible
                            break;
                        }
                        else if (probe_sum > MAX)
                        {
                            break;
                        }
                        else
                        {
                            if (lines[start] != lines[end])
                            {
                                hashT.Add(probe_sum - MIN);
                            }
                        }
                    }
                    start = current_start;

                    while (true)
                    {
                        // let see if there are any more solution starting with the same start
                        end--;
                        probe_sum = lines[start] + lines[end];
                        if (probe_sum < MIN)
                        {
                            break;
                        }
                        else if (probe_sum > MAX)
                        {
                            // This is impossible
                            break;
                        }
                        else
                        {
                            if (lines[start] != lines[end])
                            {
                                hashT.Add(probe_sum - MIN);
                            }
                        }
                    }
                    end = current_end;
                    // We have exhausted all solution with start and end, so skip them both
                    start++;
                    end--;
                }
            }
            return hashT.Count;
        }
    }
}



