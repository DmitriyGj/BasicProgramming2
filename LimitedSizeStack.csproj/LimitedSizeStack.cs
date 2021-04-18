using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private int maxSize;
        private LinkedList<T> items;
        public LimitedSizeStack(int limit)
        {
            items = new LinkedList<T>();
            maxSize = limit;
        }

        public void Push(T item)
        {
            items.AddLast(item);
            if (items.Count > maxSize)
                items.RemoveFirst();    
        }

        public T Pop()
        {
            T value = items.Last.Value;
            items.RemoveLast();
            return value;
        }

        public int Count{get => items.Count;}
    }
}
