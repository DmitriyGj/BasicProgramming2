using System;
using System.Linq;
using NUnitLite;

namespace func.brainfuck
{
	public class Program
	{
		private const string sierpinskiTriangleBrainfuckProgram = @"
                                >
                               + +
                              +   -
                             [ < + +
                            +       +
                           + +     + +
                          >   -   ]   >
                         + + + + + + + +
                        [               >
                       + +             + +
                      <   -           ]   >
                     > + + >         > > + >
                    >       >       +       <
                   < <     < <     < <     < <
                  <   [   -   [   -   >   +   <
                 ] > [ - < + > > > . < < ] > > >
                [                               [
               - >                             + +
              +   +                           +   +
             + + [ >                         + + + +
            <       -                       ]       >
           . <     < [                     - >     + <
          ]   +   >   [                   -   >   +   +
         + + + + + + + +                 < < + > ] > . [
        -               ]               >               ]
       ] +             < <             < [             - [
      -   >           +   <           ]   +           >   [
     - < + >         > > - [         - > + <         ] + + >
    [       -       <       -       >       ]       <       <
   < ]     < <     < <     ] +     + +     + +     + +     + +
  +   .   +   +   +   .   [   -   ]   <   ]   +   +   +   +   +
";

		public static void Main(string[] args)
		{
            //if (args.Contains("test"))
            //	new AutoRun().Execute(new string[0]); // Запуск тестов
            //else
            //{
            //	Brainfuck.Run(sierpinskiTriangleBrainfuckProgram, Console.Read, Console.Write);
            //	Console.WriteLine("Это была демонстрация Brainfuck на примере построения треугольника Серпинского");
            //}
            //Console.ReadLine();
            string[] programs = 
                {
            "H>e>l>l>o>++++++++++++++++++++++++++++++++>w>o>r>l>d<<<<<<<<<<.>.>.>.>.>.>.>.>.>.>.",
            "Q.W.E.R.T.Y.U.I.O.P.A.S.D.F.G.H.J.K.L.Z.X.C.V.B.N.M." +
                    "q.w.e.r.t.y.u.i.o.p.a.s.d.f.g.h.j.k.l.z.x.c.v.b.n.m." +
                    "1.2.3.4.5.6.7.8.9.0.",
            @"+++++++++++++++++++++++++++++++++++++++++++++
            ++++++++++++++++++++++++++ +.++++++++++++++++ +
            ++++++++++++.++++++ + ..+++.------------------ -
            ---------------------------------------------
            ---------------.++++++++++++++++++++++++++++ +
            ++++++++++++++++++++++++++.++++++++++++++++++
            ++++++.++ +.------.--------.------------------
            -------------------------------------------- -
            ----.---------------------- -." ,
            "+++++.",
            "[+.].",
            "++[>++[>+<-]<-]>>."
            };
            var vmBuilder = new VmBuilder(memSize: 60)
                            .AddBasicCommands(() => Console.Read(), c => Console.Write(c))
                            .AddLoopCommands();
            foreach (var prog in programs)
            {
                vmBuilder.Build(prog).Run(); // Build(...) возвращает созданную Vm
                Console.WriteLine();
            }
            Console.ReadKey();
        }
	}
}