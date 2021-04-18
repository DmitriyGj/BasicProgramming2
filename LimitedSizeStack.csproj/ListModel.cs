using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        public LimitedSizeStack<ICommand<TItem>> Chanages;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Chanages = new LimitedSizeStack<ICommand<TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            var command = new Add<TItem>(item);
            Chanages.Push(command);
            command.Execute(Items);
        }

        public void RemoveItem(int index)
        {
            var command = new RemoveItem<TItem>(index);
            Chanages.Push(command);
            command.Execute(Items);
        }

        public bool CanUndo()
        {
            return Chanages.Count > 0;
        }

        public void Undo()
        {
            var command = Chanages.Pop();
            command.Undo(Items);
        }
    }

    public interface ICommand<TItem>
    {
        void Undo(List<TItem> items);
        void Execute(List<TItem> items);
    }

    class Add<TItem> : ICommand<TItem>
    {
        private TItem item;
        public Add(TItem item)
        {
            this.item = item;
        }

        public void Undo(List<TItem> items)
        {
            items.Remove(item);
        }

        public void Execute(List<TItem> items)
        {
            items.Add(item);
        }
    }

    class RemoveItem<TItem> : ICommand<TItem>
    {
        private int index;
        private TItem item;
        public RemoveItem(int index)
        {
            this.index = index;
        }

        public void Execute(List<TItem> items)
        {
            item = items[index];
            items.RemoveAt(index);
        }

        public void Undo(List<TItem> items)
        {
            items.Insert(index, item);
        }
    }
}