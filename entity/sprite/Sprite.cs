namespace andengine.entity.sprite
{

    using TextureRegion = andengine.opengl.texture.region.TextureRegion;
    using RectangleVertexBuffer = andengine.opengl.vertex.RectangleVertexBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 19:22:38 - 09.03.2010
     */
    public class Sprite : BaseSprite
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

        public Sprite(float pX, float pY, TextureRegion pTextureRegion)
            : base(pX, pY, pTextureRegion.getWidth(), pTextureRegion.getHeight(), pTextureRegion)
        {
        }

        public Sprite(float pX, float pY, float pWidth, float pHeight, TextureRegion pTextureRegion)
            : base(pX, pY, pWidth, pHeight, pTextureRegion)
        {
        }

        public Sprite(float pX, float pY, TextureRegion pTextureRegion, RectangleVertexBuffer pRectangleVertexBuffer)
            : base(pX, pY, pTextureRegion.getWidth(), pTextureRegion.getHeight(), pTextureRegion, pRectangleVertexBuffer)
        {
        }

        public Sprite(float pX, float pY, float pWidth, float pHeight, TextureRegion pTextureRegion, RectangleVertexBuffer pRectangleVertexBuffer)
            : base(pX, pY, pWidth, pHeight, pTextureRegion, pRectangleVertexBuffer)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override TextureRegion getTextureRegion()
        {
            return (TextureRegion)this.mTextureRegion;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}