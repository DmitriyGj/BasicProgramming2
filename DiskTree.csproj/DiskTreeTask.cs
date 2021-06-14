using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public class Node
        {
            public string Name;
            public int Depth;
            public List<Node> Childs = new List<Node>();

            public void AddSubDirectory(string subDirectory) =>
                Childs.Add(new Node
                {
                    Name = subDirectory, Depth = Depth + 1
                });

        public List<Node> ToDirectoryList()
        {
                var directoryList = new List<Node>() {this};
                foreach (var child in Childs.OrderBy(child => child.Name, StringComparer.Ordinal))
                    directoryList = directoryList.Concat(child.ToDirectoryList()).ToList();
                return directoryList;
            }
        }

        public static List<string> Solve(List<string> input)
        {
            var result = new List<Node>();
            foreach (var directory in input)
            {
                var parsedDirectory = directory.Split('\\');
                var currentNode = new Node {Name = parsedDirectory.First(), Depth = 0};
                foreach (var item in parsedDirectory.Skip(1))
                {
                    currentNode.AddSubDirectory(item);
                    currentNode = currentNode.Childs.Last();
                }
                result = result.Concat(currentNode.ToDirectoryList()).ToList();
            }

            return result.Select(node => node).OrderBy(node=>node.Name)
                .Select(node =>$"{new string(' ',node.Depth)}{node.Name}" ).ToList();
        }
    }
}