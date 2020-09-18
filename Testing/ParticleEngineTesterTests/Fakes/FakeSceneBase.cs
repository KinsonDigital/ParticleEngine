// <copyright file="FakeSceneBase.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Fakes
{
    using ParticleEngineTester;
    using ParticleEngineTester.Scenes;

    public class FakeSceneBase : SceneBase
    {
        public FakeSceneBase(IRenderer renderer, IContentLoader contentLoader)
            : base(renderer, contentLoader)
        {
        }
    }
}
