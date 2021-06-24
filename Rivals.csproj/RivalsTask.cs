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
            var queue = new LinkedList<PlayerInfo>();
            var visited = new bool[map.Maze.GetLength(0),map.Maze.GetLength(1)];
            for (int i = 0; i < map.Players.Length; i++)
            {
                visited[map.Players[i].X,map.Players[i].Y] = true;
                queue.AddLast(new PlayerInfo(i,map.Players[i],0));
            }
            while (queue.Count != 0)
            {
                var currentPlayerInfo = queue.First();
                yield return currentPlayerInfo.ToOwnedLocation();
                foreach (var point in currentPlayerInfo.Location.GetNeighbours())
                {
                    if (map.ApplyPoint(point) && !visited[point.X, point.Y])
                    {
                        queue.AddLast(new PlayerInfo(currentPlayerInfo.Number, 
                            point, 
                            currentPlayerInfo.PathLength + 1));
                        visited[point.X, point.Y] = true;
                    }
                }
                queue.RemoveFirst();
            }
        }
    }

    public class PlayerInfo
    {
        public int Number ;
        public Point Location;
        public int PathLength;

        public PlayerInfo(int number, Point currentPoint, int currentLength)
        {
            Number = number;
            Location = currentPoint;
            PathLength = currentLength;
        }

        public OwnedLocation ToOwnedLocation()
        {
            return new OwnedLocation(Number,Location,PathLength);
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