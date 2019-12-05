using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NEcho.Test
{
    public class ConArgsTests
    {
        [Fact]
        public void Test1()
        {
            var conArgs = new ConArgs(new[]
            {
                "-h", "127.0.0.1", "-u" ,"root", "C:\\Program Files", "-h", "2130706433", "-p"
            }, new[] { "-" });
            var actual = conArgs.Properties.ToDictionary(x => x.Key, x => x.Value.ToArray());

            Assert.Equal(new Dictionary<string, string[]>
            {
                ["-h"] = new[] { "127.0.0.1", "2130706433" },
                ["-u"] = new[] { "root" },
                ["-p"] = new[] { "" },
            }, actual);

            Assert.Equal("127.0.0.1", conArgs["-h"]);
            Assert.Equal("root", conArgs["-u"]);
            Assert.Equal(string.Empty, conArgs["-p"]);
            Assert.Null(conArgs["-t"]);

            Assert.Equal(new[] { "C:\\Program Files" }, conArgs.Contents);
            Assert.Equal("C:\\Program Files", conArgs[0]);
        }

        [Fact]
        public void Test2()
        {
            var conArgs = new ConArgs(@"-h 127.0.0.1 -u root ""C:\Program Files"" -h 2130706433 -p", new[] { "-" });
            var actual = conArgs.Properties.ToDictionary(x => x.Key, x => x.Value.ToArray());

            Assert.Equal(new Dictionary<string, string[]>
            {
                ["-h"] = new[] { "127.0.0.1", "2130706433" },
                ["-u"] = new[] { "root" },
                ["-p"] = new[] { "" },
            }, actual);

            Assert.Equal("127.0.0.1", conArgs["-h"]);
            Assert.Equal("root", conArgs["-u"]);
            Assert.Equal(string.Empty, conArgs["-p"]);
            Assert.Null(conArgs["-t"]);

            Assert.Equal(new[] { "C:\\Program Files" }, conArgs.Contents);
            Assert.Equal("C:\\Program Files", conArgs[0]);
        }
    }
}
