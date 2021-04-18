using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var queueOfSum = new Queue<double>();
			var sum = 0.0;
			foreach(var currentPoint in data)
			{
				var forDelete = queueOfSum.Count == windowWidth ? queueOfSum.Dequeue() : 0;
				queueOfSum.Enqueue(currentPoint.OriginalY);
				sum -= queueOfSum.Count ==  windowWidth? queueOfSum.Dequeue(): 0 ; 
				sum += currentPoint.OriginalY;
				yield return currentPoint.WithAvgSmoothedY(sum / queueOfSum.Count);
			}
		}
	}
}