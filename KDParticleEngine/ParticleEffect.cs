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
        private Stopwatch _timer = new Stopwatch();
        private List<double> _timings = new List<double>();
        //////////////////////////////////
        
        #region Private Fields
        private float _angleMax;
        private float _angleMin;
        private readonly List<IBehavior> _behaviors = new List<IBehavior>();
        private readonly IRandomizerService _randomizer;
        private readonly BehaviorSetup[] _behaviorSetups;
        #endregion


        #region Constructors
        public ParticleEffect(string particleTextureName, BehaviorSetup[] setups, IRandomizerService randomizer)
        {
            ParticleTextureName = particleTextureName;
            _randomizer = randomizer;

            //Reset all of the ID numbers just in case
            setups.ToList().ForEach(s => s.ID = -1);

            setups.ToList().ForEach(s =>
            {
                s.ID = GetNewSetupId(setups);
            });

            _behaviorSetups = setups;
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


        #region Public Methods
        public void Update(Particle particle, TimeSpan timeElapsed)
        {
            _timer.Start();

            //Get all of the behaviors for the current particle
            var particleBehaviors = _behaviors.Where(b =>
            {
                return b.GetType().IsSubclassOf(typeof(EasingBehavior)) && b.ParticleID == particle.ID;
            }).Select(b => b as EasingBehavior).ToList();


            //Update all of the behaviors
            particleBehaviors.ForEach(b => b?.Update(timeElapsed));

            //Apply the behavior values to the particle attributes
            particleBehaviors.ForEach(b =>
            {
                if (b is null || b.TimeElapsed >= b.TotalTime)
                    return;

                var foundSetup = _behaviorSetups.FirstOrDefault(s => s.ID == b.BehaviorSetupID);

                if (foundSetup is null)
                    return;

                static byte ClampClrValue(float value)
                {
                    var result = (byte)(value > 255 ? 255 : value);


                    return (byte)(value < 0 ? 0 : value);
                }


                switch (foundSetup.ApplyToAttribute)
                {
                    case ParticleAttribute.X:
                        particle.Position = new PointF(b.Value, particle.Position.Y);
                        break;
                    case ParticleAttribute.Y:
                        particle.Position = new PointF(particle.Position.X, b.Value);
                        break;
                    case ParticleAttribute.Angle:
                        particle.Angle = b.Value;
                        break;
                    case ParticleAttribute.Size:
                        particle.Size = b.Value;
                        break;
                    case ParticleAttribute.RedChannel:
                        var redValue = ClampClrValue(b.Value);

                        particle.TintColor = Color.FromArgb(particle.TintColor.A, redValue, particle.TintColor.G, particle.TintColor.B);
                        break;
                    case ParticleAttribute.GreenChannel:
                        var greenValue = ClampClrValue(b.Value);

                        particle.TintColor = Color.FromArgb(particle.TintColor.A, particle.TintColor.R, greenValue, particle.TintColor.B);
                        break;
                    case ParticleAttribute.BlueChannel:
                        var blueValue = ClampClrValue(b.Value);

                        particle.TintColor = Color.FromArgb(particle.TintColor.A, particle.TintColor.R, particle.TintColor.G, blueValue);
                        break;
                    case ParticleAttribute.AlphaChannel:
                        var alphaValue = ClampClrValue(b.Value);

                        particle.TintColor = Color.FromArgb(alphaValue, particle.TintColor.R, particle.TintColor.G, particle.TintColor.B);
                        break;
                    default:
                        break;
                }
            });

            
            //If all of the behaviors have ran there course, kill the particle and reset the time elapsed for the behaviors
            if (particleBehaviors.All(b => b?.TimeElapsed >= b?.TotalTime))
            {
                particleBehaviors.ForEach(b =>
                {
                    if (b is null)
                        return;

                    b.TimeElapsed = 0;
                    b.Value = 0;
                });
                particle.IsDead = true;
            }

            _timer.Stop();

            _timings.Add(_timer.Elapsed.TotalMilliseconds);

            var perfResult = _timings.Count <= 0 ? 0 : _timings.Average();

            /*Per Results (ms):
             * 1 x P = 0.0107
             * 
             */
            _timer.Reset();
        }


        public void CreateParticleBehaviors(int particleId)
        {
            //Each particle with the given ID will get every single behavior
            //dictated by the behavior setups
            foreach (var setup in _behaviorSetups)
            {
                switch (setup.TypeOfBehavior)
                {
                    case BehaviorType.EaseOutBounce:
                        _behaviors.Add(new EaseOutBounceBehavior() { ParticleID = particleId, BehaviorSetupID = setup.ID });
                        break;
                    case BehaviorType.EaseIn:
                        _behaviors.Add(new EaseInBehavior() { ParticleID = particleId, BehaviorSetupID = setup.ID });
                        break;
                    default:
                        break;
                }
            }
        }


        public void ResetBehaviors(int particleId)
        {
            var foundBehaviors = _behaviors.Where(b => b.ParticleID == particleId && b is EasingBehavior).Select(b => b as EasingBehavior).ToList();

            foundBehaviors.ForEach(b =>
            {
                if (b is null)
                    return;

                var foundSetup = _behaviorSetups.FirstOrDefault(s => s.ID == b.BehaviorSetupID);

                b.Start = _randomizer.GetValue(foundSetup.StartMin, foundSetup.StartMax);
                b.Change = _randomizer.GetValue(foundSetup.ChangeMin, foundSetup.ChangeMax);
                b.TotalTime = _randomizer.GetValue(foundSetup.TotalTimeMin, foundSetup.TotalTimeMax);
            });

        }
        #endregion


        #region Private Methods
        private int GetNewSetupId(BehaviorSetup[] setups) => setups == null || setups.Length <= 0 ? 0 : setups.Max(s => s.ID) + 1;
        #endregion
    }
}
