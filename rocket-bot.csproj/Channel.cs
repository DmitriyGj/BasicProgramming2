using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        List<T> channelItems = new List<T>();

        public T this[int index]
        {
            get { lock (channelItems) return index < 0 || index >= Count ? null : channelItems[index];}
            set
            {
                lock (channelItems)
                {
                    if (index == channelItems.Count)
                        channelItems.Add(value);
                    else if(index >=0 && index < Count)
                    {
                        channelItems[index] = value;
                        channelItems.RemoveRange(index + 1, Count - index - 1);
                    }
                }
            }
        }

        public T LastItem()
        {
            lock (channelItems)
                return Count > 0 ? channelItems[Count - 1] : null;
        }

        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (channelItems)
            {
                if (Equals(LastItem(), knownLastItem))
                    channelItems.Add(item);
            }
        }

        public int Count
        {
            get { return channelItems.Count; }
        }
    }
}