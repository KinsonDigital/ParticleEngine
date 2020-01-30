namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Stores settings for creating an <see cref="EasingBehavior"/>.
    /// </summary>
    public class BehaviorSetting
    {
        #region Props
        /// <summary>
        /// Gets or sets the type of behavior to be created.
        /// </summary>
        public BehaviorType TypeOfBehavior { get; set; }

        /// <summary>
        /// The particle attribute to set the behavior value to.
        /// </summary>
        public ParticleAttribute ApplyToAttribute { get; set; }

        /// <summary>
        /// The starting value minimum used in randomization.
        /// </summary>
        public float StartMin { get; set; }

        /// <summary>
        /// The starting value maximum used in randomization.
        /// </summary>
        public float StartMax { get; set; }

        /// <summary>
        /// The amount of change minimum used in randomization.
        /// </summary>
        public float ChangeMin { get; set; }

        /// <summary>
        /// The amount of change maximum used in randomization.
        /// </summary>
        public float ChangeMax { get; set; }

        /// <summary>
        /// The minimum total amount of time to complete the behavior used in randomization.
        /// </summary>
        public float TotalTimeMin { get; set; }

        /// <summary>
        /// The maximum total amount of time to complete the behavior used in randomization.
        /// </summary>
        public float TotalTimeMax { get; set; }
        #endregion
    }
}
