using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KDParticleEngine
{
    /// <summary>
    /// Represents a single particle with various properties that dictate how the <see cref="Particle"/>
    /// behaves and looks on the screen.
    /// </summary>
    public class Particle
    {
        private BehaviorSetting[] _settings;
        private IBehavior[] _behaviors;


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="Particle"/>.
        /// </summary>
        /// <param name="texture">The texture used for rendering the <see cref="Particle"/>.</param>
        /// <param name="position">The position of the particle.</param>
        /// <param name="velocity">The direction and speed at which the particle is moving.</param>
        /// <param name="angle">The angle that the particle starts when it is spawned.</param>
        /// <param name="angularVelocity">The speed at which the particle is rotating.</param>
        /// <param name="color">The color to tint the <see cref="Texture"/>.</param>
        /// <param name="size">The size of the <see cref="Particle"/>.</param>
        /// <param name="lifeTime">The amount of time in milliseconds for the particle to stay alive.</param>
        public Particle(PointF position, PointF velocity, float angle, float angularVelocity, Color color, float size, int lifeTime)
        {
            Position = position;
            Angle = angle;
            TintColor = color;
            Size = size;
            LifeTime = lifeTime;
        }


        public Particle(BehaviorSetting[] settings, IRandomizerService randomService)
        {
            _settings = settings;
            CreateBehaviors(randomService);
        }
        #endregion


        #region Props
        //TODO: Possibly remove
        public int ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the position of the <see cref="Particle"/>.
        /// </summary>
        public PointF Position { get; set; }

        /// <summary>
        /// Gets or sets the angle that the <see cref="Particle"/> is at when first spawned.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// Gets or sets the color that the <see cref="Texture"/> will be tinted.
        /// </summary>
        public Color TintColor { get; set; }

        /// <summary>
        /// Gets or sets the sized of the <see cref="Particle"/>.
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// Gets or sets the amount of time in milliseconds that the <see cref="Particle"/> will stay alive.
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Gets or sets if the <see cref="Particle"/> is alive or dead.
        /// </summary>
        public bool IsAlive { get; set; } = false;

        /// <summary>
        /// Gets or sets if the <see cref="Particle"/> is dead or alive.
        /// </summary>
        public bool IsDead
        {
            get => !IsAlive;
            set => IsAlive = !value;
        }
        #endregion


        #region Public Methods
        public void Update(TimeSpan timeElapsed)
        {
            var easingBehaviors = new List<EasingBehavior>();

            for (int i = 0; i < _behaviors.Length; i++)
            {
                //TODO: Need to figure something out with this check here. Low perf.
                if (_behaviors[i].GetType().IsSubclassOf(typeof(EasingBehavior)))
                {
                    //TODO: Need to figure something out with this.  This casting is taking too much time.
                    easingBehaviors.Add(_behaviors[i] as EasingBehavior);
                }
            }

            var allExpired = true;

            //If all of the behaviors have ran there course, kill the particle and reset the time elapsed for the behaviors
            for (int i = 0; i < easingBehaviors.Count; i++)
            {
                if (easingBehaviors[i].TimeElapsed < easingBehaviors[i].TotalTime)
                {
                    allExpired = false;
                    break;
                }
            }

            //var nonExpiredBehaviors = easingBehaviors.Where(b => b.TimeElapsed < b.TotalTime).ToArray();

            //TODO: Create this as a field so it does not have to be recreated every time.  It can be cleared when not 
            //needed anymore
            var nonExpiredBehaviors = new List<EasingBehavior>();

            for (int i = 0; i < easingBehaviors.Count; i++)
            {
                var b = easingBehaviors[i];

                if (b.TimeElapsed < b.TotalTime)
                    nonExpiredBehaviors.Add(b);
            }

            if (allExpired)
            {
                IsDead = true;
                return;
            }

            //Apply the behavior values to the particle attributes
            for (int i = 0; i < easingBehaviors.Count; i++)
            {
                easingBehaviors[i].Update(timeElapsed);

                if (easingBehaviors[i].TimeElapsed >= easingBehaviors[i].TotalTime)
                    continue;

                static byte ClampClrValue(float value)
                {
                    return (byte)(value < 0 ? 0 : value);
                }

                switch (easingBehaviors[i].Settings.ApplyToAttribute)
                {
                    case ParticleAttribute.X:
                        Position = new PointF((float)easingBehaviors[i].Value, Position.Y);
                        break;
                    case ParticleAttribute.Y:
                        Position = new PointF(Position.X, (float)easingBehaviors[i].Value);
                        break;
                    case ParticleAttribute.Angle:
                        Angle = (float)easingBehaviors[i].Value;
                        break;
                    case ParticleAttribute.Size:
                        Size = (float)easingBehaviors[i].Value;
                        break;
                    //TODO: Check if the color values below can be set without re-creating and entire new object
                    //One idea is to create a method that sets the values if possible but then uses a ref param
                    //so that way memory allocation is not required.  This is only possible if the Color struct
                    //has the ability to set a single component.  If this is not possible, then creating
                    //a custom color struct that has this ability might be the only way to pull this off.
                    case ParticleAttribute.RedChannel:
                        var redValue = ClampClrValue((float)easingBehaviors[i].Value);

                        TintColor = Color.FromArgb(TintColor.A, redValue, TintColor.G, TintColor.B);
                        break;
                    case ParticleAttribute.GreenChannel:
                        var greenValue = ClampClrValue((float)easingBehaviors[i].Value);

                        TintColor = Color.FromArgb(TintColor.A, TintColor.R, greenValue, TintColor.B);
                        break;
                    case ParticleAttribute.BlueChannel:
                        var blueValue = ClampClrValue((float)easingBehaviors[i].Value);

                        TintColor = Color.FromArgb(TintColor.A, TintColor.R, TintColor.G, blueValue);
                        break;
                    case ParticleAttribute.AlphaChannel:
                        var alphaValue = ClampClrValue((float)easingBehaviors[i].Value);

                        TintColor = Color.FromArgb(alphaValue, TintColor.R, TintColor.G, TintColor.B);
                        break;
                    default:
                        break;
                }
            }
        }


        public void Reset()
        {
            for (int i = 0; i < _behaviors.Length; i++)
                _behaviors[i].Reset();

            Angle = 0;
            TintColor = Color.White;
            LifeTime = 0;
            IsAlive = true;
        }


        public void CreateBehaviors(IRandomizerService randomService)
        {
            var behaviors = new List<IBehavior>();

            //Each particle with the given ID will get every single behavior
            //dictated by the behavior setups
            foreach (var setting in _settings)
            {
                switch (setting.TypeOfBehavior)
                {
                    case BehaviorType.EaseOutBounce:
                        behaviors.Add(new EaseOutBounceBehavior(randomService)
                        {
                            Settings = setting
                        });
                        break;
                    case BehaviorType.EaseIn:
                        behaviors.Add(new EaseInBehavior(randomService)
                        {
                            Settings = setting
                        });
                        break;
                    default:
                        break;
                }
            }

            _behaviors = behaviors.ToArray();
        }
        #endregion
    }
}
