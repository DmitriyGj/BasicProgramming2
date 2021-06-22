using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;

namespace Greedy
{
	public class GreedyPathFinder : IPathFinder
	{
		public List<Point> FindPathToCompleteGoal(State state)
		{
			if (state.Chests.Count >= state.Goal)
			{
				var resultPath = new PathWithCost(0);
				var collectedChests = 0;
				while (collectedChests < state.Goal)
				{
					var path = new DijkstraPathFinder().GetPathsByDijkstra(state, state.Position, state.Chests)
						.FirstOrDefault();
					if (path == null || path.Cost > state.Energy)
					{
						break;
					}
					resultPath = new PathWithCost(path.Cost, resultPath.Path.Concat(path.Path.Skip(1)).ToArray());
					state.Position = path.End;
					state.Chests.Remove(path.End);
					collectedChests++;
				}
				return resultPath.Path;
			}
			return new List<Point>(); 
		}
	}
}