using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterConst();
			vm.RegisterCommand('.', command => write((char)vm.Memory[vm.MemoryPointer]));
			vm.RegisterCommand(',', command => vm.Memory[vm.MemoryPointer] = (byte)read());
			vm.RegisterCommand('+', command =>
					vm.Memory[vm.MemoryPointer] = CalculateByte(vm.Memory[vm.MemoryPointer], true, vm.Memory.Length));
			vm.RegisterCommand('-', command =>
					vm.Memory[vm.MemoryPointer] = CalculateByte(vm.Memory[vm.MemoryPointer], false, vm.Memory.Length));
			vm.RegisterCommand('>', command =>
					vm.MemoryPointer = CalculateMemoryPointer(vm.MemoryPointer, true, vm.Memory.Length));
			vm.RegisterCommand('<', command =>

			vm.MemoryPointer = CalculateMemoryPointer(vm.MemoryPointer, false, vm.Memory.Length));
		}

		public static int CalculateMemoryPointer(int currentIndex, bool sign, int length) =>
			(currentIndex + length + (sign ? 1 : -1) % length) % length;

		public static byte CalculateByte(byte item, bool sign, int length)
		{
			if (item == 0 && !sign)
				return 255;
			else if (item == 255 && sign)
				return 0;
			return (byte)((item + length + (sign ? 1 : -1) % length) % length);
		}
	}

	public static class IVirtualMachineExtension
	{
		static readonly char[] alpha = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789".ToCharArray();
		public static void RegisterConst(this IVirtualMachine vm)
		{
			foreach (var a in alpha)
				vm.RegisterCommand(a, command => vm.Memory[vm.MemoryPointer] = (byte)a);
		}
	}
}