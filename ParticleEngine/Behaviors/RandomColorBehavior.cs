// <copyright file="RandomColorBehavior.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System;
using ParticleEngine.Services;

namespace ParticleEngine.Behaviors
{
    /// <summary>
    /// Randomly chooses colors from a list of colors that will be applied to a particle.
    /// Used to randomly set the tint color of a particle for its entire lifetime.
    /// </summary>
    public class RandomColorBehavior : Behavior
    {
        private RandomChoiceBehaviorSettings _settings;
        private readonly IRandomizerService _randomizer;
        private bool _isColorChosen;

        /// <summary>
        /// Creates a new instance of <see cref="RandomColorBehavior"/>.
        /// </summary>
        /// <param name="settings">The behavior settings.</param>
        /// <param name="randomizer">The randomizer used to randomly choose a color from a list of colors.</param>
        public RandomColorBehavior(RandomChoiceBehaviorSettings settings, IRandomizerService randomizer) : base(settings)
        {
            _settings = settings;
            _randomizer = randomizer;
        }

        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);

            //If the amount of time has passed, disable the behavor
            Enabled = ElapsedTime < _settings.LifeTime;

            if (_isColorChosen)
                return;

            //Randomly choose a color and set the value to a floating point number that represents that color
            var randomIndex = _randomizer.GetValue(0, _settings.Data is null ? 0 : _settings.Data.Length - 1);

            Value = _settings.Data is null ? "clr:255,255,255,255" : _settings.Data[randomIndex];

            _isColorChosen = true;
        }

        /// <summary>
        /// Resets the behavior.
        /// </summary>
        public override void Reset()
        {
            _isColorChosen = false;
            base.Reset();
        }
    }
}
