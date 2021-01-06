﻿using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ink.Test
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

            Assert.Equal("127.0.0.1", conArgs["-h"].FirstOrDefault());
            Assert.Equal("root", conArgs["-u"].FirstOrDefault());
            Assert.Equal(string.Empty, conArgs["-p"].FirstOrDefault());
            Assert.Empty(conArgs["-t"]);

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

            Assert.Equal("127.0.0.1", conArgs["-h"].FirstOrDefault());
            Assert.Equal("root", conArgs["-u"].FirstOrDefault());
            Assert.Equal(string.Empty, conArgs["-p"].FirstOrDefault());
            Assert.Empty(conArgs["-t"]);

            Assert.Equal(new[] { "C:\\Program Files" }, conArgs.Contents);
            Assert.Equal("C:\\Program Files", conArgs[0]);
        }

        [Fact]
        public void Test3()
        {
            var conArgs = new ConArgs(@"-o -n -i Ink", new[] { "-" });
            var actual = conArgs.Properties.ToDictionary(x => x.Key, x => x.Value.ToArray());

            Assert.Equal(new Dictionary<string, string[]>
            {
                ["-o"] = new[] { "" },
                ["-n"] = new[] { "" },
                ["-i"] = new[] { "Ink" },
            }, actual);

            Assert.Equal("", conArgs["-o"].FirstOrDefault());
            Assert.Equal("", conArgs["-n"].FirstOrDefault());
            Assert.Equal("Ink", conArgs["-i"].FirstOrDefault());
        }

    }
}
