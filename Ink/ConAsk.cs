using NStandard;
using System;

namespace Ink
{
    public sealed class ConAsk
    {
        public static Action PrintAskHint = PrintDefalutAskHint;
        private static void PrintDefalutAskHint() => Echo.Print("? ", new ConColor { ForegroundColor = ConsoleColor.Green });

        public ConOut Cout { get; }
        public string Question { get; }

        internal ConAsk(ConOut cout, string question)
        {
            Cout = cout;
            Question = question;
        }

        public void Resolve(Action<AskAnswer> resolve)
        {
            var cursorVisible = Console.CursorVisible;
            Console.CursorVisible = true;

            while (true)
            {
                PrintAskHint?.Invoke();
                if (Question is not null && Question.Length > 0) Cout.Print($"{Question} ");

                var left = Console.CursorLeft;
                var top = Console.CursorTop;

                var line = Console.ReadLine();
                var answer = new AskAnswer { Value = line };
                resolve(answer);

                if (answer.Action == AskAction.Default && answer != null) answer.Action = AskAction.Accept;
                if (answer.Action == AskAction.Accept)
                {
                    Console.SetCursorPosition(left, top);
                    Echo.Instance.Print(answer.Value, new ConColor { ForegroundColor = ConsoleColor.Cyan });
                    if (answer.Value.GetLengthA() < line.GetLengthA())
                        Console.Write(" ".Repeat(line.GetLengthA() - answer.Value.GetLengthA()));
                    Console.WriteLine();
                    break;
                }
                else continue;
            }

            Console.CursorVisible = cursorVisible;
        }
    }

}