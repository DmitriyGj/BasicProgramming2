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
            var lengths = new[] { first.Count + 1, second.Count + 1 };
            var opt = new int[lengths[0],lengths[1]];
            for(int i =1;i != first.Count; i++)
                for(int j = 0;j != second.Count; j++)
                {
                    if (first[i].Except(second[j]).Count() == 0)
                        opt[i, j] = 0;
                    else if (first[i].Equals(second[j]))
                        opt[i, j] = opt[i - 1, j - 1]+1;
                    else
                        opt[i, j] = new[] {opt[i,j-1],opt[i-1,j] }.Max();
                }
            return opt;
        }

        private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
        {
            var res = new List<string>();
            for (int i = first.Count - 1; i != 0; i++)
                for (int j = second.Count - 1; j != 0; j++)
                    if (i == j)
                        res.Add(first[i]);
            return res;
        }
    }
}