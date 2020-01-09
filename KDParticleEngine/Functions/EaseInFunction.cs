using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Functions
{
    public class EaseInFunction : Function
    {
        public override float Run()
        {
            var t = GetInputValue("t");
            var d = GetInputValue("d");
            var c = GetInputValue("c");
            var b = GetInputValue("b");

            t /= d;

            Result = c * t * t * b;

            return base.Run();//Must be executed
        }


        protected override void InitInputs()
        {
            //Setup the inputs
            AddInput("t", InputType.X, 0);
            AddInput("b", InputType.X, 0);
            AddInput("c", InputType.X, 0);
            AddInput("d", InputType.X, 0);
        }
    }
}
