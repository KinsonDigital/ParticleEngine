using KDParticleEngine;
using KDParticleEngine.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAColor = Microsoft.Xna.Framework.Color;
using NETPointF = System.Drawing.PointF;
using System;
using KDParticleEngine.Behaviors;
using System.Collections.ObjectModel;

/*Easing Function Resources
 * 1. http://theinstructionlimit.com/flash-style-tweeneasing-functions-in-c
 * 2. http://kodhus.com/easings/
 * 3. https://joshondesign.com/2013/03/01/improvedEasingEquations
 * 4. https://onedrive.live.com/?authkey=%21AEzQODHVxYRbtp0&cid=171ED2CFBE9647CB&id=171ED2CFBE9647CB%21923534&parId=171ED2CFBE9647CB%21923533&o=OneUp
 * 5. https://doc.magnum.graphics/magnum/namespaceMagnum_1_1Animation_1_1Easing.html#a1ab2d81d7e5c4f3361d155787b5f8bdb
 */


namespace ParticleSandbox
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private SpriteFont gameFont;
        private readonly TrueRandomizerService randomService;
        private readonly ParticleEngine engine;
        private readonly ITextureLoader<IParticleTexture> textureLoader;
        private readonly FrameCounter frameCounter = new FrameCounter();


        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            this.randomService = new TrueRandomizerService();
            this.textureLoader = new TextureLoader(Content);
            this.engine = new ParticleEngine(this.textureLoader, this.randomService);

            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

            this.graphics.PreparingDeviceSettings += (sender, e) =>
            {
                e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            };
        }


        protected override void Initialize()
        {
            this.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.background = Content.Load<Texture2D>("dark-background");

            this.gameFont = Content.Load<SpriteFont>("GameFont");

            var spawnLocation = new NETPointF(Window.ClientBounds.Width / 2, 50);

            var settings = new BehaviorSettings[]
            {
                new EasingRandomBehaviorSettings()//X Position to right setup
                {
                    EasingFunctionType = EasingFunction.EaseOutBounce,
                    ApplyToAttribute = ParticleAttribute.X,
                    StartMin = Window.ClientBounds.Width / 2,
                    StartMax = Window.ClientBounds.Width / 2,
                    ChangeMin = -400,
                    ChangeMax = 400,
                    TotalTimeMin = 3000,
                    TotalTimeMax = 4500
                },
                new EasingRandomBehaviorSettings()//Y Position to left setup
                {
                    EasingFunctionType = EasingFunction.EaseIn,
                    ApplyToAttribute = ParticleAttribute.Y,
                    StartMin = Window.ClientBounds.Height / 2,
                    StartMax = Window.ClientBounds.Height / 2,
                    ChangeMin = -400,
                    ChangeMax = 400,
                    TotalTimeMin = 2000,
                    TotalTimeMax = 2000
                },
                new EasingRandomBehaviorSettings()//Angle setup
                {
                    ApplyToAttribute = ParticleAttribute.Angle,
                    StartMin = 0,
                    StartMax = 180,
                    ChangeMin = 90,
                    ChangeMax = 270,
                    TotalTimeMin = 1000,
                    TotalTimeMax = 10000
                },
                //new RandomChoiceBehaviorSettings()//Color choice
                //{
                //    ApplyToAttribute = ParticleAttribute.Color,
                //    Data = new ReadOnlyCollection<string>(new[] { "clr:255,255,0,0", "clr:255,0,255,0", "clr:255,0,0,255" }),
                //    LifeTime = 6000
                //},
                new ColorTransitionBehaviorSettings()
                {
                    EasingFunctionType = EasingFunction.EaseIn,
                    LifeTime = 3000,
                    StartColor = new ParticleColor(255, 0, 168, 235),
                    StopColor = new ParticleColor(255, 255, 106, 0),//new ParticleColor(0, 125, 150, 200)
                },
                new EasingRandomBehaviorSettings()//Alpha channel setup
                {
                    ApplyToAttribute = ParticleAttribute.AlphaColorComponent,
                    StartMin = 255,
                    StartMax = 255,
                    ChangeMin = -255,
                    ChangeMax = -255,
                    TotalTimeMin = 3000,
                    TotalTimeMax = 3000
                },
                //new EasingRandomBehaviorSettings()//Red channel setup
                //{
                //    ApplyToAttribute = ParticleAttribute.RedColorComponent,
                //    StartMin = 255,
                //    StartMax = 255,
                //    ChangeMin = -255,
                //    ChangeMax = -255,
                //    TotalTimeMin = 5000,
                //    TotalTimeMax = 5000
                //},
                //new EasingRandomBehaviorSettings()//Green channel setup
                //{
                //    ApplyToAttribute = ParticleAttribute.GreenColorComponent,
                //    StartMin = 0,
                //    StartMax = 0,
                //    ChangeMin = 0,
                //    ChangeMax = 0,
                //    TotalTimeMin = 5,
                //    TotalTimeMax = 5
                //},
                //new EasingRandomBehaviorSettings()//Blue channel setup
                //{
                //    ApplyToAttribute = ParticleAttribute.BlueColorComponent,
                //    StartMin = 0,
                //    StartMax = 0,
                //    ChangeMin = 0,
                //    ChangeMax = 0,
                //    TotalTimeMin = 5,
                //    TotalTimeMax = 5
                //},
                //new EasingRandomBehaviorSettings()//Size setup
                //{
                //    ApplyToAttribute = ParticleAttribute.Size,
                //    StartMin = 0.5f,
                //    StartMax = 0.5f,
                //    ChangeMin = 0.5f,
                //    ChangeMax = 0.5f,
                //    TotalTimeMin = 1000,
                //    TotalTimeMax = 4000
                //}
                new EasingRandomBehaviorSettings()//Size setup
                {
                    ApplyToAttribute = ParticleAttribute.Size,
                    StartMin = 0.25f,
                    StartMax = 0.5f,
                    ChangeMin = 0.25f,
                    ChangeMax = 0.25f,
                    TotalTimeMin = 3000,
                    TotalTimeMax = 3000
                }
            };

            var effectA = new ParticleEffect("Shape-B", settings)
            {
                UseColorsFromList = false,//THIS WILL LIKELY BE REMOVED
                TintColors = new ReadOnlyCollection<ParticleColor>(new ParticleColor[]//THIS WILL LIKELY BE REMOVED
                {
                    new ParticleColor(255, 255, 0, 0),
                    new ParticleColor(255, 0, 255, 0)
                }),
                SpawnLocation = spawnLocation,
                SpawnRateMin = -1,
                SpawnRateMax = -1,
                TotalParticlesAliveAtOnce = 2000,
            };

            IBehaviorFactory factory = new BehaviorFactory();

            this.engine.CreatePool(effectA, factory);

            this.engine.LoadTextures();
        }


        protected override void Update(GameTime gameTime)
        {
            this.engine.Update(gameTime.ElapsedGameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            this.frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            GraphicsDevice.Clear(XNAColor.CornflowerBlue);

            this.spriteBatch.Begin();

            //Draw the background
            var backgroundDestRect = new Rectangle(0, 0, 800, 480);
            this.spriteBatch.Draw(this.background, backgroundDestRect, XNAColor.White);

            //Render the FPS
            this.spriteBatch.DrawString(this.gameFont, $"FPS: {this.frameCounter.AverageFramesPerSecond}", new Vector2(5, 5), XNAColor.Black);

            foreach (var pool in this.engine.ParticlePools)
            {
                foreach (var particle in pool.Particles)
                {
                    if (particle.IsAlive)
                    {
                        var particleDestRect = new Rectangle((int)particle.Position.X, (int)particle.Position.Y, (int)(pool.PoolTexture.Width * particle.Size), (int)(pool.PoolTexture.Height * particle.Size));

                        var monoTexture = (pool.PoolTexture as Texture).MonoTexture;

                        // Setup transparency for the sprite
                        var preMultipliedColor = Color.FromNonPremultiplied(particle.TintColor.R, particle.TintColor.G, particle.TintColor.B, particle.TintColor.A);

                        this.spriteBatch.Draw(monoTexture, particleDestRect, pool.PoolTexture.GetSrcRect(), preMultipliedColor, ToRadians(particle.Angle), pool.PoolTexture.GetOriginAsCenter(), SpriteEffects.None, 0f);
                    }
                }
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }


        private float ToRadians(float angle)
        {
            const float PI = 3.14159265359f;

            return angle * PI / 180f;
        }
    }
}
