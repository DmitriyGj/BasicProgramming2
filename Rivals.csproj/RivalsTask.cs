using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Rivals
{
    public class RivalsTask
    {
        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queue = new Queue<Player>();
            var visited = new List<Point>();
            for (int i = 0; i < map.Players.Length; i++)
            {
                visited.Add(map.Players[i]);
                queue.Enqueue(new Player(i,map.Players[i],0));
            }
            while (queue.Count != 0)
            {
                var currentPlayer = queue.Dequeue();
                foreach (var nextPoint in currentPlayer.CurrentPoint.GetNeighbours())
                {
                    if (map.ApplyPoint(nextPoint) && !visited.Contains(nextPoint))
                        queue.Enqueue(new Player(currentPlayer.Number,nextPoint,currentPlayer.CurrentLength+1));
                    visited.Add(nextPoint);
                }
                yield return new OwnedLocation(currentPlayer.Number, currentPlayer.CurrentPoint, currentPlayer.CurrentLength);
            }
        }
    }

    public class Player
    {
        public int Number ;
        public Point CurrentPoint;
        public int CurrentLength;

        public Player(int number, Point currentPoint, int currentLength)
        {
            Number = number;
            CurrentPoint = currentPoint;
            CurrentLength = currentLength;
        }
    }

    public static class Extensions
    {
        public static IEnumerable<Point> GetNeighbours(this Point point)
        {
            yield return new Point(point.X + 1, point.Y);
            yield return new Point(point.X - 1, point.Y);
            yield return new Point(point.X, point.Y+ 1);
            yield return new Point(point.X , point.Y-1);
        }
        public static bool ApplyPoint(this Map map, Point point)
        {
            return  map.InBounds(point) && map.Maze[point.X, point.Y] != MapCell.Wall;
        }
                    
    }
}