using GreenGate.Analyzers.Results;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace GreenGate.Analyzers
{
    public abstract class ImageAnalyzer<TOutput> : IAnalyzer<Bitmap, TOutput> where TOutput : BaseAnalyzerResult
    {
        private static string _currentPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

        public event Action<string> OnTextReceived;

        public abstract void Subscribe(Bitmap input);

        protected void SaveFile(Bitmap bitmap)
        {
            Task.Run(() =>
            {
                bitmap.Save($"{_currentPath}\\{Guid.NewGuid()}.jpg");
            });
        }
    }
}