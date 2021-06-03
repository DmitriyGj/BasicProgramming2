using NUnitLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            //new AutoRun().Execute(args);
            var a = new BinaryTree<int>();
            
                a.Add(6);
            a.Add(2);
            a.Add(3);
            a.Add(4);
            a.Add(8);
            a.Add(9);
            a.Add(1);
            foreach (var nodes in a)
            {
                Console.WriteLine(nodes);
            }
            Console.WriteLine(a.Height);
            Console.ReadKey();
        }
    }
}
