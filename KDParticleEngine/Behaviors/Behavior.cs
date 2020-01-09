using KDParticleEngine.Functions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KDParticleEngine.Behaviors
{
    public abstract class Behavior : IBehavior
    {
        public Behavior(Dictionary<int, IFunction> functions)
        {
            ValueFunctions = functions;
        }


        public Dictionary<int, IFunction> ValueFunctions { get; }

        public IApplyFunction ApplyFunction { get; }


        public void Update(Particle particle, TimeSpan timeElapsed)
        {
            var runSequences = ValueFunctions.Keys.ToArray();

            Array.Sort(runSequences);


            foreach (var seqNum in runSequences)
            {
                var func = ValueFunctions.FirstOrDefault(f => f.Key == seqNum).Value;

                
            }
        }


        public void ProcessInputs(Particle particle, IFunction function)
        {
            foreach (var input in function.Inputs)
            {
                switch (input.Type)
                {
                    case InputType.X:
                        function.SetIntput("", particle.Position.X);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
