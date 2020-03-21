using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoSharp
{
    public partial class ConUtility
    {
        public class AlignLineOptions
        {
            public bool OverflowHidden { get; set; } = false;
            public bool TreatDBytesTableLineAsByte { get; set; } = false;
            public string[] Borders { get; set; } = new[] { "", "  ", "" };
            public int[] Lengths { get; set; } = null;

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
            if (cols.Length != options.Lengths.Length)
                throw new ArgumentException($"The length of the argument `{nameof(cols)}` and `{nameof(options.Lengths)}` must be same.");

            var lineCols = new string[cols.Length];
            Array.Copy(cols, lineCols, cols.Length);

            var sb = new StringBuilder(options.Lengths.Sum() + options.Borders.Sum(x => x.Length));

            var cellList = new List<string>();
            var needNewLine = true;
            while (needNewLine)
            {
                needNewLine = false;
                foreach (var kv in lineCols.AsKvPairs())
                {
                    var lineCol = kv.Value;
                    var length = options.Lengths[kv.Key];

                    if (options.GetStringLengthA(lineCol) <= length)
                    {
                        lineCols[kv.Key] = "";
                        cellList.Add(lineCol.PadRightA(length));
                    }
                    else
                    {
                        for (int i = 0, lineLengthA = 0; i < lineCol.Length; i++)
                        {
                            var chLengthA = options.GetCharLengthA(lineCol[i]);

                            if (lineLengthA + chLengthA <= length)
                                lineLengthA += chLengthA;
                            else
                            {
                                var lineContent = lineCol.Substring(0, i);
                                cellList.Add(lineContent.PadRightA(length));

                                lineCols[kv.Key] = lineCol.Substring(i);
                                needNewLine = true;
                                break;
                            }
                        }
                    }
                }

                sb.AppendLine($"{options.Borders[0]}{cellList.Join(options.Borders[1])}{options.Borders[2]}");
                cellList.Clear();

                if (options.OverflowHidden)
                    break;
            }

            sb.Length -= Environment.NewLine.Length;
            return sb.ToString();
        }

    }
}
