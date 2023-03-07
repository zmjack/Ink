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
            var autoSize = lengths is null;

            // Calculate lengths of each column
            if (lengths is null)
            {
                lengths = new int[props.Length];
                foreach (var (index, value) in props.AsIndexValuePairs())
                {
                    lengths[index] = value.Name.GetLengthA();
                }
            }

            foreach (var (index, value) in props.AsIndexValuePairs())
            {
                foreach (var model in models)
                {
                    if (autoSize || (index < lengths.Length && lengths[index] < 0))
                    {
                        var len = value.GetValue(model)?.ToString().GetLengthA() ?? 0;
                        if (len > lengths[index]) lengths[index] = len;
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
