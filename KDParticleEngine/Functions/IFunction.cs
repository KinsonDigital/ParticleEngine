using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Functions
{
    public interface IFunction
    {
        bool HasExecuted { get; set; }

        public List<Input> Inputs { get; }

        
        void SetIntput(string name, float value);


        float Run();
    }
}
