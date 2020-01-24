using KDParticleEngine;
using KDParticleEngine.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNAColor = Microsoft.Xna.Framework.Color;
using NETPointF = System.Drawing.PointF;
using Microsoft.Xna.Framework.Input;
using System;
using MathExpressionEngine;
using MathExpressionEngine.Expressions;
using KDParticleEngine.Behaviors;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

/*Easing Functino Resources
 * 1. http://theinstructionlimit.com/flash-style-tweeneasing-functions-in-c
 * 2. http://kodhus.com/easings/
 * 3. https://joshondesign.com/2013/03/01/improvedEasingEquations
 * 4. https://onedrive.live.com/?authkey=%21AEzQODHVxYRbtp0&cid=171ED2CFBE9647CB&id=171ED2CFBE9647CB%21923534&parId=171ED2CFBE9647CB%21923533&o=OneUp
 * 5. https://doc.magnum.graphics/magnum/namespaceMagnum_1_1Animation_1_1Easing.html#a1ab2d81d7e5c4f3361d155787b5f8bdb
 *  
 *  
 */
namespace ParticleSandbox
{
    public class Game1 : Game
    {
        //DEBUGGING - PERFORMANCE CHECKING
        private Stopwatch _timer = new Stopwatch();
        private Queue<double> _otherTimings = new Queue<double>();

        private double _totalMinutesPassed;
        //////////////////////////////////

        private Expression _moveRightExpression;
        private Expression _redColorExpression;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _gameFont;
        private TrueRandomizerService _randomService;
        private ParticleEngine<Texture2D> _engine;
        private ITextureLoader<Texture2D> _textureLoader;
        private Texture2D _easeTexture;
        private KeyboardState _currentState;
        private KeyboardState _prevState;
        private FrameCounter _frameCounter = new FrameCounter();


        public Game1()
        {
            _moveRightExpression = Compiler.Compile("$c*($t/$d)*($t/$d)+$b");
            _redColorExpression = Compiler.Compile("$c*($t/$d)*($t/$d)+$b");

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            _randomService = new TrueRandomizerService();
            _textureLoader = new Texture2DLoader(Content);
            _engine = new ParticleEngine<Texture2D>(_textureLoader, _randomService);

            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);


            _graphics.PreparingDeviceSettings += (sender, e) =>
            {
                e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            };
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _easeTexture = Content.Load<Texture2D>("Shape-A");
            _gameFont = Content.Load<SpriteFont>("GameFont");

            //TODO: Figure out how to deal with particle textures
            //ParticleTexture = Content.Load<Texture2D>("Shape-A"),

            var spawnLocation = new NETPointF(Window.ClientBounds.Width / 2, 50);

            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()//X Position to right setup
                {
                    TypeOfBehavior = BehaviorType.EaseOutBounce,
                    ApplyToAttribute = ParticleAttribute.X,
                    StartMin = Window.ClientBounds.Width / 2,
                    StartMax = Window.ClientBounds.Width / 2,
                    ChangeMin = -200,
                    ChangeMax = 200,
                    TotalTimeMin = 2,
                    TotalTimeMax = 6
                },
                new BehaviorSetting()//Y Position to left setup
                {
                    TypeOfBehavior = BehaviorType.EaseOutBounce,
                    ApplyToAttribute = ParticleAttribute.Y,
                    StartMin = Window.ClientBounds.Height / 2,
                    StartMax = Window.ClientBounds.Height / 2,
                    ChangeMin = -200,
                    ChangeMax = 200,
                    TotalTimeMin = 2,
                    TotalTimeMax = 6
                },
                new BehaviorSetting()//Angle setup
                {
                    TypeOfBehavior = BehaviorType.EaseOutBounce,
                    ApplyToAttribute = ParticleAttribute.Angle,
                    StartMin = 0,
                    StartMax = 180,
                    ChangeMin = 90,
                    ChangeMax = 270,
                    TotalTimeMin = 1,
                    TotalTimeMax = 5
                },
                new BehaviorSetting()//Red channel setup
                {
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ApplyToAttribute = ParticleAttribute.RedChannel,
                    StartMin = 255,
                    StartMax = 255,
                    ChangeMin = 0,
                    ChangeMax = 0,
                    TotalTimeMin = 5,
                    TotalTimeMax = 5
                },
                new BehaviorSetting()//Green channel setup
                {
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ApplyToAttribute = ParticleAttribute.GreenChannel,
                    StartMin = 255,
                    StartMax = 255,
                    ChangeMin = -255,
                    ChangeMax = -255,
                    TotalTimeMin = 5,
                    TotalTimeMax = 5
                },
                new BehaviorSetting()//Blue channel setup
                {
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ApplyToAttribute = ParticleAttribute.BlueChannel,
                    StartMin = 255,
                    StartMax = 255,
                    ChangeMin = -255,
                    ChangeMax = -255,
                    TotalTimeMin = 5,
                    TotalTimeMax = 5
                },
                //new BehaviorSetting()//Alpha channel setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseIn,
                //    ApplyToAttribute = ParticleAttribute.AlphaChannel,
                //    StartMin = 255,
                //    StartMax = 255,
                //    ChangeMin = -255,
                //    ChangeMax = -255,
                //    TotalTimeMin = 1,
                //    TotalTimeMax = 4
                //}
                new BehaviorSetting()//Size setup
                {
                    TypeOfBehavior = BehaviorType.EaseOutBounce,
                    ApplyToAttribute = ParticleAttribute.Size,
                    StartMin = 0.5f,
                    StartMax = 0.5f,
                    ChangeMin = 0.5f,
                    ChangeMax = 0.5f,
                    TotalTimeMin = 1,
                    TotalTimeMax = 4
                }
            };

            var effectA = new ParticleEffect("Shape-A", settings, _randomService)
            {
                SpawnLocation = spawnLocation,
                SpawnRateMin = 125,
                SpawnRateMax = 500,
                TotalParticlesAliveAtOnce = 2000,
                TypeOfBehavior = BehaviorType.EaseOutBounce,
                ApplyBehaviorTo = ParticleAttribute.Y
            };


            _engine.AddEffect(effectA);

            _engine.LoadTextures();
        }


        protected override void Update(GameTime gameTime)
        {
            _totalMinutesPassed += gameTime.ElapsedGameTime.TotalMinutes;

            if (_totalMinutesPassed >= 0.83)
            {

            }

            var totalIterations = 100;
            _timer.Start();

            _engine.Update(gameTime.ElapsedGameTime);

            _timer.Stop();
            _otherTimings.Enqueue(_timer.Elapsed.TotalMilliseconds);
            _timer.Reset();

            /*Perf Results(ms):
             * Engine Update:   => 
             */


            if (_otherTimings.Count >= totalIterations)
            {
                _otherTimings.Dequeue();
                var perfResult = _otherTimings.Average();
                var maxValue = _otherTimings.Max();

                if (maxValue < 121)
                {

                }
            }

            _currentState = Keyboard.GetState();


            _prevState = _currentState;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            _frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            GraphicsDevice.Clear(XNAColor.CornflowerBlue);

            //Ease Testing Draw Call
            //_spriteBatch.Begin();
            //_spriteBatch.Draw(_easeTexture, _entityPos, XNAColor.White);
            //_spriteBatch.End();

            //Particle Engine Draw Call

            //var texture = _engine.GetTexture("Shape-A");


            _spriteBatch.Begin();

            //Render the FPS
            _spriteBatch.DrawString(_gameFont, $"FPS: {_frameCounter.AverageFramesPerSecond}", new Vector2(5, 5), XNAColor.Black);

            foreach (var pool in _engine.ParticlePools)
            {
                foreach (var particle in pool.Particles)
                {
                    if (particle.IsAlive)
                    {
                        var textureName = pool.Effect.ParticleTextureName;

                        //var destRect = new Rectangle((int)p.Position.X, (int)p.Position.Y, (int)(texture.Width * p.Size), (int)(texture.Height * p.Size));
                        var destRect = new Rectangle((int)particle.Position.X, (int)particle.Position.Y, (int)(_easeTexture.Width * particle.Size), (int)(_easeTexture.Height * particle.Size));

                        _spriteBatch.Draw(_easeTexture, destRect, _easeTexture.GetSrcRect(), particle.TintColor.ToXNAColor(), ToRadians(particle.Angle), _easeTexture.GetOriginAsCenter(), SpriteEffects.None, 0f);
                    }
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private float ToRadians(float angle)
        {
            const float PI = 3.14159265359f;

            return (angle * PI) / 180f;
        }


        private float EaseInOutQuadCPP(float t)
        {

            return 1 - (2 - t) * t;
        }


        private float EaseInOutQuad(float t, float b, float c, float d)
        {
            t /= d / 2f;

            if (t < 1) return c / 2f * t * t + b;


            return -c / 2 * ((t - 1) * (t - 3f) - 1f) + b;
        }


        private float EaseOut(float t, float b, float c, float d)
        {
            t /= d;

            return -c * t * (t - 2) + b;
        }


        private float EaseInQuad(float t, float b, float c, float d)
        {
            t /= d;

            return c * t * t + b;
        }


        private float EaseOutBounce(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75f))
            {
                return c * (7.5625f * t * t) + b;
            }
            else if (t < (2 / 2.75f))
            {
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            }
            else if (t < (2.5f / 2.75f))
            {
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            }
            else
            {
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
            }
        }
    }
}
