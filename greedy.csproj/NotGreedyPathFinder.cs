using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;
 
namespace Greedy
{
    class WayFinder
    {
        public LinkedList<Point> way { get; set; }
        public int Price { get; set; }
        public int Chests { get; set; }
        public List<Point> notVisited { get; set; }
        public WayFinder previous { get; set; }
    }
 
	public class NotGreedyPathFinder : IPathFinder
	{
		public List<Point> FindPathToCompleteGoal(State state)
		{
            var pathFinder = new DijkstraPathFinder();
            var ways = new Queue<WayFinder>();
            ways.Enqueue(new WayFinder { way = new LinkedList<Point>(), Price = 0, Chests = 0, notVisited = state.Chests,previous=null});
            WayFinder resultWay = null;
            while(ways.Count != 0)
            {
                var findNext = false;
                var currWay=ways.Dequeue();
                var newWays = pathFinder.GetPathsByDijkstra(state, currWay.way.Count==0 ? state.Position: currWay.way.Last.Value,currWay.notVisited);
                foreach(var e in newWays)
                {
                    var findWay=new WayFinder();
                    findWay.Price = currWay.Price + e.Cost;
                    if (findWay.Price > state.Energy)
                        continue;
                    findWay.way = new LinkedList<Point>(e.Path);
                    findWay.way.RemoveFirst();
                    findWay.previous = currWay;
                    findWay.Chests = currWay.Chests + 1;
                    findWay.notVisited = new List<Point>(currWay.notVisited);
                    findWay.notVisited.Remove(findWay.way.Last.Value);
                    ways.Enqueue(findWay);
                    findNext = true;
                }
                if (!findNext && (resultWay==null || resultWay.Chests<currWay.Chests))
                    resultWay=currWay;
            }
            var result = new List<Point>();
            while(resultWay != null)
            {
                result.InsertRange(0, resultWay.way);
                resultWay = resultWay.previous;
            }
            return result;
		}
	}
}