using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greedy.Architecture;
using System.Drawing;
using NUnit.Framework;

namespace Greedy
{
    public class DijkstraPathFinder
    {
        static bool[,] visited;
        private static Dictionary<Point, PathWithCost> paths;
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            visited = new bool[state.MapWidth,state.MapHeight];
            paths = new Dictionary<Point, PathWithCost>() {{start, new PathWithCost(0, start)}};
            while (paths.Count > 0)
            {
                var curWay = paths.First().Value;
                if (targets.Contains(curWay.End))
                    yield return curWay;
                foreach (var point in curWay.End.GetNeighbours())
                {
                    if (state.InsideMap(point) && !state.IsWallAt(point))
                    {
                        var newCost = curWay.Cost + state.CellCost[point.X, point.Y];
                        var newPath = curWay.Path.Union(new [] { point }).ToArray();
                        if (!paths.ContainsKey(point) || newCost < paths[point].Cost && !visited[point.X,point.Y])
                            paths[point] = new PathWithCost(newCost, newPath);
                    }
                }
                visited[curWay.End.X,curWay.End.Y] = true;
                SortPaths();
            }
        }
        
        private static void SortPaths()
        {
            paths= paths.OrderBy(pair => pair.Value.Cost)
                .Where(path=>!visited[path.Key.X,path.Key.Y])
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
   
    public static class PointExtensions
    {
        public static IEnumerable<Point> GetNeighbours(this Point point)
        {
            yield return new Point(point.X + 1, point.Y);
            yield return new Point(point.X - 1, point.Y);
            yield return new Point(point.X, point.Y - 1);
            yield return new Point(point.X, point.Y + 1);
        }
    }
}