using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VmBuilder
{
   
    class Program
    {
		private static Func<int, int, int> Apply1(Func<int, int, int, int> func, int arg)
		{
			return (x, y) => func(x, arg, y);
		}

		private static Func<int, int> Apply2(Func<int, int, int> func, int arg)
		{
			return x => func(arg, x);
		}

		public static void Main()
		{
			var numbers = new List<int> { 1, 2, 3, 4 };
			var evenNumbers = numbers
				.Where(n => n % 2 == 0)
				.ToList()
				.Select(n => n * 31);
			foreach (var num in evenNumbers)
				Console.WriteLine(num);
			Console.ReadKey();
		}
	}
}
