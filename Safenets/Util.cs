using System;
using System.Linq;
using System.Collections.Generic;

namespace Safenets
{
    public static class Util
    {
        public static void DebugPrint(params object[] args) => Console.WriteLine(string.Concat("[SN] ", " ".OnJoin(args)));
        public static string OnJoin(this string delim, IEnumerable<object> args) => string.Join(delim, args.Select(o => o?.ToString() ?? "null").ToArray());
    }
}
