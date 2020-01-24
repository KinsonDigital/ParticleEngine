using KDParticleEngine.Services;
using MathExpressionEngine;
using MathExpressionEngine.Expressions;
using System;
using System.Drawing;

namespace KDParticleEngine.Behaviors
{
    public class EaseInBehavior : EasingBehavior
    {
        public EaseInBehavior(IRandomizerService randomizer) : base(randomizer) { }


        public override void Update(TimeSpan timeElapsed)
        {
            Value = EaseInQuad(TimeElapsed, Start, Change, TotalTime);

            base.Update(timeElapsed);
        }


        private double EaseInQuad(double t, double b, double c, double d)
        {
            t /= d;

            return c * t * t + b;
        }
    }
}
