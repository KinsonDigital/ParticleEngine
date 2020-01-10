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
            _expression = Compiler.Compile("$c*($t/$d)*($t/$d)+$b");
        }


        public void Update(Particle particle, TimeSpan timeElapsed)
        {
            _timeElapsed += (float)timeElapsed.TotalSeconds;

            _expression.UpdateVariable("t", _timeElapsed);
            _expression.UpdateVariable("d", 100);//Start
            _expression.UpdateVariable("s", 4);

            var result = (float)_expression.Eval();

            particle.Position = new PointF(result, particle.Position.Y);
        }


        private float EaseInQuad(float t, float b, float c, float d)
        {
            t /= d;

            return c * t * t + b;
        }
    }
}
