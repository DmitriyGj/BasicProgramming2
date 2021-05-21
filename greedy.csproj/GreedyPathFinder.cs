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
			var amountOfPickedChests = 0;
			var finalPath = new PathWithCost(0);
			if (state.Chests.Count < state.Goal)
				return new List<Point>();
			while (state.Goal > amountOfPickedChests )
			{
				var path = new DijkstraPathFinder().GetPathsByDijkstra(state, state.Position, state.Chests).FirstOrDefault();
				if (path == null || path.Cost > state.Energy)
				{ break; }
				state.Position = path.End;
				finalPath = new PathWithCost(path.Cost, finalPath.Path.Concat(path.Path.Skip(1)).ToArray());
				state.Chests.Remove(state.Position);
				amountOfPickedChests++;
			}
			return finalPath.Path;
		}
	}
}