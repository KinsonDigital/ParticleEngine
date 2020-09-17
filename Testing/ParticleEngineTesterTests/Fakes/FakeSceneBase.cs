using System;
using System.Collections.Generic;
using System.Text;
using ParticleEngineTester;
using ParticleEngineTester.Scenes;

namespace ParticleEngineTesterTests.Fakes
{
    public class FakeSceneBase : SceneBase
    {
        public FakeSceneBase(IRenderer renderer, IContentLoader contentLoader)
            : base(renderer, contentLoader)
        {
        }
    }
}
