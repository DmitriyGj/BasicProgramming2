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
            var result = CountOfTickets(opt, halfLen, totalSum/2);
            return result*result;
        }

        static BigInteger CountOfTickets(BigInteger[,] tickets, int length, int sum)
        {
            if (sum==0)
                return 1;
            else if (length ==0)
                return 0;
            else if (tickets[length, sum] != 0)
                return tickets[length, sum];
            BigInteger localRes = 0;
            for (int df = 0; df != 10; df++)
            {
                var diff = sum - df;
                if (diff>-1)
                    localRes += CountOfTickets(tickets, length - 1, diff);
            }

            tickets[length, sum] = localRes;
            return localRes;
        }
    }
}
