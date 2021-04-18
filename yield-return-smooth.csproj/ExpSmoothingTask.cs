using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			DataPoint previousPoint = null;
			foreach(var currentPoint in data)
			{
				if (previousPoint == null)
					previousPoint = new DataPoint(currentPoint).WithExpSmoothedY(currentPoint.OriginalY);
				else
					previousPoint = currentPoint.WithExpSmoothedY(alpha * currentPoint.OriginalY + (1 - alpha) * previousPoint.ExpSmoothedY);
				yield return previousPoint;
			}
		}
	}
}