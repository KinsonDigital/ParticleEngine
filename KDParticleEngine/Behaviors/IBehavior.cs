using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public interface IBehavior
    {
        void Update(Particle particle, TimeSpan timeElapsed);
    }
}
