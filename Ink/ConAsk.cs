using NStandard;
using System;
using System.Text;

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
        begin:
            var buffer = new StringBuilder();
            var answer = new AskAnswer();
            if (Question is not null && Question.Length > 0)
            {
                PrintAskHint?.Invoke();
                if (Question.EndsWith(Environment.NewLine)) Cout.Print(Question);
                else Cout.Print($"{Question} ");
            }

            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            while (true)
            {
                var line = Console.ReadLine();
                buffer.Append(line);
                answer.Value = buffer.ToString();
                resolve(answer);

                if (answer.Action == AskAction.Default)
                    answer.Action = answer.Value.IsWhiteSpace() ? AskAction.Retry : AskAction.Accept;

                if (answer.Action == AskAction.Accept)
                {
                    Console.SetCursorPosition(left, top);
                    Echo.Instance.Print(answer.Value, new ConColor { ForegroundColor = ConsoleColor.Cyan });
                    Console.WriteLine();
                    return;
                }
                else if (answer.Action == AskAction.Continue)
                {
                    buffer.AppendLine();
                    continue;
                }
                else if (answer.Action == AskAction.Retry) goto begin;
                else throw new NotImplementedException();
            }
        }
    }

}