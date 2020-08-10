// <copyright file="ParticleEffect.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.CompilerServices;
    using KDParticleEngine.Behaviors;

    /// <summary>
    /// Holds the particle setup settings data for the <see cref="ParticleEngine"/> to consume.
    /// </summary>
    public class ParticleEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEffect"/> class.
        /// </summary>
        public ParticleEffect()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEffect"/> class.
        /// </summary>
        /// <param name="particleTextureName">The name of the texture used in the particle effect.</param>
        /// <param name="settings">The settings used to setup the particle effect.</param>
        public ParticleEffect(string particleTextureName, BehaviorSettings[] settings)
        {
            ParticleTextureName = particleTextureName;
            BehaviorSettings = settings is null
                ? new ReadOnlyCollection<BehaviorSettings>(Array.Empty<BehaviorSettings>())
                : new ReadOnlyCollection<BehaviorSettings>(settings);
        }

        /// <summary>
        /// Gets the name of the particle texture used in the particle effect.
        /// </summary>
        public string ParticleTextureName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets or sets the particle attribute to apply the behavior to.
        /// </summary>
        public ParticleAttribute ApplyBehaviorTo { get; set; }

        /// <summary>
        /// Gets or sets the location on the screen of where to spawn the <see cref="Particle"/>s.
        /// </summary>
        public PointF SpawnLocation { get; set; }

        /// <summary>
        /// Gets or sets the list of colors that the <see cref="ParticleEngine"/> will
        /// randomly choose from when spawning a new <see cref="Particle"/>.
        /// Only used if the <see cref="UseColorsFromList"/> is set to true.
        /// </summary>
        public ReadOnlyCollection<ParticleColor> TintColors { get; set; } = new ReadOnlyCollection<ParticleColor>(Array.Empty<ParticleColor>());

        /// <summary>
        /// Gets or sets the total number of particles that can be alive at once.
        /// </summary>
        public int TotalParticlesAliveAtOnce { get; set; } = 1;

        /// <summary>
        /// Gets or sets the minimum spawn rate of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public int SpawnRateMin { get; set; } = 250;

        /// <summary>
        /// Gets or sets the maximum spawn rate of the range that a <see cref="Particle"/> will be randomly set to.
        /// </summary>
        public int SpawnRateMax { get; set; } = 1000;

        /// <summary>
        /// Gets or sets a value indicating whether the colors will be randomly chosen from a list.
        /// </summary>
        public bool UseColorsFromList { get; set; }

        /// <summary>
        /// Gets the list of behavior settings that describe how the particle effect is setup.
        /// </summary>
        public ReadOnlyCollection<BehaviorSettings> BehaviorSettings { get; } = new ReadOnlyCollection<BehaviorSettings>(Array.Empty<BehaviorSettings>());

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is ParticleEffect effect))
                return false;

            var colorsAreSame = true;

            if (TintColors.Count == effect.TintColors.Count)
            {
                for (var i = 0; i < TintColors.Count; i++)
                {
                    if (TintColors[i] != effect.TintColors[i])
                    {
                        colorsAreSame = false;
                        break;
                    }
                }
            }
            else
            {
                colorsAreSame = false;
            }

            return ParticleTextureName == effect.ParticleTextureName &&
                ApplyBehaviorTo == effect.ApplyBehaviorTo &&
                SpawnLocation == effect.SpawnLocation &&
                colorsAreSame &&
                TotalParticlesAliveAtOnce == effect.TotalParticlesAliveAtOnce &&
                SpawnRateMin == effect.SpawnRateMin &&
                SpawnRateMax == effect.SpawnRateMax &&
                UseColorsFromList == effect.UseColorsFromList &&
                BehaviorSettings.ItemsAreEqual(effect.BehaviorSettings);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            var hash = default(HashCode);

            hash.Add(ParticleTextureName);
            hash.Add(ApplyBehaviorTo);
            hash.Add(SpawnLocation);

            var colorHash = 0;

            foreach (var clr in TintColors)
            {
                colorHash += clr.GetHashCode();
            }

            hash.Add(colorHash);
            hash.Add(TotalParticlesAliveAtOnce);
            hash.Add(SpawnRateMin);
            hash.Add(SpawnRateMax);
            hash.Add(UseColorsFromList);
            hash.Add(BehaviorSettings);

            return hash.ToHashCode();
        }
    }
}
