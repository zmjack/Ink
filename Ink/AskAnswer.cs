using System.Text;

namespace Ink
{
    public class AskAnswer
    {
        public StringBuilder Buffer { get; set; }
        public string Value { get; set; }
        public AskAction Action { get; set; }
    }
}
