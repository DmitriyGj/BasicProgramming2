using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
	public class BfsTask
	{
		public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
		{
			var queue = new Queue<Point>();
			queue.Enqueue(start);
			var visited = new bool[map.Dungeon.GetLength(0), map.Dungeon.GetLength(1)];
			visited[start.X, start.Y] = true;
			var dataway = new Dictionary<Point, SinglyLinkedList<Point>>() { { start, new SinglyLinkedList<Point>(start) } };
			while (queue.Count != 0)
			{
				var previousPoint = queue.Dequeue();
				for (int dx = -1; dx <= 1; dx++)
					for (int dy = -1; dy <= 1; dy++)
					{
						var nextPoint = new Point(previousPoint.X + dx, previousPoint.Y + dy);
						if (Math.Abs(dx + dy) != 1 || !map.InBounds(nextPoint)
						|| visited[nextPoint.X, nextPoint.Y] || map.Dungeon[nextPoint.X, nextPoint.Y].Equals(MapCell.Wall))
							continue;
						queue.Enqueue(nextPoint);
						visited[nextPoint.X, nextPoint.Y] = true;
						dataway[nextPoint] = new SinglyLinkedList<Point>(nextPoint, dataway[previousPoint]);
					}
			}
			foreach (var chest in chests)
				if (dataway.ContainsKey(chest))
					yield return dataway[chest];
		}
	}
}