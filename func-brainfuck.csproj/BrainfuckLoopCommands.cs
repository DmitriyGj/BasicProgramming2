using System.Collections.Generic;
using System.Security.Policy;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		static Dictionary<int, int> cyclesBegin = new Dictionary<int, int>();
		static Dictionary<int, int> cyclesEnd = new Dictionary<int, int>();
		static Stack<int> open_brackets = new Stack<int>();

		public static void RegisterTo(IVirtualMachine vm)
		{
			var a = vm.Instructions;
			for (int i = 0; i != a.Length; i++)
			{
				if (a[i] == '[')
					open_brackets.Push(i);
				if (a[i] == ']')
				{
					cyclesEnd[i] = open_brackets.Peek();
					cyclesBegin[open_brackets.Pop()] = i;
				}
			}
			vm.RegisterCommand('[', b =>
			{
				vm.InstructionPointer = vm.Memory[vm.MemoryPointer] == 0 ?
				cyclesBegin[vm.InstructionPointer] : vm.InstructionPointer;
			});
			vm.RegisterCommand(']', b =>
			{
				vm.InstructionPointer = vm.Memory[vm.MemoryPointer] != 0 ?
				cyclesEnd[vm.InstructionPointer] : vm.InstructionPointer;
			});
		}
	}
}