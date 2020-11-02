using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ink
{
    public partial class ConOut
    {
        internal ConOut()
        {
        }

        public ConOut ClearRow()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(" ".Repeat(Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            return this;
        }

        public ConOut RowBeginning()
        {
            Console.Write('\b');
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
                var originColor = ConColor;
                ConColor = color;
                Process();
                ConColor = originColor;
            }

            return this;
        }

        public ConOut Left(string line, ConColor color = null)
        {
            RowBeginning();
            Print($"{line}{" ".Repeat(Console.WindowWidth - line.GetLengthA())}", color);
            return this;
        }

        public ConOut Right(string line, ConColor color = null)
        {
            RowBeginning();
            Print($"{" ".Repeat(Console.WindowWidth - line.GetLengthA())}{line}", color);
            return this;
        }

        public ConOut Center(string line, ConColor color = null)
        {
            RowBeginning();
            Print($"{line.Center(Console.WindowWidth)}", color);
            return this;
        }

        public ConOut Row(string[] cols, int[] colLengths)
        {
            var top = Console.CursorTop;

            ClearRow();
            Console.Write(ConUtility.Row(cols, colLengths));

            if (Console.CursorTop != top)
                Console.CursorTop = top;

            return this;
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

        public ConOut Table<TModel>(IEnumerable<TModel> models)
        {
            Console.Write(ConUtility.Table(models));
            return this;
        }
        public ConOut Table(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.Table(headers, colLines, lengths));
            return this;
        }

        public ConOut NoBorderTable<TModel>(IEnumerable<TModel> models)
        {
            Console.Write(ConUtility.NoBorderTable(models));
            return this;
        }
        public ConOut NoBorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.NoBorderTable(headers, colLines, lengths));
            return this;
        }

        public ConOut SeamlessTable<TModel>(IEnumerable<TModel> models)
        {
            Console.Write(ConUtility.SeamlessTable(models));
            return this;
        }
        public ConOut SeamlessTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.SeamlessTable(headers, colLines, lengths));
            return this;
        }

        public ConOut Ask(string question, Action<AskAnswer> resolve)
        {
            var ask = new ConAsk(this, question);
            ask.Resolve(resolve);
            return this;
        }

        public ConOut Ask(string question, out string value)
        {
            string _value = null;
            var ask = new ConAsk(this, question);
            ask.Resolve(answer => _value = answer.Value);
            value = _value;
            return this;
        }

        public ConOut AskYN(string question) => AskYN(question, out _);
        public ConOut AskYN(string question, out bool value)
        {
            var _value = false;
            var ask = new ConAsk(this, question);
            ask.Resolve(answer =>
            {
                if (new[] { "y", "yes", "Y", "Yes", "YES" }.Contains(answer.Value))
                {
                    answer.Action = AskAction.Accept;
                    answer.Value = "Yes";
                    _value = true;
                }
                else if (new[] { "n", "no", "N", "No", "NO" }.Contains(answer.Value))
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

    }
}
