using KDParticleEngine.Services;
using MathExpressionEngine;
using MathExpressionEngine.Expressions;
using System;
using System.Drawing;

namespace KDParticleEngine.Behaviors
{
    public class EaseInBehavior : EasingBehavior
    {
        public EaseInBehavior()
        {
        }


        public override void Update(TimeSpan timeElapsed)
        {
            Value = EaseInQuad(TimeElapsed, Start, Change, TotalTime);

            base.Update(timeElapsed);
        }


        private float EaseInQuad(float t, float b, float c, float d)
        {
            t /= d;

            return c * t * t + b;
        }
    }
}
