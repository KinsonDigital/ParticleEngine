using System;

namespace KDParticleEngine.Behaviors
{
    public interface IBehavior
    {
        double Value { get; }

        double TimeElapsed { get; }

        public BehaviorSetting Settings { get; set; }


        void Update(TimeSpan timeElapsed);


        void Reset();
    }
}
