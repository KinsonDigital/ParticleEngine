// <copyright file="FakeSceneBase.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Fakes
{
    using ParticleEngineTester;
    using ParticleEngineTester.Scenes;

    /// <summary>
    /// Provides a fake object to test the <see cref="SceneBase"/> class.
    /// </summary>
    public class FakeSceneBase : SceneBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeSceneBase"/> class.
        /// </summary>
        /// <param name="renderer">The mocked renderer used to render the scene.</param>
        /// <param name="contentLoader">The mocked content loader to load content..</param>
        /// <param name="name">The name of the scene.</param>
        public FakeSceneBase(IRenderer renderer, IContentLoader contentLoader, string name)
            : base(renderer, contentLoader, name)
        {
        }
    }
}
