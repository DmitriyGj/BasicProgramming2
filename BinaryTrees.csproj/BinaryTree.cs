using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    public class BinaryTree<TValue> : IEnumerable<TValue> where TValue : IComparable
    {
        public BinaryTree<TValue> Right, Left;
        public TValue Value;
        public bool IsRoot { get => Height > 0; }
        public int Height { get; private set; }


        public BinaryTree(TValue value)
        {
            Value = value;
            Height = 1;
        }

        public BinaryTree()
        {
            Height = 0;
        }

        public void Add(TValue value)
        {
            if (!IsRoot)
                Value = value;
            else if (Value.CompareTo(value) >= 0)
            {
                if (Left is null)
                    Left = new BinaryTree<TValue>();
                Left.Add(value);
            }
            else
            {
                if (Right is null)
                    Right = new BinaryTree<TValue>();
                Right.Add(value);
            }
            Height++;
        }

        public bool Contains(TValue node)
        {
            if (!IsRoot)
                return false;
            if (Value.Equals(node))
                return true;
            foreach (var nodes in this)
                if (nodes.Equals(node))
                    return true;
            return false;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            if (IsRoot)
            {
                if (Left != null)
                    foreach (var value in Left)
                        yield return value;

                yield return Value;

                if (Right != null)
                    foreach (var value in Right)
                        yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[int index]
        {
            get
            {
                if (!IsRoot || index < 0 || index >= Height)
                    throw new IndexOutOfRangeException();
                return GetValue(this, index);
            }
        }
        TValue GetValue(BinaryTree<TValue> tree, int index)
        {
            if (!(tree?.Left is null) &&   index < tree?.Left.Height)
                return GetValue(tree.Left,index);
            if (!(tree?.Right is null) && index > tree?.Height - tree.Right.Height-1 )
                return GetValue(tree.Right, index - tree.Height + tree.Right.Height);
            return tree.Value;
        }
    }
}
