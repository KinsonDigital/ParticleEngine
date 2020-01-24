using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public abstract class EasingBehavior : IBehavior
    {
        private IRandomizerService _randomizer;


        public EasingBehavior(IRandomizerService randomizer)
        {
            _randomizer = randomizer;
        }


        public double Start { get; set; }

        public double Change { get; set; }

        public double TotalTime { get; set; }

        public double Value { get; set; }

        public BehaviorSetting Settings { get; set; }

        public double TimeElapsed { get; private set; }


        public void Reset()
        {
            Value = 0;
            Start = _randomizer.GetValue(Settings.StartMin, Settings.StartMax);
            Change = _randomizer.GetValue(Settings.ChangeMin, Settings.ChangeMax);
            TotalTime = _randomizer.GetValue(Settings.TotalTimeMin, Settings.TotalTimeMax);
            TimeElapsed = 0;
        }


        public virtual void Update(TimeSpan timeElapsed)
        {
            TimeElapsed += timeElapsed.TotalSeconds;
        }
    }
}
