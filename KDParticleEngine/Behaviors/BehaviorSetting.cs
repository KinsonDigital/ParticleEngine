using KDParticleEngine.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    //TODO: Add code docs and regions
    public class BehaviorSetting
    {
        public BehaviorType TypeOfBehavior { get; set; }

        public ParticleAttribute ApplyToAttribute { get; set; }

        public float StartMin { get; set; }

        public float StartMax { get; set; }

        public float ChangeMin { get; set; }

        public float ChangeMax { get; set; }

        public float TotalTimeMin { get; set; }

        public float TotalTimeMax { get; set; }
    }
}
