using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Functions
{
    public struct Input
    {
        public Input(string name, InputType type, float value)
        {
            Name = name;
            Type = type;
            Value = value;
        }


        public string Name { get; set; }

        public InputType Type { get; set; }

        public float Value { get; set; }
    }
}
