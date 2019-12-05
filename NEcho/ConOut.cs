using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NEcho
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

        public ConOut Offset(int offsetRow, int offsetCol)
        {
            var left = (Console.CursorLeft - offsetCol).For(_ => _ >= 0 ? _ : 0);
            var top = (Console.CursorTop - offsetRow).For(_ => _ >= 0 ? _ : 0);
            Console.SetCursorPosition(left, top);
            return this;
        }

        public ConOut OffsetRow(int offsetRow)
        {
            var top = (Console.CursorTop - offsetRow).For(_ => _ >= 0 ? _ : 0);
            Console.SetCursorPosition(Console.CursorLeft, top);
            return this;
        }

        public ConOut OffsetCol(int offsetCol)
        {
            var left = (Console.CursorLeft - offsetCol).For(_ => _ >= 0 ? _ : 0);
            Console.SetCursorPosition(left, Console.CursorTop);
            return this;
        }

        public ConOut BorderTable<TModel>(IEnumerable<TModel> models)
        {
            Console.Write(ConUtility.BorderTable(models));
            return this;
        }
        public ConOut BorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.BorderTable(headers, colLines, lengths));
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

        public ConOut Ask(string question, Func<string, string> resolver)
        {
            new ConAsk(this, question, new ConAsk.ResolveDelegate(resolver)).Resolve();
            return this;
        }

        public ConOut AskYN(string question, Func<bool, string> resolver)
        {
            new ConAsk(this, question, new ConAsk.ResolveDelegate((answer) =>
            {
                if (new[] { "y", "yes", "Y", "Yes", "YES" }.Contains(answer))
                    return resolver(true);
                else if (new[] { "n", "no", "N", "No", "NO" }.Contains(answer))
                    return resolver(false);
                else return null;
            })).Resolve();

            return this;
        }
        public ConOut AskYN(string question, Action<bool> method)
        {
            new ConAsk(this, question, new ConAsk.ResolveDelegate((answer) =>
            {
                if (new[] { "y", "yes", "Y", "Yes", "YES" }.Contains(answer))
                {
                    method(true);
                    return "Yes";
                }
                else if (new[] { "n", "no", "N", "No", "NO" }.Contains(answer))
                {
                    method(false);
                    return "No";
                }
                else return null;
            })).Resolve();

            return this;
        }
        public ConOut AskYN(string question, out bool ret)
        {
            bool _ret = false;
            new ConAsk(this, question, new ConAsk.ResolveDelegate((answer) =>
            {
                if (new[] { "y", "yes", "Y", "Yes", "YES" }.Contains(answer))
                {
                    _ret = true;
                    return "Yes";
                }
                else if (new[] { "n", "no", "N", "No", "NO" }.Contains(answer))
                {
                    _ret = false;
                    return "No";
                }
                else return null;
            })).Resolve();

            ret = _ret;
            return this;
        }

    }
}
