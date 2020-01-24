using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace KDParticleEngine
{
    /// <summary>
    /// Holds the particle setup settings data for the <see cref="ParticleEngine"/> to consume.
    /// </summary>
    public class ParticleEffect
    {
        //DEBUGGING - PERFORMANCE CHECKING
        //private Stopwatch _timer = new Stopwatch();
        //private List<double> _timings = new List<double>();
        //////////////////////////////////

        #region Private Fields
        private readonly IRandomizerService _randomizer;
        private readonly Dictionary<int, IBehavior[]> _behaviors = new Dictionary<int, IBehavior[]>();
        #endregion


        #region Constructors
        public ParticleEffect(string particleTextureName, BehaviorSetting[] settings, IRandomizerService randomizer)
        {
            ParticleTextureName = particleTextureName;
            BehaviorSettings = settings;
            _randomizer = randomizer;
        }
        #endregion


        #region Props
        public string ParticleTextureName { get; private set; }

        public BehaviorType TypeOfBehavior { get; set; }

        public ParticleAttribute ApplyBehaviorTo { get; set; }

        //TODO: Most likely remove all the props below
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

        public BehaviorSetting[] BehaviorSettings { get; }
        #endregion


        #region Public Methods
        public void Reset()
        {
            var keys = _behaviors.Keys.ToArray();

            foreach (var key in keys)
            {
                foreach (var item in _behaviors[key])
                {
                    item.Reset();
                }
            }
        }
        #endregion


        #region Private Methods
        #endregion
    }
}
