using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;
 
namespace Greedy
{
	public class NotGreedyPathFinder : IPathFinder
	{
		public List<Point> FindPathToCompleteGoal(State state)
		{
            var visited = new List<Point>();
            var currentPoint = state.Position;
		}
	}
}