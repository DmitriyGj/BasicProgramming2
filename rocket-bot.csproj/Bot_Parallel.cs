using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var tasks = new List<Task<Tuple<Turn,double>>>();
            var results = new List<Tuple<Turn, double>>();
            int range = iterationsCount / threadsCount;

            for (int i =0; i != threadsCount;i++)
                tasks.Add(new Task<Tuple<Turn, double>>(() => SearchBestMove(rocket, new Random(random.Next()),range)));
            foreach (var task in tasks)
                task.Start();
            foreach (var complitedTasks in tasks)
                results.Add(complitedTasks.Result);
            return rocket.Move(results.OrderBy(way => way.Item2).FirstOrDefault().Item1, level);
        }
    }
}