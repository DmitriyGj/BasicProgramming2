using System;
using System.Collections.Generic;
using System.Linq;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            var opt = BuildTable(first, second);
            return BuildAnswer(opt, first, second);
        }

        private static int[,] BuildTable(List<string> first, List<string> second)
        {
            var opt = new int[first.Count + 1, second.Count + 1];
            for(int i =1; i != first.Count + 1; i++)
                for (int j = 1; j !=second.Count + 1; j++)
                {
                    if (first[i - 1] == second[j - 1])
                        opt[i, j] = opt[i - 1, j - 1] + 1;
                    else
                        opt[i, j] = Math.Max(opt[i, j - 1], opt[i - 1, j]);
                }
            return opt;
        }

        private static List<string> BuildAnswer(int[,] opt, List<string> first, List<string> second)
        {
            var result = new List<string>();
            var i = first.Count;
            var j = second.Count;
            while (i != 0 && j !=0 )
            {
                if (opt[i, j] == opt[i - 1, j])
                {
                    i--;
                    continue;
                }
                if (opt[i, j] == opt[i, j-1])
                {
                    j--;
                    continue;
                }
                if(opt[i-1,j-1]<opt[i,j])
                   result.Add(first[i-1]);
                i--;
                j--;
            }
            result.Reverse();
            return result;
        }
    }
}