using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Dungeon
{
	public class DungeonTask
	{
		public static MoveDirection[] FindShortestPath(Map map)
		{
			var shortestFromsStartToExit = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();
			if (shortestFromsStartToExit == null)
				return new MoveDirection[0];

			var pathToExit = BfsTask.FindPaths(map, map.Exit, map.Chests);
			var pathFromStart = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
			var resultWays = from pte in pathToExit
							 join pfs in pathFromStart on pte.Value equals pfs.Value
							 select Tuple.Create(pte, pfs);
			if (resultWays.Count() > 0)
			{
				var result = resultWays.OrderBy(way => way.Item1.Length + way.Item2.Length).FirstOrDefault();
				return result.Item1.Reverse().Concat(result.Item2).ToArray().ToMoveDirections();
			}
			return shortestFromsStartToExit.ToArray().ToMoveDirections();
		}
	}

	public static class Extensions
	{
		public static MoveDirection[] ToMoveDirections(this Point[] points)
		{
			var resultList = new List<MoveDirection>();
			if (points == null)
				return new MoveDirection[0];

			for (var i = points.Length - 1; i > 0; i--)
			{
				if (points[i].X - points[i - 1].X == -1) resultList.Add(MoveDirection.Right);
				if (points[i].X - points[i - 1].X == 1) resultList.Add(MoveDirection.Left);
				if (points[i].Y - points[i - 1].Y == 1) resultList.Add(MoveDirection.Up);
				if (points[i].Y - points[i - 1].Y == -1) resultList.Add(MoveDirection.Down);
			}
			return resultList.ToArray();
		}
	}
}
