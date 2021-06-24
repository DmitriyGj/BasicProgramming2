using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        public T Value;
        public bool IsNotNULL = false;
        public int Height { get; private set; }
        public BinaryTree<T> Right, Left;

        public void Add(T value)
        {
            if (!IsNotNULL)
            {
                Value = value;
                IsNotNULL = true;
            }
            else if (Value.CompareTo(value) >= 0)
            {
                if (Left is null)
                    Left = new BinaryTree<T>();
                Left.Add(value);
            }
            else
            {
                if (Right is null)
                    Right = new BinaryTree<T>();
                Right.Add(value);
            }

            Height++;
        }

        public bool Contains(T node)
        {
            if (!IsNotNULL)
                return false;
            if (Value.Equals(node))
                return true;
            foreach (var nodes in this)
                if (nodes.Equals(node))
                    return true;
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        { 
            if (Left != null) 
                foreach (var value in Left) 
                    yield return value;
            if (IsNotNULL) 
                yield return Value;
            if (Right != null)
                foreach (var value in Right)
                    yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get
            {
                if (!IsNotNULL || index < 0 || index >= Height)
                    throw new AggregateException();
                var current = this;
                int ind = index;
                while (true)
                {
                    if (current.Left != null && ind < current.Left.Height)
                        current = current.Left;
                    else 
                    {
                        if (current.Left != null && ind == current.Left.Height)
                            return current.Value;
                        ind -= current.Left.Height ;
                        current = current.Right;
                    }
                }
            }
        }
    }
}
