using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			try
			{
				return visits
				.GroupBy(visit => visit.UserId)
				.Select(group => group.OrderBy(item => item.DateTime)
				.Bigrams())
				.Select(bigram => bigram.Select(pair => pair)
					.Where(pair => pair.Item1.SlideType == slideType).Distinct())
				.SelectMany(bigram => bigram.Select(pair => (pair.Item2.DateTime - pair.Item1.DateTime).TotalMinutes)
					.Where(time => time >= 1 && time <= 120)).Median();
			}
			catch
			{
				return 0.0;
			}
		}
	}
}