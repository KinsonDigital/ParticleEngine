using MathExpressionEngine;
using MathExpressionEngine.Expressions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public class EaseInBehavior
    {
        private float _timeElapsed;//milliseconds
        private Expression _expression;


        public EaseInBehavior()
        {
            _expression = Compiler.Compile("$t*(t/$d)*(t/$d)+$s");
        }


        public void Update(Particle particle, TimeSpan timeElapsed)
        {
            _timeElapsed += (float)timeElapsed.TotalMilliseconds;
            //particle.Position = new PointF(result, particle.Position.Y);


        }


        private float EaseInQuad(float t, float b, float c, float d)
        {
            t /= d;

            return c * t * t + b;
        }
    }
}
