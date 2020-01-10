using System;

namespace KDParticleEngine.Behaviors
{
    public interface IBehavior
    {
        void Update(Particle particle, TimeSpan timeElapsed);
    }
}
