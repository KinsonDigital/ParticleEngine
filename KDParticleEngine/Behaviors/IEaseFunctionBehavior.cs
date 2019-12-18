using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public interface IEaseFunctionBehavior : IBehavior
    {
        float Destination { get; }

        float TotalTime { get; }//milliseconds
    }
}
