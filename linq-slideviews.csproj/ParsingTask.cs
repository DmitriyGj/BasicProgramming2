using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines.Skip(1)
						.Select(line => line.Split(';'))
						.Select(mbslide=>ToSlideRecord(mbslide))
						.Where(slide => !(slide is null))
						.ToDictionary(slide => slide.SlideId);
		}

		public static SlideRecord ToSlideRecord(string[] record)
		{
			int id;
			SlideType slideType;
			if (record.Length == 3
			&& record[2] != string.Empty
			&& int.TryParse(record[0], out id)
			&& Enum.TryParse(record[1],true, out slideType))
				return new SlideRecord(id, slideType, record[2]);

			return null;
        }

		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines.Skip(1)
						.Select(s=> s.Split(';'))
						.ToArray()
						.Select(visit => ToVisitRecord(visit,slides));
		}

		public static VisitRecord ToVisitRecord(string[] record, IDictionary<int,SlideRecord> source)
        {
			int userid,
				slideid;
			DateTime visitDate;

			if (int.TryParse(record[0], out userid)
			&& int.TryParse(record[1], out slideid)
			&& DateTime.TryParse(record[2]+" "+record[3], out visitDate)
			&& source.ContainsKey(slideid))
				return new VisitRecord(userid, slideid, visitDate, source[slideid].SlideType);
			throw new FormatException($"Wrong line [{string.Join(";",record)}]");
        }
	}
}