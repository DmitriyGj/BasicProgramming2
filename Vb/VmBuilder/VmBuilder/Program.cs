using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VmBuilder
{

	class Program
	{
		public static void Main()
		{
			var a = new VMBuilder(memSize: 60);
		}
	}


	public interface IVirtualMachine
	{
		void RegisterCommand(char symbol, Action<IVirtualMachine> execute);
		string Instructions { get; }
		int InstructionPointer { get; set; }
		byte[] Memory { get; }
		int MemoryPointer { get; set; }
		void Run();
	}

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
	public class VMBuilder
    {
		private VirtualMachine vm = new VirtualMachine("",0);
		public VMBuilder(string construction = "", int memSize = 0)
        {
			this.memSize = memSize;
			this.construction = construction;
        }
		public VMBuilder AddBasicCommands(Action<int> command, Action<string> execute)
        {
			
        }
		public VirtualMachine Build(string program)
        {
			return new VirtualMachine(program, memSize);
        }
    }
}
