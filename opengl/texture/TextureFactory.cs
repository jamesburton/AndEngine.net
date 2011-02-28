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

        public static Texture createForTextureSourceSize(TextureRegion pTextureRegion)
        {
            return createForTextureRegionSize(pTextureRegion, TextureOptions.DEFAULT);
        }

        public static Texture createForTextureRegionSize(TextureRegion pTextureRegion, TextureOptions pTextureOptions)
        {
            int loadingScreenWidth = pTextureRegion.getWidth();
            int loadingScreenHeight = pTextureRegion.getHeight();
            return new Texture(MathUtils.nextPowerOfTwo(loadingScreenWidth), MathUtils.nextPowerOfTwo(loadingScreenHeight), pTextureOptions);
        }

        public static Texture createForTextureSourceSize(ITextureSource pTextureSource)
        {
            return createForTextureSourceSize(pTextureSource, TextureOptions.DEFAULT);
        }

        public static Texture createForTextureSourceSize(ITextureSource pTextureSource, TextureOptions pTextureOptions)
        {
            int loadingScreenWidth = pTextureSource.getWidth();
            int loadingScreenHeight = pTextureSource.getHeight();
            return new Texture(MathUtils.nextPowerOfTwo(loadingScreenWidth), MathUtils.nextPowerOfTwo(loadingScreenHeight), pTextureOptions);
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