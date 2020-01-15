using KDParticleEngine.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine
{
    //TODO: Add code docs and regions
    public class BehaviorSetup
    {
        //TODO: Make sure that this does not get saved into the JSON data in the ParticleMaker app
        public int ID { get; set; }

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
