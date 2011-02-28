namespace andengine.opengl.texture.region.buffer
{

    using TextureRegion = andengine.opengl.texture.region.TextureRegion;

    public class TextureRegionBuffer : BaseTextureRegionBuffer
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

        public TextureRegionBuffer(TextureRegion pTextureRegion, int pDrawType)
            : base(pTextureRegion, pDrawType)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public override TextureRegion getTextureRegion()
        {
            return (TextureRegion)base.getTextureRegion();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override float getX1()
        {
            TextureRegion textureRegion = this.getTextureRegion();
            return (float)textureRegion.getTexturePositionX() / textureRegion.getTexture().getWidth();
        }

        protected override float getX2()
        {
            TextureRegion textureRegion = this.getTextureRegion();
            return (float)(textureRegion.getTexturePositionX() + textureRegion.getWidth()) / textureRegion.getTexture().getWidth();
        }

        protected override float getY1()
        {
            TextureRegion textureRegion = this.getTextureRegion();
            return (float)textureRegion.getTexturePositionY() / textureRegion.getTexture().getHeight();
        }

        protected override float getY2()
        {
            TextureRegion textureRegion = this.getTextureRegion();
            return (float)(textureRegion.getTexturePositionY() + textureRegion.getHeight()) / textureRegion.getTexture().getHeight();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }

}