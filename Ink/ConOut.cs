using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ink
{
    public class ConOut
    {
        internal ConOut()
        {
        }

        public ConColor ConsoleColor
        {
            get
            {
                return new ConColor
                {
                    BackgroundColor = Console.BackgroundColor,
                    ForegroundColor = Console.ForegroundColor,
                };
            }
            set
            {
                Console.BackgroundColor = value.BackgroundColor;
                Console.ForegroundColor = value.ForegroundColor;
            }
        }

        public ConOut ClearRow()
        {
            using (new InkScope())
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(" ".Repeat(Console.WindowWidth));
            }
            return this;
        }

        public ConOut RowBeginning()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            return this;
        }

        public ConOut RowEnd()
        {
            Console.SetCursorPosition(Console.WindowWidth, Console.CursorTop);
            return this;
        }

        public ConOut Line(int number = 1)
        {
            for (int i = 0; i < number; i++)
                Console.WriteLine();
            return this;
        }

        public ConOut Line(string content, ConColor color = null)
        {
            Print(content, color);
            Console.WriteLine();
            return this;
        }

        public ConOut Print(string content, ConColor color = null)
        {
            void Process() { Console.Write(content); }

            if (color is null) Process();
            else
            {
                var originColor = ConsoleColor;
                ConsoleColor = color;
                Process();
                ConsoleColor = originColor;
            }

            return this;
        }

        public ConOut Left(string line, ConColor color = null)
        {
            using (new InkScope())
            {
                RowBeginning();
                Print($"{line}{" ".Repeat(Console.WindowWidth - line.GetLengthA())}", color);
                return this;
            }
        }
        public ConOut Center(string line, ConColor color = null)
        {
            using (new InkScope())
            {
                RowBeginning();
                Print($"{line.Center(Console.WindowWidth)}", color);
                return this;
            }
        }
        public ConOut Right(string line, ConColor color = null)
        {
            using (new InkScope())
            {
                RowBeginning();
                Print($"{" ".Repeat(Console.WindowWidth - line.GetLengthA())}{line}", color);
                return this;
            }
        }

        public ConOut CoverLeft(string line, ConColor color = null)
        {
            using (new InkScope())
            {
                RowBeginning();
                Console.SetCursorPosition(0, Console.CursorTop);
                Print(line, color);
                return this;
            }
        }
        public ConOut CoverCenter(string line, ConColor color = null)
        {
            using (new InkScope())
            {
                var len = line.GetLengthA();
                int left;
                if (Console.WindowWidth <= len) left = 0;
                else
                {
                    var total = Console.WindowWidth - len;
                    left = total / 2;
                    if (total.IsOdd()) left += 1;
                }

                RowBeginning();
                Console.SetCursorPosition(left, Console.CursorTop);
                Print(line, color);
                return this;
            }
        }
        public ConOut CoverRight(string line, ConColor color = null)
        {
            using (new InkScope())
            {
                RowBeginning();
                Console.SetCursorPosition(Console.WindowWidth - line.GetLengthA(), Console.CursorTop);
                Print(line, color);
                return this;
            }
        }

        public ConOut Row(string[] cols, int[] colLengths)
        {
            using (new InkScope())
            {
                ClearRow();
                Console.Write(ConUtility.Row(cols, colLengths));
                return this;
            }
        }

        public ConOut Move(int offsetRow, int offsetCol)
        {
            var left = (Console.CursorLeft - offsetCol).For(x => x >= 0 ? x : 0);
            var top = (Console.CursorTop - offsetRow).For(x => x >= 0 ? x : 0);
            Console.SetCursorPosition(left, top);
            return this;
        }

        public ConOut RowMove(int offsetRow)
        {
            var top = (Console.CursorTop - offsetRow).For(x => x >= 0 ? x : 0);
            Console.SetCursorPosition(Console.CursorLeft, top);
            return this;
        }

        public ConOut ColMove(int offsetCol)
        {
            var left = (Console.CursorLeft - offsetCol).For(x => x >= 0 ? x : 0);
            Console.SetCursorPosition(left, Console.CursorTop);
            return this;
        }

        public ConOut Table<TModel>(IEnumerable<TModel> models, int[] lengths = null)
        {
            Console.Write(ConUtility.Table(models, lengths));
            return this;
        }
        public ConOut Table(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.Table(headers, colLines, lengths));
            return this;
        }

        public ConOut NoBorderTable<TModel>(IEnumerable<TModel> models, int[] lengths = null)
        {
            Console.Write(ConUtility.NoBorderTable(models, lengths));
            return this;
        }
        public ConOut NoBorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.NoBorderTable(headers, colLines, lengths));
            return this;
        }

        public ConOut SeamlessTable<TModel>(IEnumerable<TModel> models, int[] lengths = null)
        {
            Console.Write(ConUtility.SeamlessTable(models, lengths));
            return this;
        }
        public ConOut SeamlessTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.SeamlessTable(headers, colLines, lengths));
            return this;
        }

        protected ConOut AskResolve<T>(string question, out T value, Action<AskAnswer> followResolve, bool whitespaceRetry, T defaultValue)
        {
            T _value = default;
            var ask = new ConAsk(this, question);
            ask.Resolve(answer =>
            {
                if (answer.Value.IsWhiteSpace())
                {
                    if (whitespaceRetry) { answer.Action = AskAction.Retry; return; }
                    else
                    {
                        answer.Action = AskAction.Accept;
                        answer.Value = defaultValue?.ToString();
                        _value = defaultValue; return;
                    }
                }

                try
                {
                    followResolve(answer);
                    if (answer.Action == AskAction.Accept) _value = (T)Convert.ChangeType(answer.Value, typeof(T));
                }
                catch { answer.Action = AskAction.Retry; }
            });
            value = _value;
            return this;
        }

        public ConOut Ask<T>(string question, out T value, Action<AskAnswer> followResolve) => AskResolve(question, out value, followResolve, true, default);
        public ConOut Ask<T>(string question, out T value) => AskResolve(question, out value, answer => answer.Action = AskAction.Accept, true, default);
        public ConOut Ask<T>(string question, out T value, T defaultValue) => AskResolve(question, out value, answer => answer.Action = AskAction.Accept, false, defaultValue);

        protected void AskMultilineFollowResolve(AskAnswer answer, string endsWith, int adjustEnd)
        {
            if (answer.Value.EndsWith(endsWith))
            {
                if (adjustEnd != 0) answer.Value = answer.Value.Substring(0, answer.Value.Length + adjustEnd);
                answer.Action = AskAction.Accept;
            }
            else answer.Action = AskAction.Continue;
        }

        public ConOut Ask(string question, out string value, string endsWith, int adjustEnd)
        {
            return AskResolve(question, out value, answer => AskMultilineFollowResolve(answer, endsWith, adjustEnd), true, default);
        }
        public ConOut Ask(string question, out string value, string endsWith, int adjustEnd, string defaultValue)
        {
            return AskResolve(question, out value, answer => AskMultilineFollowResolve(answer, endsWith, adjustEnd), false, defaultValue);
        }

        public ConOut AskYN(string question, out bool value)
        {
            var _value = false;
            var ask = new ConAsk(this, question);
            ask.Resolve(answer =>
            {
                if (new[] { "y", "yes", "Y", "YES", "Yes" }.Contains(answer.Value))
                {
                    answer.Action = AskAction.Accept;
                    answer.Value = "Yes";
                    _value = true;
                }
                else if (new[] { "n", "no", "N", "NO", "No" }.Contains(answer.Value))
                {
                    answer.Action = AskAction.Accept;
                    answer.Value = "No";
                    _value = false;
                }
                else answer.Action = AskAction.Retry;
            });
            value = _value;
            return this;
        }

        public ConOut PressContinue()
        {
            Console.ReadKey(true);
            return this;
        }
        public ConOut PressContinue(ConsoleKey key)
        {
            while (Console.ReadKey(true).Key != key) ;
            return this;
        }
        public ConOut PressContinue(ConsoleModifiers modifiers, ConsoleKey key)
        {
            while (!Console.ReadKey(true).For(consoleKey => consoleKey.Key == key && consoleKey.Modifiers == modifiers)) ;
            return this;
        }

        public ConOut Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
            return this;
        }
        public ConOut Sleep(TimeSpan timeout)
        {
            Thread.Sleep(timeout);
            return this;
        }

    }
}
