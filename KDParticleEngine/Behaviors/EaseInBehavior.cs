using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public class EaseInBehavior
    {
        private float _timeElapsed;//milliseconds


        public EaseInBehavior(float destination, float totalTime)
        {
            Destination = destination;
            TotalTime = totalTime;
        }


        public float Destination { get; }

        public float TotalTime { get; }


        public void Update(Particle particle, TimeSpan timeElapsed)
        {
            var result = EaseInQuad(_timeElapsed, particle.Position.X, Destination - particle.Position.X, TotalTime);

            particle.Position = new PointF(result, particle.Position.Y);

            _timeElapsed += (float)timeElapsed.TotalMilliseconds;
        }


        private float EaseInQuad(float t, float b, float c, float d)
        {
            t /= d;

            return c * t * t + b;
        }
    }
}
