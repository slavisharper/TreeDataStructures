namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Node<T>
    {
        private List<Node<T>> children;

        public Node(T value)
        {
            this.Value = value;
            this.children = new List<Node<T>>();
        }

        public Node(T value, params Node<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                this.children.Add(child);
            }
        }

        public T Value { get; set; }

        public bool HasParent { get; set; }

        public int ChildrenCount
        {
            get { return this.children.Count; }
        }

        public List<Node<T>> Children 
        { 
            get { return this.children; } 
        }

        public void AddChild(Node<T> child)
        {
            if (child == null)
            {
                throw new ArgumentNullException("Cannot insert null value!");
            }

            if (child.HasParent)
            {
                throw new ArgumentException("The node already has a parent!");
            }

            child.HasParent = true;
            this.children.Add(child);
        }

        public Node<T> GetChild(int index)
        {
            return this.children[index];
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
