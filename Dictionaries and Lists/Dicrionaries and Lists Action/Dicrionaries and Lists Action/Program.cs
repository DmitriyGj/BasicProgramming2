using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dicrionaries_and_Lists_Action
{
    class Program
    {
        public static Dictionary<int, int> SequenceCollecotr = new Dictionary<int, int>(); 
        static void Main()
        {
            DefaultSequence();
            
        }

        public static void DefaultSequence()
        {
            int i = 1;
            while (i != 50001)
                SequenceCollecotr.Add(i,0);
        }
    }
}