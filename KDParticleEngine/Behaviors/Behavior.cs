﻿using System;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Represents a behavior that can be applied to a particle.
    /// </summary>
    public abstract class Behavior : IBehavior
    {
        #region Private Fields
        private readonly BehaviorSettings _setting;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of behavior.
        /// </summary>
        /// <param name="settings">The settings used to dictate how the behavior makes a particle behave.</param>
        public Behavior(BehaviorSettings settings) => _setting = settings;
        #endregion


        #region Props
        /// <summary>
        /// Gets the current value of the behavior.
        /// </summary>
        public string Value { get; protected set; } = "0";

        /// <summary>
        /// Gets the current amount of time that has elapsed for the behavior in milliseconds.
        /// </summary>
        public double ElapsedTime { get; protected set; }

        /// <summary>
        /// Gets the attribute to apply the behavior value to.
        /// </summary>
        public ParticleAttribute ApplyToAttribute => _setting.ApplyToAttribute;

        /// <summary>
        /// Gets a value indicating if the behavior is enabled.
        /// </summary>
        public bool Enabled { get; protected set; } = true;
        #endregion


        #region Public Methods
        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        public virtual void Update(TimeSpan timeElapsed) => ElapsedTime += timeElapsed.TotalMilliseconds;


        /// <summary>
        /// Resets the behvior.
        /// </summary>
        public virtual void Reset()
        {
            Value = string.Empty;
            ElapsedTime = 0;
            Enabled = true;
        }
        #endregion
    }
}
