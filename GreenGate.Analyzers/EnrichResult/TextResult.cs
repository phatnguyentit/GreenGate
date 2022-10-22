using System.Collections.Generic;
using System.Linq;

namespace GreenGate.Analyzers.Results
{
    public class TextResult : BaseAnalyzerResult
    {
        public TextResult(IEnumerable<string> lines)
        {
            Lines = lines;
            IsSuccess = lines.Any();
        }

        public TextResult(string text)
        {
            Text = text;
            IsSuccess = !string.IsNullOrWhiteSpace(text) && text.Length > 0;
        }

        public string Text { get; private set; }
        public IEnumerable<string> Lines { get; private set; }
    }
}