using NStandard;
using System;

namespace Ink
{
    public class InkScope : Scope<InkScope>
    {
        public int CursorTop { get; private set; }
        public int CursorLeft { get; private set; }

        public InkScope()
        {
            CursorTop = Console.CursorTop;
            CursorLeft = Console.CursorLeft;
        }

        public override void Disposing()
        {
            Console.CursorTop = CursorTop;
            Console.CursorLeft = CursorLeft;
        }

    }
}
