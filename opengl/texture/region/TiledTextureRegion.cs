namespace andengine.opengl.texture.region
{

    //import javax.microedition.khronos.opengles.GL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11Consts = Javax.Microedition.Khronos.Opengles.GL11Consts;

    using Texture = andengine.opengl.texture.Texture;
    using TiledTextureRegionBuffer = andengine.opengl.texture.region.buffer.TiledTextureRegionBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 18:14:42 - 09.03.2010
     */
    public class TiledTextureRegion : BaseTextureRegion
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ int mTileColumns;
        private /* final */ int mTileRows;
        private int mCurrentTileColumn;
        private int mCurrentTileRow;
        private /* final */ int mTileCount;

        // ===========================================================
        // Constructors
        // ===========================================================

        public TiledTextureRegion(Texture pTexture, int pTexturePositionX, int pTexturePositionY, int pWidth, int pHeight, int pTileColumns, int pTileRows)
            : base(pTexture, pTexturePositionX, pTexturePositionY, pWidth, pHeight)
        {
            this.mTileColumns = pTileColumns;
            this.mTileRows = pTileRows;
            this.mTileCount = this.mTileColumns * this.mTileRows;
            this.mCurrentTileColumn = 0;
            this.mCurrentTileRow = 0;

            this.initTextureBuffer();
        }

        protected override void initTextureBuffer()
        {
            if (this.mTileRows != 0 && this.mTileColumns != 0)
            {
                base.initTextureBuffer();
            }
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public override TiledTextureRegionBuffer getTextureBuffer()
        {
            return (TiledTextureRegionBuffer)this.mTextureRegionBuffer;
        }

        public int getTileCount()
        {
            return this.mTileCount;
        }

        public int getTileWidth()
        {
            return base.getWidth() / this.mTileColumns;
        }

        public int getTileHeight()
        {
            return base.getHeight() / this.mTileRows;
        }

        public int getCurrentTileColumn()
        {
            return this.mCurrentTileColumn;
        }

        public int getCurrentTileRow()
        {
            return this.mCurrentTileRow;
        }

        public int getCurrentTileIndex()
        {
            return this.mCurrentTileRow * this.mTileColumns + this.mCurrentTileColumn;
        }

        public void setCurrentTileIndex(int pTileColumn, int pTileRow)
        {
            if (pTileColumn != this.mCurrentTileColumn || pTileRow != this.mCurrentTileRow)
            {
                this.mCurrentTileColumn = pTileColumn;
                this.mCurrentTileRow = pTileRow;
                base.updateTextureRegionBuffer();
            }
        }

        public void setCurrentTileIndex(int pTileIndex)
        {
            if (pTileIndex < this.mTileCount)
            {
                int tileColumns = this.mTileColumns;
                this.setCurrentTileIndex(pTileIndex % tileColumns, pTileIndex / tileColumns);
            }
        }

        public float getTexturePositionOfCurrentTileX()
        {
            return base.getTexturePositionX() + this.mCurrentTileColumn * this.getTileWidth();
        }

        public float getTexturePositionOfCurrentTileY()
        {
            return base.getTexturePositionY() + this.mCurrentTileRow * this.getTileHeight();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override TiledTextureRegion clone()
        {
            TiledTextureRegion clone = new TiledTextureRegion(this.mTexture, this.getTexturePositionX(), this.getTexturePositionY(), this.getWidth(), this.getHeight(), this.mTileColumns, this.mTileRows);
            clone.setCurrentTileIndex(this.mCurrentTileColumn, this.mCurrentTileRow);
            return clone;
        }

        protected override TiledTextureRegionBuffer onCreateTextureRegionBuffer()
        {
            return new TiledTextureRegionBuffer(this, GL11Consts.GlStaticDraw);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void nextTile()
        {
            int tileIndex = (this.getCurrentTileIndex() + 1) % this.getTileCount();
            this.setCurrentTileIndex(tileIndex);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}