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
        private List<double> _timings = new List<double>();
        //////////////////////////////////

        private Expression _moveRightExpression;
        private Expression _redColorExpression;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TrueRandomizerService _randomService;
        private ParticleEngine<Texture2D> _engine;
        private ITextureLoader<Texture2D> _textureLoader;
        private Texture2D _easeTexture;
        private Vector2 _entityPos = new Vector2(100, 200);
        private Vector2 _velocity = new Vector2(100, 0);
        private KeyboardState _currentState;
        private KeyboardState _prevState;
        private XNAColor _tintColor;
        private bool _enableEaseFunction;
        private int _duration = 4;
        private float _timeElapsed = 0;
        private float _dest = 400;
        private float _start = 100;
        private double _clrResult;

        public Game1()
        {
            _moveRightExpression = Compiler.Compile("$c*($t/$d)*($t/$d)+$b");
            _redColorExpression = Compiler.Compile("$c*($t/$d)*($t/$d)+$b");

            _tintColor = new XNAColor(255, 255, 255, 255);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            _randomService = new TrueRandomizerService();
            _textureLoader = new Texture2DLoader(Content);
            _engine = new ParticleEngine<Texture2D>(_textureLoader, _randomService);
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _easeTexture = Content.Load<Texture2D>("Shape-B");

            //TODO: Figure out how to deal with particle textures
            //ParticleTexture = Content.Load<Texture2D>("Shape-A"),

            var spawnLocation = new NETPointF(Window.ClientBounds.Width / 2, 50);

            var setups = new BehaviorSetup[]
            {
                //new BehaviorSetup()//X Position to left setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseOutBounce,
                //    ApplyToAttribute = ParticleAttribute.X,
                //    StartMin = Window.ClientBounds.Width / 2,
                //    StartMax = Window.ClientBounds.Width / 2,
                //    ChangeMin = -200,
                //    ChangeMax = 200,
                //    TotalTimeMin = 2,
                //    TotalTimeMax = 2
                //},
                //new BehaviorSetup()//X Position to right setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseOutBounce,
                //    ApplyToAttribute = ParticleAttribute.X,
                //    StartMin = Window.ClientBounds.Width / 2,
                //    StartMax = Window.ClientBounds.Width / 2,
                //    ChangeMin = 200,
                //    ChangeMax = 200,
                //    TotalTimeMin = 3,
                //    TotalTimeMax = 6
                //},
                //new BehaviorSetup()//Y Position setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseOutBounce,
                //    ApplyToAttribute = ParticleAttribute.Y,
                //    StartMin = 50,
                //    StartMax = 50,
                //    ChangeMin = 200,
                //    ChangeMax = 200,
                //    TotalTimeMin = 3,
                //    TotalTimeMax = 6
                //},
                //new BehaviorSetup()//Angle setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseOutBounce,
                //    ApplyToAttribute = ParticleAttribute.Angle,
                //    StartMin = 0,
                //    StartMax = 180,
                //    ChangeMin = 90,
                //    ChangeMax = 270,
                //    TotalTimeMin = 1,
                //    TotalTimeMax = 8
                //},
                //new BehaviorSetup()//Red channel setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseIn,
                //    ApplyToAttribute = ParticleAttribute.RedChannel,
                //    StartMin = 255,
                //    StartMax = 255,
                //    ChangeMin = -255,
                //    ChangeMax = -255,
                //    TotalTimeMin = 1,
                //    TotalTimeMax = 4
                //},
                //new BehaviorSetup()//Green channel setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseIn,
                //    ApplyToAttribute = ParticleAttribute.GreenChannel,
                //    StartMin = 255,
                //    StartMax = 255,
                //    ChangeMin = -255,
                //    ChangeMax = -255,
                //    TotalTimeMin = 1,
                //    TotalTimeMax = 4
                //},
                //new BehaviorSetup()//Blue channel setup
                //{
                //    TypeOfBehavior = BehaviorType.EaseIn,
                //    ApplyToAttribute = ParticleAttribute.BlueChannel,
                //    StartMin = 255,
                //    StartMax = 255,
                //    ChangeMin = -255,
                //    ChangeMax = -255,
                //    TotalTimeMin = 1,
                //    TotalTimeMax = 4
                //},
                //new BehaviorSetup()//Alpha channel setup
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
                new BehaviorSetup()//Size setup
                {
                    TypeOfBehavior = BehaviorType.EaseOutBounce,
                    ApplyToAttribute = ParticleAttribute.Size,
                    StartMin = 1,
                    StartMax = 1,
                    ChangeMin = -1f,
                    ChangeMax = -1f,
                    TotalTimeMin = 1,
                    TotalTimeMax = 4
                }
            };

            var effectA = new ParticleEffect("Shape-A", setups, _randomService)
            {
                SpawnLocation = spawnLocation,
                //This becomes a behavior
                VelocityXMin = 100f,
                VelocityXMax = 100,
                VelocityYMin = 0f,
                VelocityYMax = 0,

                AngularVelocityMin = 50,
                AngularVelocityMax = 200,
                SizeMin = 0.25f,
                SizeMax = 0.5f,
                LifeTimeMin = 5000,
                LifeTimeMax = 5000,
                SpawnRateMin = 125,
                SpawnRateMax = 500,
                TotalParticlesAliveAtOnce = 1,
                TypeOfBehavior = BehaviorType.EaseOutBounce,
                ApplyBehaviorTo = ParticleAttribute.Y
            };


            _engine.AddEffect(effectA);

            _engine.LoadTextures();
        }


        protected override void Update(GameTime gameTime)
        {
            _engine.Update(gameTime.ElapsedGameTime);

            _currentState = Keyboard.GetState();

            if (_currentState.IsKeyDown(Keys.Space) && _prevState.IsKeyUp(Keys.Space))
                _enableEaseFunction = true;

            _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_entityPos.X < 400)
            {
                //_moveRightExpression.UpdateVariable("t", _timeElapsed);
                //_moveRightExpression.UpdateVariable("b", 100);//Start
                //_moveRightExpression.UpdateVariable("c", 400 - 100);
                //_moveRightExpression.UpdateVariable("d", 4);
                //_entityPos.X = (float)_moveRightExpression.Eval();

                //_entityPos.X = EaseOutBounce(_timeElapsed, 100, 300, 4);
            }


            //if (_clrResult <= 255)
            //{
            //    _clrResult = EaseInQuad(_timeElapsed, 0, 255, 4);

            //    _clrResult = _clrResult > 255 ? 255 : _clrResult;

            //    _tintColor.G = (byte)(255 - _clrResult);
            //    _tintColor.B = (byte)(255 - _clrResult);
            //}

            _prevState = _currentState;
            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(XNAColor.CornflowerBlue);

            //Ease Testing Draw Call
            //_spriteBatch.Begin();
            //_spriteBatch.Draw(_easeTexture, _entityPos, XNAColor.White);
            //_spriteBatch.End();

            //Particle Engine Draw Call

            _spriteBatch.Begin();

            _engine.ParticlePools.ToList().ForEach(pool =>
            {
                pool.Particles.ToList().ForEach(p =>
                {
                    if (p.IsAlive)
                    {
                        var textureName = pool.Effect.ParticleTextureName;

                        var texture = _engine.GetTexture(textureName);

                        //DEBUGGING ONLY
                        //var position = p.Position.ToVector2();
                        //var srcRect = texture.GetSrcRect();
                        //var tintClr = p.TintColor.ToXNAColor();
                        //var angle = ToRadians(p.Angle);
                        //var center = texture.GetOriginAsCenter();
                        //var size = p.Size;


                        _spriteBatch.Draw(texture,
                                          p.Position.ToVector2(),
                                          texture.GetSrcRect(),
                                          p.TintColor.ToXNAColor(),
                                          ToRadians(p.Angle),
                                          texture.GetOriginAsCenter(),
                                          p.Size,
                                          SpriteEffects.None,
                                          0f);
                    }
                });
            });

            _timer.Start();
            _spriteBatch.End();
            _timer.Stop();
            _timings.Add(_timer.Elapsed.TotalMilliseconds);
            _timer.Reset();


            var perfResult = _timings.Average();


            /*Perf Results(ms):
             * Using ToList().ForEach()     => 3.46
             * Using standard for loop      => 4.32
             * GetTexture()                 => 2.65
             * Smaller Size                 => 
             */

            if (_timings.Count >= 10000)
            {

            }
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
