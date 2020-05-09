namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Represents different types of behaviors that can be applied to a <see cref="Particle"/>.
    /// </summary>
    public enum BehaviorType
    {
        /// <summary>
        /// An unknown behavior.
        /// </summary>
        Unknown,
        /// <summary>
        /// Ease out bounce easing funtion behavior.
        /// </summary>
        EaseOutBounce,
        /// <summary>
        /// Ease in easing function behavior.
        /// </summary>
        EaseIn,
        /// <summary>
        /// Random choice behavior.
        /// </summary>
        RandomChoice
    }
}
