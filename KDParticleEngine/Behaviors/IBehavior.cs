using System;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Represents a behavior over time.
    /// </summary>
    public interface IBehavior
    {
        #region Props
        /// <summary>
        /// Gets the current value of the behavior.
        /// </summary>
        double Value { get; }

        /// <summary>
        /// Gets the current amount of time that has elapsed for the behavior.
        /// </summary>
        double ElapsedTime { get; }

        /// <summary>
        /// The particle attribute to set the behavior value to.
        /// </summary>
        ParticleAttribute ApplyToAttribute { get; }

        /// <summary>
        /// Gets a value indicating if the behavior is enabled.
        /// </summary>
        bool Enabled { get; }
        #endregion


        #region Public Methods
        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed for this update of the behavior.</param>
        void Update(TimeSpan timeElapsed);


        /// <summary>
        /// Resets the behavior.
        /// </summary>
        void Reset();
        #endregion
    }
}
