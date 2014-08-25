namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var nodes = ParseInput(n);
            var tree = new Tree<int>(nodes);
            
            // Exercise 1
            Console.WriteLine("The root is: {0}", tree.Root.Value);

            // Exercse 2
            var leafs = tree.GetLeafs();
            Console.Write("Leafs - ");
            Console.Write(string.Join(", ", leafs));
            Console.WriteLine();

            // Exercse 3
            var middleNodes = tree.GetMiddleNodes();
            Console.Write("Middle nodes - ");
            Console.Write(string.Join(", ", middleNodes));
            Console.WriteLine();

            // Exercse 4
            var longestPath = tree.LongestPath;
            Console.WriteLine("The longest path is: " + longestPath);
            Console.WriteLine("The number of levels are: " + tree.LevelsCount);
        }

        private static Node<int>[] ParseInput(int n)
        {
            var nodes = new Dictionary<int, Node<int>>();

            for (int i = 0; i < n-1; i++)
            {
                string[] inputLine = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int parent = int.Parse(inputLine[0]);
                int child = int.Parse(inputLine[1]);

                if (!nodes.ContainsKey(parent))
                {
                    nodes.Add(parent, new Node<int>(parent));
                }

                if (!nodes.ContainsKey(child))
                {
                    nodes.Add(child, new Node<int>(child));
                }

                nodes[parent].AddChild(nodes[child]);
                nodes[child].HasParent = true;
            }

            return nodes.Values.ToArray();
        }
    }
}
