using NStandard;
using System.Collections.Generic;
using System.Linq;
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

            return NoBorderTable(
                headers: props.Select(x => x.Name).ToArray(),
                colLines: models.Select(model => props.Select(x => x.GetValue(model)?.ToString() ?? "").ToArray()).ToArray(),
                lengths: lengths);
        }

        /// <summary>
        /// Prints console table with no border for models.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="colLines"></param>
        /// <param name="lengths"></param>
        public static string NoBorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            var sb = new StringBuilder();

            var options = new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "", "  ", "" },
                TreatDBytesTableLineAsByte = false,
            };

            if (headers is not null)
                sb.AppendLine(GetAlignConsoleLine(headers, options));

            foreach (var colLine in colLines)
                sb.AppendLine(GetAlignConsoleLine(colLine, options));

            return sb.ToString();
        }

    }
}
