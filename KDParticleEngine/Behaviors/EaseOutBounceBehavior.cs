using KDParticleEngine.Services;
using MathExpressionEngine.Expressions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public class EaseOutBounceBehavior : EasingBehavior
    {
        public EaseOutBounceBehavior() { }


        public override void Update(TimeSpan timeElapsed)
        {
            Value = EaseOutBounce(TimeElapsed, Start, Change, TotalTime);

            base.Update(timeElapsed);
        }


        //TODO: This can be moved out to a helper/util/static method of some kind
        private float EaseOutBounce(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75f))
            {
                return c * (7.5625f * t * t) + b;
            }
            else if (t < (2 / 2.75f))
            {
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            }
            else if (t < (2.5f / 2.75f))
            {
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            }
            else
            {
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
            }
        }
    }
}
