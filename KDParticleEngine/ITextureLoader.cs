namespace KDParticleEngine
{
    public interface ITextureLoader<T> where T : class
    {
        T LoadTexture(string textureName);
    }
}
