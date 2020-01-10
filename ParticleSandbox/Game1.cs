using KDParticleEngine;
using KDParticleEngine.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Linq;

using XNAColor = Microsoft.Xna.Framework.Color;
using NETPointF = System.Drawing.PointF;
using NETColor = System.Drawing.Color;
using Microsoft.Xna.Framework.Input;
using System;
using MathExpressionEngine;
using MathExpressionEngine.Expressions;

/*Easing Functino Resources
 * 1. http://theinstructionlimit.com/flash-style-tweeneasing-functions-in-c
 * 2. http://kodhus.com/easings/
 * 3. https://joshondesign.com/2013/03/01/improvedEasingEquations
 * 4. https://onedrive.live.com/?authkey=%21AEzQODHVxYRbtp0&cid=171ED2CFBE9647CB&id=171ED2CFBE9647CB%21923534&parId=171ED2CFBE9647CB%21923533&o=OneUp
 * 5. https://doc.magnum.graphics/magnum/namespaceMagnum_1_1Animation_1_1Easing.html#a1ab2d81d7e5c4f3361d155787b5f8bdb
 *  NOTE: Very promising one.  This one is used differently due to the params used.
 *  
 */
namespace ParticleSandbox
{
    public class Game1 : Game
    {
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

            var spawnLocation = new NETPointF(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

            var setupA = new ParticleEffect("Shape-A")
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
                TotalParticlesAliveAtOnce = 1
            };


            var setupB = new ParticleEffect("Shape-B")
            {
                SpawnLocation = new NETPointF(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2),
                VelocityXMin = 100f,
                VelocityXMax = 100,
                VelocityYMin = -100f,
                VelocityYMax = 100,
                AngularVelocityMin = 200,
                AngularVelocityMax = 200,
                SizeMin = 0.25f,
                SizeMax = 0.5f,
                LifeTimeMin = 125,
                LifeTimeMax = 2000,
                SpawnRateMin = 125,
                SpawnRateMax = 125,
                TotalParticlesAliveAtOnce = 40
            };

            _engine.AddSetup(setupA);
            //_engine.AddSetup(setupB);

            _engine.LoadTextures();
        }


        protected override void Update(GameTime gameTime)
        {
            //_engine.Update(gameTime.ElapsedGameTime);

            _currentState = Keyboard.GetState();

            if (_currentState.IsKeyDown(Keys.Space) && _prevState.IsKeyUp(Keys.Space))
                _enableEaseFunction = true;

            _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_entityPos.X < 400)
            {
                _moveRightExpression.UpdateVariable("t", _timeElapsed);
                _moveRightExpression.UpdateVariable("b", 100);//Start
                _moveRightExpression.UpdateVariable("c", 400 - 100);
                _moveRightExpression.UpdateVariable("d", 4);

                _entityPos.X = (float)_moveRightExpression.Eval();
            }

            if (_clrResult <= 255)
            {
                _redColorExpression.UpdateVariable("t", _timeElapsed);
                _redColorExpression.UpdateVariable("b", 0);//Start
                _redColorExpression.UpdateVariable("c", 255);
                _redColorExpression.UpdateVariable("d", 1);

                _clrResult = _redColorExpression.Eval();

                _tintColor.G -= (byte)_clrResult;
                _tintColor.B -= (byte)_clrResult;
            }

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

            _spriteBatch.Draw(_easeTexture, _entityPos, _tintColor);

            //_engine.ParticlePools.ToList().ForEach(pool =>
            //{
            //    pool.Particles.ToList().ForEach(p =>
            //    {
            //        var textureName = pool.Setup.ParticleTextureName;

            //        var texture = _engine.GetTexture(textureName);

            //        _spriteBatch.Draw(texture,
            //                          p.Position.ToVector2(),
            //                          texture.GetSrcRect(),
            //                          p.TintColor.ToXNAColor(),
            //                          ToRadians(p.Angle),
            //                          texture.GetOriginAsCenter(),
            //                          p.Size,
            //                          SpriteEffects.None,
            //                          0f);
            //    });
            //});

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


        private float EaseInQuad(float t, float b, float c, float d)
        {
            t /= d;

            return c * t * t + b;
        }
    }


    public static class Easing
    {
        // Adapted from source : http://www.robertpenner.com/easing/

        public static float Ease(double linearStep, float acceleration, EasingType type)
        {
            float easedStep = acceleration > 0 ? EaseIn(linearStep, type) :
                              acceleration < 0 ? EaseOut(linearStep, type) :
                              (float)linearStep;

            return MathHelper.Lerp(linearStep, easedStep, Math.Abs(acceleration));
        }

        public static float EaseIn(double linearStep, EasingType type)
        {
            switch (type)
            {
                case EasingType.Step: return linearStep < 0.5 ? 0 : 1;
                case EasingType.Linear: return (float)linearStep;
                case EasingType.Sine: return Sine.EaseIn(linearStep);
                case EasingType.Quadratic: return Power.EaseIn(linearStep, 2);
                case EasingType.Cubic: return Power.EaseIn(linearStep, 3);
                case EasingType.Quartic: return Power.EaseIn(linearStep, 4);
                case EasingType.Quintic: return Power.EaseIn(linearStep, 5);
            }
            throw new NotImplementedException();
        }

        public static float EaseOut(double linearStep, EasingType type)
        {
            switch (type)
            {
                case EasingType.Step: return linearStep < 0.5 ? 0 : 1;
                case EasingType.Linear: return (float)linearStep;
                case EasingType.Sine: return Sine.EaseOut(linearStep);
                case EasingType.Quadratic: return Power.EaseOut(linearStep, 2);
                case EasingType.Cubic: return Power.EaseOut(linearStep, 3);
                case EasingType.Quartic: return Power.EaseOut(linearStep, 4);
                case EasingType.Quintic: return Power.EaseOut(linearStep, 5);
            }
            throw new NotImplementedException();
        }

        public static float EaseInOut(double linearStep, EasingType easeInType, EasingType easeOutType)
        {
            return linearStep < 0.5 ? EaseInOut(linearStep, easeInType) : EaseInOut(linearStep, easeOutType);
        }
        public static float EaseInOut(double linearStep, EasingType type)
        {
            switch (type)
            {
                case EasingType.Step: return linearStep < 0.5 ? 0 : 1;
                case EasingType.Linear: return (float)linearStep;
                case EasingType.Sine: return Sine.EaseInOut(linearStep);
                case EasingType.Quadratic: return Power.EaseInOut(linearStep, 2);
                case EasingType.Cubic: return Power.EaseInOut(linearStep, 3);
                case EasingType.Quartic: return Power.EaseInOut(linearStep, 4);
                case EasingType.Quintic: return Power.EaseInOut(linearStep, 5);
            }
            throw new NotImplementedException();
        }

        static class Sine
        {
            public static float EaseIn(double s)
            {
                return (float)Math.Sin(s * MathHelper.HalfPi - MathHelper.HalfPi) + 1;
            }
            public static float EaseOut(double s)
            {
                return (float)Math.Sin(s * MathHelper.HalfPi);
            }
            public static float EaseInOut(double s)
            {
                return (float)(Math.Sin(s * MathHelper.Pi - MathHelper.HalfPi) + 1) / 2;
            }
        }

        static class Power
        {
            public static float EaseIn(double s, int power)
            {
                return (float)Math.Pow(s, power);
            }
            public static float EaseOut(double s, int power)
            {
                var sign = power % 2 == 0 ? -1 : 1;
                return (float)(sign * (Math.Pow(s - 1, power) + sign));
            }
            public static float EaseInOut(double s, int power)
            {
                s *= 2;
                if (s < 1) return EaseIn(s, power) / 2;
                var sign = power % 2 == 0 ? -1 : 1;
                return (float)(sign / 2.0 * (Math.Pow(s - 2, power) + sign * 2));
            }
        }
    }


    public enum EasingType
    {
        Step,
        Linear,
        Sine,
        Quadratic,
        Cubic,
        Quartic,
        Quintic
    }


    public static class MathHelper
    {
        public const float Pi = (float)Math.PI;
        public const float HalfPi = (float)(Math.PI / 2);


        public static float Lerp(double from, double to, double step)
        {
            return (float)((to - from) * step + from);
        }
    }
}
