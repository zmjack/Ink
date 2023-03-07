using NStandard;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ink
{
    public partial class ConUtility
    {
        /// <summary>
        /// Prints console seamless table for models.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="models"></param>
        public static string SeamlessTable<TModel>(IEnumerable<TModel> models, int[] lengths = null)
        {
            var props = typeof(TModel).GetProperties();
            var headers = props.Select(x => x.Name).ToArray();
            var lines = models.Select(model => props.Select(x => x.GetValue(model)?.ToString() ?? "").ToArray()).ToArray();
            return SeamlessTable(headers, lines, lengths);
        }

        /// <summary>
        /// Prints console seamless table.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="lines"></param>
        /// <param name="lengths"></param>
        public static string SeamlessTable(string[] headers, string[][] lines, int[] lengths)
        {
            lengths ??= GetLengths(headers, lines);

            var sb = new StringBuilder();

            var borderLine = "─";
            var borderCols = new string[lengths.Length];
            for (int i = 0; i < borderCols.Length; i++)
            {
                borderCols[i] = borderLine.Repeat(lengths[i]);
            }

            sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "┌─", "┬─", "┐" },
                TreatDBytesTableLineAsByte = true,
            }));

            if (headers is not null)
            {
                sb.AppendLine(GetAlignConsoleLine(headers, new AlignLineOptions
                {
                    Lengths = lengths,
                    Borders = new[] { "│ ", "│ ", "│" },
                    TreatDBytesTableLineAsByte = true,
                }));

                if (lines.Any())
                {
                    sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
                    {
                        Lengths = lengths,
                        Borders = new[] { "├─", "┼─", "┤" },
                        TreatDBytesTableLineAsByte = true,
                    }));
                }
            }

            foreach (var colLine in lines)
            {
                sb.AppendLine(GetAlignConsoleLine(colLine, new AlignLineOptions
                {
                    Lengths = lengths,
                    Borders = new[] { "│ ", "│ ", "│" },
                    TreatDBytesTableLineAsByte = true,
                }));
            }

            sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "└─", "┴─", "┘" },
                TreatDBytesTableLineAsByte = true,
            }));

            return sb.ToString();
        }

    }
}
