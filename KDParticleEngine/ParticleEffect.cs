using KDParticleEngine.Behaviors;
using System.Drawing;

namespace KDParticleEngine
{
    /// <summary>
    /// Holds the particle setup settings data for the <see cref="ParticleEngine"/> to consume.
    /// </summary>
    public class ParticleEffect
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ParticleEffect"/>.
        /// </summary>
        /// <param name="particleTextureName">The name of the texture used in the particle effect.</param>
        /// <param name="settings">The settings used to setup the particle effect.</param>
        public ParticleEffect(string particleTextureName, BehaviorSetting[] settings)
        {
            ParticleTextureName = particleTextureName;
            BehaviorSettings = settings;
        }
        #endregion


        #region Props
        /// <summary>
        /// Gets the name of the particle texture used in the particle effect.
        /// </summary>
        public string ParticleTextureName { get; private set; }

        /// <summary>
        /// Gets or sets the type of behavior that this particle effect will have.
        /// </summary>
        public BehaviorType TypeOfBehavior { get; set; }

        /// <summary>
        /// Gets or sets the particle attribute to set the behavior to.
        /// </summary>
        public ParticleAttribute ApplyBehaviorTo { get; set; }

        /// <summary>
        /// Gets or sets the location on the screen of where to spawn the <see cref="Particle"/>s.
        /// </summary>
        public PointF SpawnLocation { get; set; }

        /// <summary>
        /// Gets or sets the list of colors that the <see cref="ParticleEngine"/> will
        /// randomly choose from when spawning a new <see cref="Particle"/>.
        /// Only used if the <see cref="UseColorsFromList"/> is set to true.
        /// </summary>
        public Color[] TintColors { get; set; } = new Color[0];

        /// <summary>
        /// Gets or sets the total number of particles that can be alive at once.
        /// </summary>
        public int TotalParticlesAliveAtOnce { get; set; } = 1;

        /// <summary>
        /// Gets or sets the minimum spawn rate of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public int SpawnRateMin { get; set; } = 250;

        /// <summary>
        /// Gets or sets the maximum spawn rate of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public int SpawnRateMax { get; set; } = 1000;

        /// <summary>
        /// Gets or sets a value indicating if the colors will be randomly chosen from a list.
        /// </summary>
        public bool UseColorsFromList { get; set; }

        /// <summary>
        /// Gets the list of behavior settings that describe how the particle effect is setup.
        /// </summary>
        public BehaviorSetting[] BehaviorSettings { get; }
        #endregion
    }
}
