using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
    public class NotGreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var ways = new LinkedList<Tuple<PathWithCost,HashSet<Point>>>();
            ways.AddLast(Tuple.Create(new PathWithCost(0,new[] { state.Position }),new HashSet<Point>()));
            Tuple<PathWithCost,HashSet<Point>> result = null;
            while (ways.Count != 0)
            {
                var currWay = ways.Last.Value;
                ways.RemoveLast();
                var remainingChests = state.Chests.Except(currWay.Item2);
                var newWays = new DijkstraPathFinder()
                    .GetPathsByDijkstra(state, currWay.Item1.End, remainingChests).GetEnumerator();
                if(currWay.Item2.Count == state.Chests.Count)
                    return currWay.Item1.Path.Skip(1).ToList();
                while (newWays.MoveNext())
                {
                    var newWay = new PathWithCost(currWay.Item1.Cost + newWays.Current.Cost ,
                                                  currWay.Item1.Path.Concat(newWays.Current.Path.Skip(1)).ToArray());
                    if (newWay.Cost > state.Energy) continue;
                    ways.AddLast(Tuple.Create(newWay, currWay.Item2.Union(new[] { newWay.End }).ToHashSet()));
                }
                if (result == null || result.Item2.Count <= currWay.Item2.Count)
                    result = currWay;
            }
            return result.Item1.Path.Skip(1).ToList();
        }
    }
}