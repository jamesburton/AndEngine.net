namespace andengine.opengl.texture
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    /**
     * @author Nicolas Gramlich
     * @since 13:00:09 - 05.04.2010
     */
    public class TextureOptions
    {
        // ===========================================================
        // Constants
        // ===========================================================

        public static readonly TextureOptions NEAREST = new TextureOptions(GL10Consts.GlNearest, GL10Consts.GlNearest, GL10Consts.GlClampToEdge, GL10Consts.GlClampToEdge, GL10Consts.GlModulate, false);
        public static readonly TextureOptions BILINEAR = new TextureOptions(GL10Consts.GlLinear, GL10Consts.GlLinear, GL10Consts.GlClampToEdge, GL10Consts.GlClampToEdge, GL10Consts.GlModulate, false);
        public static readonly TextureOptions REPEATING = new TextureOptions(GL10Consts.GlNearest, GL10Consts.GlNearest, GL10Consts.GlRepeat, GL10Consts.GlRepeat, GL10Consts.GlModulate, false);
        public static readonly TextureOptions REPEATING_BILINEAR = new TextureOptions(GL10Consts.GlLinear, GL10Consts.GlLinear, GL10Consts.GlRepeat, GL10Consts.GlRepeat, GL10Consts.GlModulate, false);

        public static readonly TextureOptions NEAREST_PREMULTIPLYALPHA = new TextureOptions(GL10Consts.GlNearest, GL10Consts.GlNearest, GL10Consts.GlClampToEdge, GL10Consts.GlClampToEdge, GL10Consts.GlModulate, true);
        public static readonly TextureOptions BILINEAR_PREMULTIPLYALPHA = new TextureOptions(GL10Consts.GlLinear, GL10Consts.GlLinear, GL10Consts.GlClampToEdge, GL10Consts.GlClampToEdge, GL10Consts.GlModulate, true);
        public static readonly TextureOptions REPEATING_PREMULTIPLYALPHA = new TextureOptions(GL10Consts.GlNearest, GL10Consts.GlNearest, GL10Consts.GlRepeat, GL10Consts.GlRepeat, GL10Consts.GlModulate, true);
        public static readonly TextureOptions REPEATING_BILINEAR_PREMULTIPLYALPHA = new TextureOptions(GL10Consts.GlLinear, GL10Consts.GlLinear, GL10Consts.GlRepeat, GL10Consts.GlRepeat, GL10Consts.GlModulate, true);

        public static readonly TextureOptions DEFAULT = NEAREST_PREMULTIPLYALPHA;

        // ===========================================================
        // Fields
        // ===========================================================

        public /*final*/ int mMagFilter;
        public /*final*/ int mMinFilter;
        public /*final*/ float mWrapT;
        public /*final*/ float mWrapS;
        public /*final*/ int mTextureEnvironment;
        public /*final*/ bool mPreMultipyAlpha;

        // ===========================================================
        // Constructors
        // ===========================================================

        public TextureOptions(int pMinFilter, int pMagFilter, int pWrapT, int pWrapS, int pTextureEnvironment, bool pPreMultiplyAlpha)
        {
            this.mMinFilter = pMinFilter;
            this.mMagFilter = pMagFilter;
            this.mWrapT = pWrapT;
            this.mWrapS = pWrapS;
            this.mTextureEnvironment = pTextureEnvironment;
            this.mPreMultipyAlpha = pPreMultiplyAlpha;
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