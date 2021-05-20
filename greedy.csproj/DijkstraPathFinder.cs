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
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
                IEnumerable<Point> targets)
        {
            var paths = new Dictionary<Point, PathWithCost> { {start,new PathWithCost(0,Point.Empty) } };
            var visited = new bool[state.MapWidth, state.MapHeight];
            while(true)
            {
                var currentPoint = Point.Empty;
                var minCost = int.MaxValue;

                foreach(var path in paths)
                    if(!visited[path.Key.X,path.Key.Y] && path.Value.Cost < minCost)
                    {
                        currentPoint = path.Key;
                        minCost = path.Value.Cost;
                    }

                if (currentPoint == Point.Empty) break;
                if (targets.Contains(currentPoint)) yield return GetPath(paths,currentPoint);
                
                for(int dx =-1;dx != 2; dx++)
                    for(int dy = -1; dy != 2; dy++)
                    {
                        if(Math.Abs(dx+dy)==1)
                        {
                            var nearPoint = new Point(currentPoint.X + dx, currentPoint.Y + dy);
                            if (!state.InsideMap(nearPoint) || state.IsWallAt(nearPoint))
                                continue;
                            var currentPath =
                                new PathWithCost(paths[currentPoint].Cost + state.CellCost[nearPoint.X, nearPoint.Y],
                                new List<Point>(paths[currentPoint].Path) { currentPoint }.ToArray());
                            if (!paths.ContainsKey(nearPoint) || paths[nearPoint].Cost > currentPath.Cost)
                                paths[nearPoint] = currentPath;
                        }
                    }
                visited[currentPoint.X, currentPoint.Y] = true;
            }
        }
        public static PathWithCost GetPath(Dictionary<Point,PathWithCost> paths, Point end)
        {
            return new PathWithCost(paths[end].Cost, new List<Point>(paths[end].Path).ToArray());
        }
    }
}
