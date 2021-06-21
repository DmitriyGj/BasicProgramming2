using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dungeon
{
	public class DungeonTask
	{
		public static MoveDirection[] FindShortestPath(Map map)
		{
			var pathToExit = BfsTask.FindPaths(map, map.Exit, map.Chests);
			var pathFromStart = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
			var resultWays = pathFromStart.SelectMany(pfs =>
				pathToExit.Where(pte => pte.Value == pfs.Value).Select(pte => pte.Reverse().Concat(pfs)));
			var result = resultWays.OrderBy(way => way.Count()).FirstOrDefault();
			if (result is null)
			{
				var fromstartToExit = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();
				return fromstartToExit is null ? new MoveDirection[0] : fromstartToExit.ToArray().ToMoveDirections();
			}
			return result.ToArray().ToMoveDirections();
		}
	}

	static class PointExtensions
	{
		public static MoveDirection[] ToMoveDirections(this Point[] points)
		{
			var directions = new List<MoveDirection>();

			for (var i = points.Length - 1; i > 0; i--)
			{
				if (points[i].Y - points[i - 1].Y == 1) directions.Add(MoveDirection.Up);
				if (points[i].Y - points[i - 1].Y == -1) directions.Add(MoveDirection.Down);
				if (points[i].X - points[i - 1].X == -1) directions.Add(MoveDirection.Right);
				if (points[i].X - points[i - 1].X == 1) directions.Add(MoveDirection.Left);
			}
			return directions.ToArray();
		}
	}
}
