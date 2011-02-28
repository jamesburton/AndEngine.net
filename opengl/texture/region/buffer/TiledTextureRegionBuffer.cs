namespace andengine.opengl.texture.region.buffer
{

    using TiledTextureRegion = andengine.opengl.texture.region.TiledTextureRegion;

    /**
     * @author Nicolas Gramlich
     * @since 19:01:11 - 09.03.2010
     */
    public class TiledTextureRegionBuffer : BaseTextureRegionBuffer
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

        public TiledTextureRegionBuffer(TiledTextureRegion pTextureRegion, int pDrawType)
            : base(pTextureRegion, pDrawType)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public TiledTextureRegion getTextureRegion()
        {
            return (TiledTextureRegion)base.getTextureRegion();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override float getX1()
        {
            TiledTextureRegion textureRegion = this.getTextureRegion();
            return textureRegion.getTexturePositionOfCurrentTileX() / textureRegion.getTexture().getWidth();
        }

        protected override float getX2()
        {
            TiledTextureRegion textureRegion = this.getTextureRegion();
            return (textureRegion.getTexturePositionOfCurrentTileX() + textureRegion.getTileWidth()) / textureRegion.getTexture().getWidth();
        }

        protected override float getY1()
        {
            TiledTextureRegion textureRegion = this.getTextureRegion();
            return textureRegion.getTexturePositionOfCurrentTileY() / textureRegion.getTexture().getHeight();
        }

        protected override float getY2()
        {
            TiledTextureRegion textureRegion = this.getTextureRegion();
            return (textureRegion.getTexturePositionOfCurrentTileY() + textureRegion.getTileHeight()) / textureRegion.getTexture().getHeight();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}