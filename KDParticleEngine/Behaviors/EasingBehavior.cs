using KDParticleEngine.Services;
using System;

namespace KDParticleEngine.Behaviors
{
    public abstract class EasingBehavior : IBehavior
    {
        #region Private Fields
        private readonly BehaviorSetting _setting;
        private readonly IRandomizerService _randomizer;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EasingBehavior"/>.
        /// </summary>
        /// <param name="randomizer"></param>
        public EasingBehavior(BehaviorSetting setting, IRandomizerService randomizer)
        {
            _setting = setting;
            _randomizer = randomizer;
        }
        #endregion


        #region Props
        /// <summary>
        /// Gets or sets the starting value of the easing behavior.
        /// </summary>
        public double Start { get; set; }

        /// <summary>
        /// Gets or sets the amount of change to apply to the behavior value over time.
        /// </summary>
        public double Change { get; set; }

        /// <summary>
        /// Gets or sets the total amount of time to change the <see cref="Value"/>
        /// dictated by the <see cref="Change"/> amount.
        /// </summary>
        public double TotalTime { get; set; }

        /// <summary>
        /// Gets the current value of the behavior.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets the current amount of time that has elapsed for the behavior.
        /// </summary>
        public double ElapsedTime { get; private set; }

        /// <summary>
        /// Gets the attribute to apply the behavior value to.
        /// </summary>
        public ParticleAttribute ApplyToAttribute => _setting.ApplyToAttribute;

        /// <summary>
        /// Gets a value indicating if the behavior is enabled.
        /// </summary>
        public bool Enabled { get; private set; } = true;
        #endregion


        #region Public Methods
        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed for this update of the behavior.</param>
        public virtual void Update(TimeSpan timeElapsed)
        {
            ElapsedTime += timeElapsed.TotalSeconds;

            Enabled = ElapsedTime < TotalTime;
        }


        /// <summary>
        /// Resets the behavior.
        /// </summary>
        public void Reset()
        {
            Value = 0;
            Start = _randomizer.GetValue(_setting.StartMin, _setting.StartMax);
            Change = _randomizer.GetValue(_setting.ChangeMin, _setting.ChangeMax);
            TotalTime = _randomizer.GetValue(_setting.TotalTimeMin, _setting.TotalTimeMax);
            ElapsedTime = 0;
            Enabled = true;
        }
        #endregion
    }
}
