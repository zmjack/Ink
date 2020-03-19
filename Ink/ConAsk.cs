using NStandard;
using System;

namespace Ink
{
    public sealed class ConAsk
    {
        public ConOut Cout { get; }

        public string Question { get; }
        public ResolveDelegate Resolver { get; private set; }
        public delegate string ResolveDelegate(string answer);

        internal ConAsk(ConOut cout, string question, ResolveDelegate resolver)
        {
            Cout = cout;
            Question = question;
            Resolver = resolver;
        }

        public void Resolve()
        {
            var cursorVisible = Console.CursorVisible;
            Console.CursorVisible = true;

            while (true)
            {
                Cout.Print("? ", new ConColor { ForegroundColor = ConsoleColor.Green });
                Cout.Print($"{Question}: ");

                var left = Console.CursorLeft;
                var top = Console.CursorTop;

                var answer = Console.ReadLine();
                var result = Resolver(answer);

                if (result is null) continue;

                Console.SetCursorPosition(left, top);
                Echo.Instance.Print(result, new ConColor { ForegroundColor = ConsoleColor.Cyan });
                if (result.GetLengthA() < answer.GetLengthA())
                    Console.Write(" ".Repeat(answer.GetLengthA() - result.GetLengthA()));
                Console.WriteLine();

                break;
            }

            Console.CursorVisible = cursorVisible;
        }
    }

}