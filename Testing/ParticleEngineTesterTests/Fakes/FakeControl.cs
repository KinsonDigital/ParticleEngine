// <copyright file="FakeControl.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Fakes
{
    using ParticleEngineTester;
    using ParticleEngineTester.UI;

    public class FakeControl : Control
    {
        public FakeControl(IMouse mouse)
            : base(mouse)
        {
            GetWidth = () => 100;
            GetHeight = () => 100;
        }
    }
}
