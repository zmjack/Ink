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
            var line = new StringBuilder();

            // Calculate lengths of each column
            if (lengths is null)
            {
                lengths = new int[props.Length];
                foreach (var kv in props.AsKvPairs())
                {
                    lengths[kv.Key] = kv.Value.Name.GetLengthA();
                }
            }

            foreach (var kv in props.AsKvPairs())
            {
                foreach (var model in models)
                {
                    if (kv.Key < lengths.Length && lengths[kv.Key] < 0)
                    {
                        var len = kv.Value.GetValue(model)?.ToString().GetLengthA() ?? 0;
                        if (len > lengths[kv.Key]) lengths[kv.Key] = len;
                    }
                }
            }

            return SeamlessTable(
                headers: props.Select(x => x.Name).ToArray(),
                colLines: models.Select(model => props.Select(x => x.GetValue(model)?.ToString() ?? "").ToArray()).ToArray(),
                lengths: lengths);
        }

        /// <summary>
        /// Prints console seamless table.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="colLines"></param>
        /// <param name="lengths"></param>
        public static string SeamlessTable(string[] headers, string[][] colLines, int[] lengths)
        {
            var sb = new StringBuilder();

            var borderLine = "─";
            var borderCols = new string[lengths.Length];
            for (int i = 0; i < borderCols.Length; i++)
                borderCols[i] = borderLine.Repeat(lengths[i]);

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

                if (colLines.Any())
                {
                    sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
                    {
                        Lengths = lengths,
                        Borders = new[] { "├─", "┼─", "┤" },
                        TreatDBytesTableLineAsByte = true,
                    }));
                }
            }

            foreach (var colLine in colLines)
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
