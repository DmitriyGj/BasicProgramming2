using System;
using System.Collections.Generic;
using System.Linq;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            var opt = CreateOptimizationTable(first, second);
            return RestoreAnswer(opt, first, second);
        }

        private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
        {
            var lengths = new Tuple<int,int>( first.Count + 1, second.Count + 1);
            var opt = new int[lengths.Item1,lengths.Item2];
            for(int i =1; i != lengths.Item1; i++)
                for(int j =1; j != lengths.Item2; j++)
                    opt[i, j] = first[i - 1] == second[j - 1] ? 
                        opt[i - 1, j - 1] + 1: new[] { opt[i, j - 1], opt[i - 1, j] }.Max();
            return opt;
        }

        private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
        {
            var result = new List<string>();
            var i = first.Count;
            var j = second.Count;
            while (i != 0 && j !=0 )
            {
                if(first[i-1]==second[j-1])
                {
                    result.Add(first[i-1]);
                    i--;
                    j--;
                    continue;
                }
                else if (opt[i - 1, j] > opt[i, j-1])
                {
                    i--;
                    continue;
                }
                else
                    j--;
            }
            result.Reverse();
            return result;
        }
    }
}