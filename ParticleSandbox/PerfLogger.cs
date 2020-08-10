using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParticleSandbox
{
    public class PerfLogger
    {
        private readonly Stopwatch _timer = new Stopwatch();
        private readonly List<double> _timings = new List<double>();


        public double Performance => this._timings.Count <= 0 ? 0 : this._timings.Average();


        public int TotalRecordings { get; set; } = 10;


        public void Start() => this._timer.Start();

        
        public void LogPerf()
        {
            Stop();
            this._timings.Add(this._timer.Elapsed.TotalMilliseconds);

            if (this._timings.Count >= TotalRecordings + 1)
                this._timings.RemoveAt(0);

            this._timer.Reset();
            Start();
        }


        public void Stop() => this._timer.Stop();
    }
}
