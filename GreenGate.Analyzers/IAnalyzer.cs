using GreenGate.Analyzers.Results;
using System;

namespace GreenGate.Analyzers
{
    public interface IAnalyzer<in TInput, TOutput> where TOutput : BaseAnalyzerResult
    {
        event Action<string> OnTextReceived;

        void Subscribe(TInput input);
    }
}