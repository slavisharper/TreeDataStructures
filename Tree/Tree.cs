namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T>
    {
        private Node<T> root;

        public Tree(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Cannot insert null value!");
            }

            this.root = new Node<T>(value);
        }

        public Tree(params Node<T>[] nodes)
        {
            this.root = this.FindRoot(nodes);
        }

        public Node<T> Root
        { 
            get { return this.root; } 
        }

        public int LevelsCount
        {
            get
            {
                return this.LongestPath + 1;
            }
        }

        public int LongestPath
        {
            get
            {
                if (this.root == null)
                {
                    throw new ArgumentNullException("The tree does not have a root!");
                }

                var longestPath = this.FindLongestPath(this.root);
                return longestPath;
            }
        }

        public List<Node<T>> GetLeafs()
        {
            return this.FindLeafs(this.root, new List<Node<T>>());
        }

        public List<Node<T>> GetMiddleNodes()
        {
            return this.FindMiddleNodes(this.root, new List<Node<T>>());
        }

        private List<Node<T>> FindLeafs(Node<T> root, List<Node<T>> foundLeafs)
        {
            foreach (var node in root.Children)
            {
                this.FindLeafs(node, foundLeafs);
            }

            if (root.ChildrenCount == 0)
            {
                foundLeafs.Add(root);
            }

            return foundLeafs;
        }

        private List<Node<T>> FindMiddleNodes(Node<T> root, List<Node<T>> foundMiddleNodes)
        {
            foreach (var node in root.Children)
            {
                this.FindMiddleNodes(node, foundMiddleNodes);
            }

            if (root.ChildrenCount > 0 && root.HasParent)
            {
                foundMiddleNodes.Add(root);
            }

            return foundMiddleNodes;
        }

        private int FindLongestPath(Node<T> root)
        {
            if (root.Children.Count == 0)
            {
                return 0;
            }

            int maxPath = 0;
            foreach (var node in root.Children)
            {
                maxPath = Math.Max(maxPath, this.FindLongestPath(node));
            }

            return maxPath + 1;
        }

        private Node<T> FindRoot(Node<T>[] nodes)
        {
            foreach (var node in nodes)
            {
                if (!node.HasParent)
                {
                    return node;
                }
            }

            throw new ArgumentException("No single root found!");
        }
    }
}
