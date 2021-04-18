using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ZedGraph;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var potentialMaximums = new LinkedList<double>();
			var window = new Queue<double>();
			foreach (var point in data)
			{
				window.Enqueue(point.OriginalY);
				if (window.Count == windowWidth && potentialMaximums.First.Value == window.Dequeue())
					potentialMaximums.RemoveFirst();
				while (potentialMaximums.Count != 0 && potentialMaximums.Last.Value <= point.OriginalY)
					potentialMaximums.RemoveLast();
				potentialMaximums.AddLast(point.OriginalY);
				yield return point.WithMaxY(potentialMaximums.First.Value);
			}
		}
	}
}