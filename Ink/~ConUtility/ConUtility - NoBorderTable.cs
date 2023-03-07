using NStandard;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ink
{
    public partial class ConUtility
    {
        /// <summary>
        /// Prints console table with no border for models.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="models"></param>
        public static string NoBorderTable<TModel>(IEnumerable<TModel> models, int[] lengths = null)
        {
            var props = typeof(TModel).GetProperties();
            var headers = props.Select(x => x.Name).ToArray();
            var lines = models.Select(model => props.Select(x => x.GetValue(model)?.ToString() ?? "").ToArray()).ToArray();
            return NoBorderTable(headers, lines, lengths);
        }

        /// <summary>
        /// Prints console table with no border for models.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="lines"></param>
        /// <param name="lengths"></param>
        public static string NoBorderTable(string[] headers, string[][] lines, int[] lengths = null)
        {
            lengths ??= GetLengths(headers, lines);
            var options = new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "", "  ", "" },
                TreatDBytesTableLineAsByte = false,
            };

            var sb = new StringBuilder();
            var borderLine = "=";
            var borderCols = new string[lengths.Length];
            for (int i = 0; i < borderCols.Length; i++)
            {
                borderCols[i] = borderLine.Repeat(lengths[i]);
            }

            if (headers is not null)
            {
                sb.AppendLine(GetAlignConsoleLine(headers, options));

                if (lines.Any())
                {
                    sb.AppendLine(GetAlignConsoleLine(borderCols, options));
                }
            }

            foreach (var line in lines)
            {
                sb.AppendLine(GetAlignConsoleLine(line, options));
            }

            return sb.ToString();
        }

    }
}
