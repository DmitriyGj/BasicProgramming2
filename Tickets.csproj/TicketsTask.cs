using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Tickets
{
    public static class TicketsTask
    {
        public static BigInteger Solve(int halfLen, int totalSum)
        {
            if (totalSum % 2 != 0)
                return 0;
            var lengths = new Tuple<int, int>(halfLen + 1, totalSum / 2 + 1);
            var opt = new BigInteger[lengths.Item1, lengths.Item2];
            for (int i = 1; i != lengths.Item1; i++) 
                opt[i, 0] = 1;
            for(int i = 1; i != lengths.Item1; i++)
                for(int j = 1; j != lengths.Item2; j++)
                {
                    if (j > i * 9) opt[i, j] = 0;
                    else
                    {
                        opt[i, j] = opt[i, j - 1] + opt[i - 1, j];
                        if (j > 9) opt[i, j] -= opt[i - 1, j - 10];
                    }
                }
            return opt[halfLen, totalSum / 2]* opt[halfLen, totalSum / 2];
        }
    }
}
