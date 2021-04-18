using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Clones
{
	public class CloneVersionSystem : ICloneVersionSystem
	{
		public List<Clone> CloneList = new List<Clone>() { new Clone() };
		public string Execute(string query)
		{
			var parsedQuery = query.Split().ToArray();
			var command = parsedQuery[0];
			var numberOfClone = Convert.ToInt32(parsedQuery[1]) - 1;

			switch (command)
			{
				case "check":
					return CloneList[numberOfClone].Check();
				case "learn":
					CloneList[numberOfClone].Learn(parsedQuery[2]);
					return null;
				case "clone":
					CloneList.Add(new Clone(CloneList[numberOfClone]));
					return null;
				case "rollback":
					CloneList[numberOfClone].RollBack();
					return null;
				case "relearn":
					CloneList[numberOfClone].Relearn();
					return null;
			}
			return null;
		}
	}
	public class Clone
	{
		public Stack<int> LearnedProgramms;
		public Stack<int> HistoryOfRollBacks;

		public Clone()
		{
			LearnedProgramms = new Stack<int>();
			HistoryOfRollBacks = new Stack<int>();
		}

		public void Learn(string numberOfCommand)
		{
			LearnedProgramms.Push(Convert.ToInt32(numberOfCommand));
			HistoryOfRollBacks.Clear();
		}

		public void RollBack()
		{
			HistoryOfRollBacks.Push(LearnedProgramms.Pop());
		}

		public void Relearn()
		{
			LearnedProgramms.Push(HistoryOfRollBacks.Pop());
		}

		public Clone(Clone differentClone)
		{
			LearnedProgramms = new Stack<int>(differentClone.LearnedProgramms);
			HistoryOfRollBacks = new Stack<int>(differentClone.HistoryOfRollBacks);
		}

		public string Check() => LearnedProgramms.Count == 0 ? "basic" : LearnedProgramms.Peek().ToString();
	}

	public class Stack<T>
	{
		StackItem head;
		public int Count { get; set; }

		public Stack() 
		{ 
			Count = 0; 
		}

		public Stack(Stack<T> anotherStack) 
		{
			head = anotherStack.head;
			Count = anotherStack.Count;
		}

		public void Push(T value)
		{
			Count++;
			head = new StackItem(value, head);
		}

		public T Pop()
		{
			var value = head.Value;
			head = head.Previous;
			Count--;
			return value;
		}

		public void Clear()
		{
			Count = 0;
			head = null;
		}
		public T Peek() => head.Value;


		private class  StackItem
		{
		    public readonly T Value;
			public readonly StackItem Previous;

			public StackItem(T value, StackItem previous)
			{
				Value = value;
				Previous = previous;
			}
		}
	}
}
