// <copyright file="MouseInput.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using Microsoft.Xna.Framework.Input;

    public class MouseInput : IMouse
    {
        public MouseState GetState() => Mouse.GetState();
    }
}
