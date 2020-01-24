using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ParticleSandbox
{
    public class PerfLogger
    {
        private readonly Stopwatch _timer = new Stopwatch();
        private readonly List<double> _timings = new List<double>();


        public double Performance => _timings.Count <= 0 ? 0 : _timings.Average();


        public int TotalRecordings { get; set; } = 10;


        public void Start() => _timer.Start();

        
        public void LogPerf()
        {
            Stop();
            _timings.Add(_timer.Elapsed.TotalMilliseconds);

            if (_timings.Count >= TotalRecordings + 1)
                _timings.RemoveAt(0);

            _timer.Reset();
            Start();
        }


        public void Stop() => _timer.Stop();
    }
}
