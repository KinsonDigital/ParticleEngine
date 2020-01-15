﻿using KDParticleEngine.Behaviors;
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
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            TintColor = color;
            Size = size;
            LifeTime = lifeTime;
        }


        public Particle() { }
        #endregion


        #region Props
        public int ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the position of the <see cref="Particle"/>.
        /// </summary>
        public PointF Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the <see cref="Particle"/>.
        /// </summary>
        public PointF Velocity { get; set; }

        /// <summary>
        /// Gets or sets the angle that the <see cref="Particle"/> is at when first spawned.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// Gets or sets the speed and direction of rotation of the <see cref="Particle"/>.
        /// </summary>
        public float AngularVelocity { get; set; }

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
        /// <summary>
        /// Resets all of the particles properties.
        /// </summary>
        //TODO: Remove this
        public void Reset()
        {
            Position = PointF.Empty;
            Velocity = PointF.Empty;
            Angle = 0;
            AngularVelocity = 0;
            TintColor = Color.White;
            Size = 0;
            LifeTime = 0;
            IsAlive = false;
        }
        #endregion
    }
}
