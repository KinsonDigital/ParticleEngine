using System;

namespace KDParticleEngine.Behaviors
{
    //TODO: Add code docs
    public class RandomChoiceBehaviorSettings : BehaviorSettings
    {
        #region Constructors
        public RandomChoiceBehaviorSettings() => TypeOfBehavior = BehaviorType.RandomChoice;
        #endregion


        #region Props
        /// <summary>
        /// Holds data for the use by a <see cref="IBehavior"/> implementation.
        /// </summary>
        public string[]? Data { get; set; }

        /// <summary>
        /// Gets or sets the amount of time that the <see cref="RandomColorBehavior"/> should be enabled.
        /// </summary>
        public double LifeTime { get; set; }
        #endregion


        #region Public Methods
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is RandomChoiceBehaviorSettings setting))
                return false;


            return TypeOfBehavior == setting.TypeOfBehavior &&
                ApplyToAttribute == setting.ApplyToAttribute;
        }


        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => HashCode.Combine(Data);
        #endregion
    }
}
