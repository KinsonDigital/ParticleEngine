using KDParticleEngine.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public interface IBehavior
    {
        Dictionary<int, IFunction> ValueFunctions { get; }

        IApplyFunction ApplyFunction { get; }

        void Update(Particle particle, TimeSpan timeElapsed);
    }
}
