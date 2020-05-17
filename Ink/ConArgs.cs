using NStandard;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ink
{
    /// <summary>
    /// This class provides some methods to separate the console arguments to content or property.
    /// </summary>
    public class ConArgs
    {
        public string[] OriginArgs { get; private set; }
        public Dictionary<string, HashSet<string>> Properties { get; } = new Dictionary<string, HashSet<string>>();
        public string[] Contents { get; private set; }

        public ConArgs(string line, params string[] keyStarts)
        {
            var regex = new Regex(@" *(?:""(.+?)""(?: +|$)|([^ ]+)(?: +|$))*$");
            var match = regex.Match(line);
            var args = match.Groups.OfType<Group>()
                .Skip(1).Take(2)
                .SelectMany(x => x.Captures.OfType<Capture>())
                .OrderBy(x => x.Index)
                .Select(x => x.Value)
                .ToArray();

            Constructor(args, keyStarts);
        }
        public ConArgs(string[] args, params string[] keyStarts) => Constructor(args, keyStarts);

        private void Constructor(string[] args, params string[] keyStarts)
        {
            OriginArgs = args;
            var contents = new List<string>();

            string key = string.Empty;
            string value = string.Empty;

            foreach (var arg in args)
            {
                if (keyStarts.Any(x => arg.StartsWith(x)))
                {
                    if (!key.IsNullOrEmpty())
                        Properties[key].Add(string.Empty);

                    key = arg;
                    if (!Properties.ContainsKey(key))
                        Properties.Add(key, new HashSet<string>());
                }
                else
                {
                    if (!key.IsNullOrEmpty())
                    {
                        Properties[key].Add(arg);
                        key = string.Empty;
                    }
                    else contents.Add(arg);
                }
            }

            if (!key.IsNullOrEmpty())
                Properties[key].Add(string.Empty);

            Contents = contents.ToArray();
        }

        public string this[int n] => n < Contents.Length ? Contents[n] : null;
        public string[] this[string key]
        {
            get
            {
                if (Properties.ContainsKey(key))
                    return Properties[key].ToArray();
                else return null;
            }
        }

    }
}
