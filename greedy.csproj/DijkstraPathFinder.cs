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
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            var visited = new List<Point>();
            var waitVisit = new Dictionary<Point, PathWithCost>() { {start,new PathWithCost(0,start) } };

            while (waitVisit.Count > 0)
            {
                var currentData = waitVisit.FirstOrDefault();
                if (targets.Contains(currentData.Key))
                    yield return currentData.Value;
               
                foreach (var point in currentData.Key.GetNearPoints())
                {
                    if (!state.InsideMap(point) || state.IsWallAt(point))
                        continue;
                    var currentWeight = currentData.Value.Cost + state.CellCost[point.X, point.Y];
                    var potentialPath = currentData.Value.Path.Union(new [] { point }).ToArray();
                    if (!waitVisit.ContainsKey(point) || waitVisit[point].Cost > currentWeight)
                        waitVisit[point] = new PathWithCost(currentWeight,potentialPath) ;
                }
                waitVisit.Remove(currentData.Key);
                visited.Add(currentData.Key);
                waitVisit = waitVisit.OrderBy(s => s.Value.Cost)
                    .Where(s => !visited.Contains(s.Key))
                    .ToDictionary(s => s.Key, s => s.Value);
            }
        }
    }

    public static class PointExtensions
    {
        public static IEnumerable<Point> GetNearPoints(this Point point)
        {
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (Math.Abs(dx + dy) == 1)
                        yield return new Point(point.X + dx, point.Y + dy);
        }
    }
}