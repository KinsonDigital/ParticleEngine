using System;

namespace ParticleEngine.Behaviors
{
    /// <summary>
    /// Represents a behavior that can be applied to a particle.
    /// </summary>
    public interface IBehavior
    {
        /// <summary>
        /// Gets the current value of the behavior.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the current amount of time that has elapsed for the behavior.
        /// </summary>
        double ElapsedTime { get; }

        /// <summary>
        /// The particle attribute to apply the behavior result to.
        /// </summary>
        ParticleAttribute ApplyToAttribute { get; }

        /// <summary>
        /// Gets a value indicating if the behavior is enabled.
        /// </summary>
        bool Enabled { get; }


        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        void Update(TimeSpan timeElapsed);


        /// <summary>
        /// Resets the behavior.
        /// </summary>
        void Reset();
    }
}
