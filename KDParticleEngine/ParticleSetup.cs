using System.Drawing;

namespace KDParticleEngine
{
    /// <summary>
    /// Holds the particle setup settings data for the <see cref="ParticleEngine"/> to consume.
    /// </summary>
    public class ParticleSetup<Texture> where Texture : class
    {
        #region Private Fields
        private float _angleMax;
        private float _angleMin;
        #endregion


        #region Props
        public Texture ParticleTexture { get; set; }

        /// <summary>
        /// Gets or sets the location on the screen of where to spawn the <see cref="Particle"/>s.
        /// </summary>
        public PointF SpawnLocation { get; set; }

        /// <summary>
        /// Gets or sets the minimum value of the red color component range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public byte RedMin { get; set; }

        /// <summary>
        /// Gets or sets the maximum value of the red color component range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public byte RedMax { get; set; } = 255;

        /// <summary>
        /// Gets or sets the minimum value of the green color component range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public byte GreenMin { get; set; }

        /// <summary>
        /// Gets or sets the maximum value of the green color component range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public byte GreenMax { get; set; } = 255;

        /// <summary>
        /// Gets or sets the minimum value of the blue color component range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public byte BlueMin { get; set; }

        /// <summary>
        /// Gets or sets the maximum value of the blue color component range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public byte BlueMax { get; set; } = 255;

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
        /// Gets or sets the minimum size of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float SizeMin { get; set; } = 0.5f;

        /// <summary>
        /// Gets or sets the maximum size of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float SizeMax { get; set; } = 1.5f;

        /// <summary>
        /// Gets or sets the minimum angle of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float AngleMin
        {
            get => _angleMin;
            set
            {
                _angleMin = value;
                _angleMin = _angleMin < 0 ? 360 : _angleMin;
                _angleMin = _angleMin > 360 ? 0 : _angleMin;
            }
        }

        /// <summary>
        /// Gets or sets the maximum angle of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float AngleMax
        {
            get => _angleMax;
            set
            {
                _angleMax = value;
                _angleMax = _angleMax < 0 ? 360 : _angleMax;
                _angleMax = _angleMax > 360 ? 0 : _angleMax;
            }
        }

        /// <summary>
        /// Gets or sets the minimum angular velocity of the range that a <see cref="Particle"/> be will randomly set to.
        /// </summary>
        public float AngularVelocityMin { get; set; }

        /// <summary>
        /// Gets or sets the maximum angular velocity of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float AngularVelocityMax { get; set; } = 1.0f;

        /// <summary>
        /// Gets or sets the minimum X velocity of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float VelocityXMin { get; set; } = -1f;

        /// <summary>
        /// Gets or sets the maximum X velocity of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float VelocityXMax { get; set; } = 1f;

        /// <summary>
        /// Gets or sets the minimum X velocity of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float VelocityYMin { get; set; } = -1f;

        /// <summary>
        /// Gets or sets the maximum Y velocity of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public float VelocityYMax { get; set; } = 1f;

        /// <summary>
        /// Gets or sets a value that indicates if the <see cref="ParticleEngine"/> will 
        /// spawn <see cref="Particle"/>s with a random or set velocity.
        /// </summary>
        public bool UseRandomVelocity { get; set; } = true;

        /// <summary>
        /// Gets or sets the velocity of newly spawned <see cref="Particle"/>s. This is only used
        /// when the <see cref="UseRandomVelocity"/> property is set to false.
        /// <seealso cref="UseRandomVelocity"/>
        /// </summary>
        public PointF ParticleVelocity { get; set; } = new PointF(0, 1);

        /// <summary>
        /// Gets or sets the minimum life time of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public int LifeTimeMin { get; set; } = 250;

        /// <summary>
        /// Gets or sets the maximum life time of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public int LifeTimeMax { get; set; } = 1000;

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
        /// Gets or sets the list of colors to randomly choose from.
        /// </summary>
        public Color[] Colors { get; set; } = new Color[]
        {
            Color.FromArgb(255, 255, 0, 0 ),
            Color.FromArgb(255, 0, 255, 0 ),
            Color.FromArgb(255, 0, 0, 255 )
        };
        #endregion
    }
}
