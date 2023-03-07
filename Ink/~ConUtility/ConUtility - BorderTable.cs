using NStandard;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ink
{
    public partial class ConUtility
    {
        /// <summary>
        /// Prints console table for models.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="models"></param>
        public static string Table<TModel>(IEnumerable<TModel> models, int[] lengths = null)
        {
            var props = typeof(TModel).GetProperties();
            var headers = props.Select(x => x.Name).ToArray();
            var lines = models.Select(model => props.Select(x => x.GetValue(model)?.ToString() ?? "").ToArray()).ToArray();
            return Table(headers, lines, lengths);
        }

        /// <summary>
        /// Prints console table for models.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="lines"></param>
        /// <param name="lengths"></param>
        public static string Table(string[] headers, string[][] lines, int[] lengths = null)
        {
            lengths ??= GetLengths(headers, lines);

            var sb = new StringBuilder();

            var borderLine = "-";
            var borderCols = new string[lengths.Length];
            for (int i = 0; i < borderCols.Length; i++)
            {
                borderCols[i] = borderLine.Repeat(lengths[i]);
            }

            sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "+-", "-+-", "-+" },
                TreatDBytesTableLineAsByte = false,
            }));

            if (headers is not null)
            {
                sb.AppendLine(GetAlignConsoleLine(headers, new AlignLineOptions
                {
                    Lengths = lengths,
                    Borders = new[] { "| ", " | ", " |" },
                    TreatDBytesTableLineAsByte = false,
                }));

                if (lines.Any())
                {
                    sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
                    {
                        Lengths = lengths,
                        Borders = new[] { "+-", "-+-", "-+" },
                        TreatDBytesTableLineAsByte = false,
                    }));
                }
            }

            foreach (var colLine in lines)
            {
                sb.AppendLine(GetAlignConsoleLine(colLine, new AlignLineOptions
                {
                    Lengths = lengths,
                    Borders = new[] { "| ", " | ", " |" },
                    TreatDBytesTableLineAsByte = false,
                }));
            }

            sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "+-", "-+-", "-+" },
                TreatDBytesTableLineAsByte = false,
            }));

            return sb.ToString();
        }

    }
}
