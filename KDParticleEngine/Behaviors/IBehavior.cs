using System;

namespace KDParticleEngine.Behaviors
{
    public interface IBehavior
    {
        int ParticleID { get; set; }

        int BehaviorSetupID { get; set; }

        float Value { get; set; }


        void Update(TimeSpan timeElapsed);
    }
}
