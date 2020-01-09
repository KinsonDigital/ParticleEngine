using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Functions
{
    public class MoveRightFunction : Function
    {
        protected override void InitInputs()
        {
            AddInput("x", InputType.X, 0);
            AddInput("speed", InputType.Constant, 0);
        }
    }
}
