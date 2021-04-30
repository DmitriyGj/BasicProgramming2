using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			var times = visits
				.GroupBy(visit => visit.UserId)
				.Select(g => g.OrderBy(x=>x.DateTime).Where(x=>x.SlideType ==slideType).Select(x=>x.DateTime.Minute).Where(x=>x>=1 && x<=120))
				.SelectMany(g => g.Select(x => x));
			var count = times.Count();
			if (count != 0)
				return count % 2 == 0 ?times.Average() : times.ToArray()[count / 2];
			return 0.0;
		}
	}
}