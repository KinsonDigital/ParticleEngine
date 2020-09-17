using System;
using System.Collections.Generic;
using System.Text;
using ParticleEngineTester;
using ParticleEngineTester.UI;

namespace ParticleEngineTesterTests.Fakes
{
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
