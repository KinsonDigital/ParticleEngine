using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public abstract class EasingBehavior : IBehavior
    {
        public EasingBehavior() { }


        public float Start { get; set; }

        public float Change { get; set; }

        public float TotalTime { get; set; }

        public int ParticleID { get; set; } = -1;

        public int BehaviorSetupID { get; set; } = -1;

        public float Value { get; set; }

        public float TimeElapsed { get; set; }//seconds


        public virtual void Update(TimeSpan timeElapsed)
        {
            //TODO: Have this part managed by the ParticleEffect class
            //if (TimeElapsed >= TotalTime)
            //{
                //particle.IsAlive = false;
                //ParticleID = -1;
                //TimeElapsed = 0;
                //return;
            //}

            TimeElapsed += (float)timeElapsed.TotalSeconds;
        }


        //TODO: Remove
        //protected void ApplyToAttribute(Particle particle, float result)
        //{
        //    switch (ApplyToAttribute)
        //    {
        //        case ParticleAttribute.X:
        //            particle.Position = new PointF(result, particle.Position.Y);
        //            break;
        //        case ParticleAttribute.Y:
        //            particle.Position = new PointF(particle.Position.X, result);
        //            break;
        //        case ParticleAttribute.Angle:
        //            particle.Angle = result;
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
