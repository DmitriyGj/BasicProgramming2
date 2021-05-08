using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rivals
{
    public class RivalsTask
    {
        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queue = new Queue<Tuple<Point, int, int>>();
            var visited = new bool[map.Maze.GetLength(0), map.Maze.GetLength(1)];
            for (int i = 0; i < map.Players.Length; i++)
            {
                queue.Enqueue(Tuple.Create(map.Players[i], i, 0));
                visited[map.Players[i].X, map.Players[i].Y] = true;
            }
            while (queue.Count != 0)
            {
                var playerInfo = queue.Dequeue();
                var playerPoint = playerInfo.Item1;
                for (var dx = -1; dx <= 1; dx++)
                    for (var dy = -1; dy <= 1; dy++)
                    {
                        var nextPoint = new Point(playerPoint.X + dx, playerPoint.Y + dy);
                        if (Math.Abs(dx + dy) == 1 && map.IsValidPoint(nextPoint) && !visited[nextPoint.X,nextPoint.Y])
                        { 
                           queue.Enqueue(Tuple.Create(nextPoint, playerInfo.Item2, playerInfo.Item3 + 1));
                           visited[nextPoint.X, nextPoint.Y] = true;
                        }
                    }
                yield return new OwnedLocation(playerInfo.Item2, playerPoint, playerInfo.Item3);
            }
        }
    }

    public static class Extensions
    {
        public static bool IsValidPoint(this Map map, Point point) =>
                    point.X >= 0 && point.X < map.Maze.GetLength(0)
                    && point.Y >= 0 && point.Y < map.Maze.GetLength(1)
                    && map.Maze[point.X, point.Y] != MapCell.Wall;
    }
}