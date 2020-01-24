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
        public EaseOutBounceBehavior(IRandomizerService randomizer) : base (randomizer) { }


        public override void Update(TimeSpan timeElapsed)
        {
            Value = EaseOutBounce(TimeElapsed, Start, Change, TotalTime);

            base.Update(timeElapsed);
        }


        //TODO: This can be moved out to a helper/util/static method of some kind
        private double EaseOutBounce(double t, double b, double c, double d)
        {
            if ((t /= d) < 0.36363636363636363636363636363636)
            {
                return c * (7.5625 * t * t) + b;
            }
            else if (t < 0.72727272727272727272727272727273)
            {
                return c * (7.5625 * (t -= 0.54545454545454545454545454545455) * t + 0.75) + b;
            }
            else if (t < 0.90909090909090909090909090909091)
            {
                return c * (7.5625 * (t -= 0.81818181818181818181818181818182) * t + 0.9375) + b;
            }
            else
            {
                return c * (7.5625 * (t -= 0.9) * t + 0.95454545454545454545454545454545) + b;
            }
        }
    }
}
