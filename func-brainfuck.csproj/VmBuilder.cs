using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func.brainfuck
{
    class VmBuilder
    {
        private int memSize;
        private VirtualMachine vm;
        Func<int> read;
        Action<char> write;
        bool needBasic;
        bool needLoops;
        public VmBuilder(string construction = "", int memSize = 0)
        {
            this.memSize = memSize;
        }

        public VmBuilder AddBasicCommands(Func<int> read, Action<char> write)
        {
            this.needBasic = true;
            this.read = read;
            this.write = write;
            return this;
        }

        public VmBuilder AddLoopCommands()
        {
            this.needLoops = true;
            return this;
        }

        public VirtualMachine Build(string program)
        {
            vm = new VirtualMachine(program, memSize);
            if (needBasic)
                BrainfuckBasicCommands.RegisterTo(vm,read,write);
            if (needLoops)
                BrainfuckLoopCommands.RegisterTo(vm);
            return vm;
        }
    }
}
