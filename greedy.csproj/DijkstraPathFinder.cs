using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
    public class DijkstraPathFinder
    {
        public class NodeInfo
        {
            public Point Previous { get; }
            public int Cost { get; }
            public NodeInfo(Point previous, int cost)
            {
                Previous = previous;
                Cost = cost;
            }
        }
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
                IEnumerable<Point> targets)
        {
            var paths = new Dictionary<Point, NodeInfo> { { start, new NodeInfo(Point.Empty, 0) } };
            var visited = new Dictionary<Point, NodeInfo>();
            while (paths.Count != 0)
            {
                var open = new Point();
                var maxPrice = int.MaxValue;

                foreach (var path in paths)
                    if (maxPrice > path.Value.Cost)
                    {
                        open = path.Key;
                        maxPrice = path.Value.Cost;
                    }

                visited.Add(open, paths[open]);
                paths.Remove(open);

                if (targets.Contains(open))
                    yield return GetPath(visited,start,  open);

                foreach (var path in FindNearPoints(open,state))
                {
                    if (visited.ContainsKey(path))
                        continue;

                    var currentPrice = visited[open].Cost + state.CellCost[path.X, path.Y];

                    if (!paths.ContainsKey(path) || paths[path].Cost > currentPrice)
                        paths[path] = new NodeInfo(open, currentPrice);
                }
            }
        }
        
        public List<Point> FindNearPoints(Point currentPoint, State state)
        {
            var result = new List<Point>();
            for (int dx = -1; dx != 2; dx++)
                for (int dy = -1; dy != 2; dy++)
                {
                    if (Math.Abs(dx+dy)==1)
                    {
                        var nearPoint = new Point(currentPoint.X + dx, currentPoint.Y + dy);
                        if (state.InsideMap(nearPoint) && !state.IsWallAt(nearPoint))
                            result.Add(nearPoint);
                    }
                }
            return result.OrderBy(point => state.CellCost[point.X, point.Y]).ToList();
        }

        public static PathWithCost GetPath(Dictionary<Point, NodeInfo> visited,Point start, Point end)
        {
            var res = new PathWithCost(visited[end].Cost);

            while (end != start)
            {
                res.Path.Add(end);
                end = visited[end].Previous;
            }
            res.Path.Add(end);
            res.Path.Reverse();

            return res;
        }
    }
}
