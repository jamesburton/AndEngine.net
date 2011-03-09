namespace andengine.opengl.texture.region
{

    //import javax.microedition.khronos.opengles.GL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11Consts = Javax.Microedition.Khronos.Opengles.GL11Consts;

    using Texture = andengine.opengl.texture.Texture;
    using BaseTextureRegionBuffer = andengine.opengl.texture.region.buffer.BaseTextureRegionBuffer;
    using TextureRegionBuffer = andengine.opengl.texture.region.buffer.TextureRegionBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 14:29:59 - 08.03.2010
     */
    public class TextureRegion : BaseTextureRegion
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

        public TextureRegion(Texture pTexture, int pTexturePositionX, int pTexturePositionY, int pWidth, int pHeight)
            : base(pTexture, pTexturePositionX, pTexturePositionY, pWidth, pHeight)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public new TextureRegionBuffer GetTextureBuffer()
        {
            return (TextureRegionBuffer)this.mTextureRegionBuffer;
        }

        public TextureRegionBuffer TextureBuffer { get { return GetTextureBuffer(); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ virtual TextureRegion Clone()
        {
            return new TextureRegion(this.mTexture, this.mTexturePositionX, this.mTexturePositionY, this.mWidth, this.mHeight);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        /*
        protected override BaseTextureRegionBuffer OnCreateTextureRegionBuffer()
        {
            return new TextureRegionBuffer(this, GL11Consts.GlStaticDraw);
        }
        */
        protected override BaseTextureRegionBuffer OnCreateTextureRegionBufferCore()
        {
            return new TextureRegionBuffer(this, GL11Consts.GlStaticDraw);
        }
        protected new TextureRegionBuffer OnCreateTextureRegionBuffer() { return (TextureRegionBuffer)OnCreateTextureRegionBufferCore(); }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}