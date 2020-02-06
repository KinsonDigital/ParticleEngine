namespace KDParticleEngine
{
    /// <summary>
    /// Loads textures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITextureLoader<T> where T : class
    {
        #region Methods
        /// <summary>
        /// Loads and returns a texture with the given <paramref name="textureName"/>.
        /// </summary>
        /// <param name="textureName">The name of the texture to load.</param>
        /// <returns></returns>
        T LoadTexture(string textureName);
        #endregion
    }
}
