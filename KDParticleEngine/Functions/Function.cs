using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDParticleEngine.Functions
{
    public abstract class Function : IFunction
    {
        public Function() => InitInputs();


        public bool HasExecuted { get; set; }

        public List<Input> Inputs { get; }

        public float Result { get; set; }


        public virtual float Run()
        {
            HasExecuted = true;

            return Result;
        }


        public void SetIntput(string name, float value)
        {
            var foundInput = Inputs.FirstOrDefault(i => i.Name == name);

            //TODO: Check if this will set the value in the list due to references or if
            //the item will have to be replaced by the found item
            foundInput.Value = value;
        }


        protected void AddInput(string name, InputType type,  float value)
        {
            Inputs.Add(new Input(name, type, value));
        }


        protected float GetInputValue(string name)
        {
            return Inputs.FirstOrDefault(i => i.Name == name).Value;
        }


        protected abstract void InitInputs();


        public void Reset()
        {
            HasExecuted = false;
            Result = 0f;
        }
    }
}
