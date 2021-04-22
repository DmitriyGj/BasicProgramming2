using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		public static double Median(this IEnumerable<double> items)
		{
			var parsedItems =items.OrderBy(x=>x).ToArray();
			var count = parsedItems.Length;
			if(count != 0)
				return count % 2 == 0 ? parsedItems.Average():parsedItems.ToArray()[count/2] ;
			throw new InvalidOperationException();
		}

		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var parsedItems = items.GetEnumerator();
			parsedItems.MoveNext();
			var previous = parsedItems.Current;
			while (parsedItems.MoveNext())
			{
				yield return new Tuple<T, T>(previous,parsedItems.Current);
				previous = parsedItems.Current;
			}
				/*items.SelectMany((x,count) => items.Skip(count+1).Select(y=> Tuple.Create(x,y));*/
		}
	}
}