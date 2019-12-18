using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine
{
    public interface ITextureLoader<T> where T : class
    {
        T LoadTexture(string textureName);
    }
}
