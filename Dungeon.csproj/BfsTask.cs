using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            var chestsPaths = new Dictionary<Point, SinglyLinkedList<Point>>();
            var visited = new List<Point>() { start };
            while (queue.Count != 0)
            {
                var prevPoint = queue.Dequeue();
                foreach (var curPoint in prevPoint.Value.GetNeighbours())
                    if (map.InBounds(curPoint) && !visited.Contains(curPoint) && map.Dungeon[curPoint.X, curPoint.Y] != MapCell.Wall)
                        queue.Enqueue(new SinglyLinkedList<Point>(curPoint, prevPoint));
                if (chests.Contains(prevPoint.Value))
                    chestsPaths[prevPoint.Value] = prevPoint;
                visited.Add(prevPoint.Value);
            }
            foreach (var chest in chests)
                if (chestsPaths.ContainsKey(chest))
                    yield return chestsPaths[chest];
        }
    }

    static class Extensions
    {
        public static IEnumerable<Point> GetNeighbours(this Point point)
        {
            yield return new Point(point.X + 1, point.Y);
            yield return new Point(point.X - 1, point.Y);
            yield return new Point(point.X, point.Y + 1);
            yield return new Point(point.X, point.Y - 1);
        }
    }
}