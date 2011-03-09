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

        public TiledTextureRegion GetTextureRegion()
        {
            return (TiledTextureRegion)base.GetTextureRegion();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override float GetX1()
        {
            TiledTextureRegion textureRegion = this.GetTextureRegion();
            return textureRegion.GetTexturePositionOfCurrentTileX() / textureRegion.GetTexture().GetWidth();
        }

        protected override float GetX2()
        {
            TiledTextureRegion textureRegion = this.GetTextureRegion();
            return (textureRegion.GetTexturePositionOfCurrentTileX() + textureRegion.GetTileWidth()) / textureRegion.GetTexture().GetWidth();
        }

        protected override float GetY1()
        {
            TiledTextureRegion textureRegion = this.GetTextureRegion();
            return textureRegion.GetTexturePositionOfCurrentTileY() / textureRegion.GetTexture().GetHeight();
        }

        protected override float GetY2()
        {
            TiledTextureRegion textureRegion = this.GetTextureRegion();
            return (textureRegion.GetTexturePositionOfCurrentTileY() + textureRegion.GetTileHeight()) / textureRegion.GetTexture().GetHeight();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}