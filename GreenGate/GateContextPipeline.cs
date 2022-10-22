using GreenGate.Analyzers;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace GreenGate
{
    internal class GateContextPipeline
    {
        private static readonly object _lockObj = new object();
        private static GateContext _gateContext;
        private const int MaxImageScan = 5;
        private static int _scanCount = 0;

        public event Action<PipelineCompletedResult> OnPipelineCompleted;

        private readonly ConcurrentStack<GateContext> _cachedContext;
        private readonly ConcurrentStack<Bitmap> _inProcessImages;

        private readonly DoseCountAnalyzer _doseCountAnalyzer;

        public GateContextPipeline()
        {
            _cachedContext = new ConcurrentStack<GateContext>();
            _inProcessImages = new ConcurrentStack<Bitmap>();

            _doseCountAnalyzer = new DoseCountAnalyzer();
            _doseCountAnalyzer.OnDoseCountReveived += OnDoseCountReveived;
        }

        public void Push(Bitmap bitmap, string qrCode)
        {
            if (_cachedContext.LastOrDefault(ct => ct.QrCode.Equals(qrCode)) is GateContext gateContext)
            {
                OnPipelineCompleted(new PipelineCompletedResult
                {
                    QrCode = qrCode,
                    AlreadyScanned = true
                });
            }
            else if (CheckPipelineRunning(qrCode))
            {
                if (_inProcessImages.Count <= MaxImageScan)
                {
                    _inProcessImages.Push(bitmap);
                }
            }

        }

        public void Next()
        {
            if (_inProcessImages.TryPop(out var image))
            {
                _doseCountAnalyzer.Subscribe(image);
            }
        }

        private void OnDoseCountReveived(Analyzers.Results.DoseCountResult doseCountResult)
        {
            if (CheckPipelineRunning())
            {
                if (!doseCountResult.IsSuccess)
                {
                    if (_scanCount <= MaxImageScan)
                    {
                        StopForNext();
                        UpdateScanCount();
                    }
                    else
                    {
                        StopForNext();
                        CompletePipeline(true);
                    }
                }
                else
                {
                    StopForNext();
                    _gateContext.HasFirstDose = doseCountResult.DoseCount == 1;
                    _gateContext.HasSecondDose = doseCountResult.DoseCount == 2;
                    CompletePipeline();
                }
            }
        }

        public void StopForNext()
        {
            UpdateScanCount(true);
            _inProcessImages.Clear();
        }

        /// <summary>
        /// Complete pipeline and save information
        /// </summary>
        /// <param name="expired"></param>
        private void CompletePipeline(bool expired = false)
        {
            if (expired)
            {
                _gateContext = null;
                OnPipelineCompleted.Invoke(new PipelineCompletedResult(expired));
            }
            else
            {
                var clonedContext = (GateContext)_gateContext.Clone();
                _cachedContext.Push(clonedContext);
                _gateContext = null;
                var result = new PipelineCompletedResult
                {
                    Completed = clonedContext.IsGood,
                    HasSecondDose = clonedContext.HasSecondDose,
                    OnlyFisrtDose = clonedContext.HasFirstDose,
                    QrCode = clonedContext.QrCode
                };
                OnPipelineCompleted.Invoke(result);
            }
        }

        private static bool CheckPipelineRunning(string qrCode = null)
        {
            lock (_lockObj)
            {
                if (!string.IsNullOrWhiteSpace(qrCode))
                {
                    if (_gateContext is null) _gateContext = new GateContext(qrCode);
                }

                return _gateContext != null;
            }
        }

        private static void UpdateScanCount(bool reset = false)
        {
            lock (_lockObj)
            {
                if (reset)
                {
                    _scanCount = 0;
                }
                else
                {
                    Interlocked.Increment(ref _scanCount);
                }
            }
        }

        private void RemoveExpiredContext(object sender, EventArgs e)
        {
            if (_cachedContext.TryPeek(out var latestGateContext))
            {
                if (latestGateContext.ScanCountReached && !latestGateContext.Completed)
                {
                    _cachedContext.TryPop(out latestGateContext);
                }
            }

            ClearCache();
        }

        private void ClearCache(int cacheAllowed = 5)
        {
            if (_cachedContext.Count > 0)
            {
                var reverseStack = _cachedContext.ToList();
                while (reverseStack.Count() > cacheAllowed)
                {
                    reverseStack.RemoveAt(reverseStack.Count() - 1);
                }
                _cachedContext.Clear();
                _cachedContext.PushRange(reverseStack.Reverse<GateContext>().ToArray());
            }
        }
    }
}