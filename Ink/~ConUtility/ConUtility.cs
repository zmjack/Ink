using NStandard;
using NStandard.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ink
{
    public partial class ConUtility
    {
        private static int[] GetLengths(string[] headers, string[][] lines)
        {
            var lengths = new int[headers.Length];
            foreach (var (index, value) in headers.Pairs())
            {
                lengths[index] = value.GetLengthA();
            }

            foreach (var line in lines)
            {
                foreach (var (index, value) in line.Pairs())
                {
                    if (index < lengths.Length)
                    {
                        var len = value.GetLengthA();
                        if (len > lengths[index]) lengths[index] = len;
                    }
                }
            }

            return lengths;
        }

        public class AlignLineOptions
        {
            public bool OverflowHidden { get; set; } = false;
            public bool TreatDBytesTableLineAsByte { get; set; } = false;
            public string[] Borders { get; set; } = ["", "  ", ""];

#if NET5_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET46_OR_GREATER
            private int[] _lengths = Array.Empty<int>();
#else
            private int[] _lengths = ArrayEx.Empty<int>();
#endif
            public int[] Lengths
            {
                get => _lengths;
                set
                {
                    if (value is null)
                    {

#if NET5_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET46_OR_GREATER
                        _lengths = Array.Empty<int>();
#else
                        _lengths = ArrayEx.Empty<int>();
#endif
                    }
                    else _lengths = value.Select(x => x <= 0 ? 1 : x).ToArray();
                }
            }

            public int GetCharLengthA(char ch)
            {
                if (TreatDBytesTableLineAsByte && "─│┌┬┐├┼┤└┴┘".Contains(ch))
                    return 1;
                else return ch.GetLengthA();
            }

            public int GetStringLengthA(string str) => str.Sum(ch => GetCharLengthA(ch));
        }

        public static string GetAlignConsoleLine(string[] cols, AlignLineOptions options)
        {
            if (cols.Length != options.Lengths.Length) throw new ArgumentException($"The length of the argument `{nameof(cols)}` and `{nameof(options.Lengths)}` must be same.");

            var lineCols = new string[cols.Length];
            Array.Copy(cols, lineCols, cols.Length);

            var sb = new StringBuilder(options.Lengths.Sum() + options.Borders.Sum(x => x.Length));

            var cellList = new List<string>();
            var needNewLine = true;
            while (needNewLine)
            {
                needNewLine = false;

                foreach (var (index, lineCol) in lineCols.Pairs())
                {
                    var length = options.Lengths[index];

                    if (options.GetStringLengthA(lineCol) <= length)
                    {
                        lineCols[index] = "";
                        cellList.Add(lineCol.PadRightA(length));
                    }
                    else
                    {
                        for (int i = 0, lineLengthA = 0; i < lineCol.Length; i++)
                        {
                            var chLengthA = options.GetCharLengthA(lineCol[i]);

                            if (lineLengthA + chLengthA <= length) lineLengthA += chLengthA;
                            else
                            {
                                var lineContent = lineCol.Substring(0, i);
                                cellList.Add(lineContent.PadRightA(length));

                                lineCols[index] = lineCol.Substring(i);
                                needNewLine = true;
                                break;
                            }
                        }
                    }
                }

                sb.AppendLine($"{options.Borders[0]}{cellList.Join(options.Borders[1])}{options.Borders[2]}");
                cellList.Clear();

                if (options.OverflowHidden) break;
            }

            sb.Length -= Environment.NewLine.Length;
            return sb.ToString();
        }

    }
}
