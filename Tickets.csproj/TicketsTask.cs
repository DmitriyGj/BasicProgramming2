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
            var opt = new BigInteger[halfLen+1, totalSum/2+1];
            for(int i=0; i != halfLen+1;i++)
                for(int j =0; j != totalSum/2+1;j++)
                    opt[i, j] = -1;
            var result = CountOfTickets(opt, halfLen, totalSum/2);
            return result*result;
        }

        static BigInteger CountOfTickets(BigInteger[,] tickets, int length, int sum)
        {
            if (sum == 0 && length != 0)
                return 1;
            if (sum != 0 && length == 0) 
                return 0;
            if (tickets[length, sum] == -1)
            {
                BigInteger value = 0;
                for (var df = 0; df < 10; df++)
                    if (sum - df >= 0)
                        value += CountOfTickets(tickets, length - 1, sum - df);
                tickets[length, sum] = value;
            }
            return tickets[length,sum];
        }
    }
}
