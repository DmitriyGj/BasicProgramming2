using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		Dictionary<char, Action<IVirtualMachine>> commands;

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
			InstructionPointer = 0;
			commands = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute) => commands[symbol] = execute;

		public void Run()
		{
			while (InstructionPointer != Instructions.Length)
			{
				if (commands.ContainsKey(Instructions[InstructionPointer]))
					commands[Instructions[InstructionPointer]](this);
				InstructionPointer++;
			}
		}
	}
}