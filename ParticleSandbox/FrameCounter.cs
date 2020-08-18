using System.Collections.Generic;
using System.Linq;

namespace ParticleSandbox
{
    public class FrameCounter
    {
        public FrameCounter()
        {
        }

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;

        private readonly Queue<float> _sampleBuffer = new Queue<float>();

        public bool Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            this._sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (this._sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                this._sampleBuffer.Dequeue();
                AverageFramesPerSecond = this._sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
