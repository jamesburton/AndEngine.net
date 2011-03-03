namespace andengine.opengl.texture
{

    using TextureRegion = andengine.opengl.texture.region.TextureRegion;
    using ITextureSource = andengine.opengl.texture.source.ITextureSource;
    using MathUtils = andengine.util.MathUtils;

    /**
     * @author Nicolas Gramlich
     * @since 09:38:51 - 03.05.2010
     */
    public class TextureFactory
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public static Texture CreateForTextureSourceSize(TextureRegion pTextureRegion)
        {
            return CreateForTextureRegionSize(pTextureRegion, TextureOptions.DEFAULT);
        }

        public static Texture CreateForTextureRegionSize(TextureRegion pTextureRegion, TextureOptions pTextureOptions)
        {
            int loadingScreenWidth = pTextureRegion.getWidth();
            int loadingScreenHeight = pTextureRegion.getHeight();
            return new Texture(MathUtils.NextPowerOfTwo(loadingScreenWidth), MathUtils.NextPowerOfTwo(loadingScreenHeight), pTextureOptions);
        }

        public static Texture CreateForTextureSourceSize(ITextureSource pTextureSource)
        {
            return CreateForTextureSourceSize(pTextureSource, TextureOptions.DEFAULT);
        }

        public static Texture CreateForTextureSourceSize(ITextureSource pTextureSource, TextureOptions pTextureOptions)
        {
            int loadingScreenWidth = pTextureSource.getWidth();
            int loadingScreenHeight = pTextureSource.getHeight();
            return new Texture(MathUtils.NextPowerOfTwo(loadingScreenWidth), MathUtils.NextPowerOfTwo(loadingScreenHeight), pTextureOptions);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}