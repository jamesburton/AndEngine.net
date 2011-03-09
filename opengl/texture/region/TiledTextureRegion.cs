namespace andengine.opengl.texture.region
{

    //import javax.microedition.khronos.opengles.GL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11Consts = Javax.Microedition.Khronos.Opengles.GL11Consts;

    using Texture = andengine.opengl.texture.Texture;
    using TiledTextureRegionBuffer = andengine.opengl.texture.region.buffer.TiledTextureRegionBuffer;
    using BaseTextureRegionBuffer = andengine.opengl.texture.region.buffer.BaseTextureRegionBuffer;

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

            this.InitTextureBuffer();
        }

        protected override void InitTextureBuffer()
        {
            if (this.mTileRows != 0 && this.mTileColumns != 0)
            {
                base.InitTextureBuffer();
            }
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public /* override */ new TiledTextureRegionBuffer GetTextureBuffer()
        {
            return (TiledTextureRegionBuffer)this.mTextureRegionBuffer;
        }
        public TiledTextureRegionBuffer TextureBuffer { get { return GetTextureBuffer(); } }

        public int GetTileCount()
        {
            return this.mTileCount;
        }
        public int TileCount { get { return GetTileCount(); } }

        public int GetTileWidth()
        {
            return base.GetWidth() / this.mTileColumns;
        }
        public override int Width { get { return GetWidth(); } }

        public int GetTileHeight()
        {
            return base.GetHeight() / this.mTileRows;
        }
        public override int Height { get { return GetHeight(); } }

        public int GetCurrentTileColumn()
        {
            return this.mCurrentTileColumn;
        }
        public int CurrentTileColumn { get { return GetCurrentTileColumn(); } }

        public int GetCurrentTileRow()
        {
            return this.mCurrentTileRow;
        }
        public int CurrentTileRow { get { return GetCurrentTileRow(); } }

        public int GetCurrentTileIndex()
        {
            return this.mCurrentTileRow * this.mTileColumns + this.mCurrentTileColumn;
        }
        public int CurrentTileIndex { get { return GetCurrentTileIndex(); } set { SetCurrentTileIndex(value); } }

        public void SetCurrentTileIndex(int pTileColumn, int pTileRow)
        {
            if (pTileColumn != this.mCurrentTileColumn || pTileRow != this.mCurrentTileRow)
            {
                this.mCurrentTileColumn = pTileColumn;
                this.mCurrentTileRow = pTileRow;
                base.UpdateTextureRegionBuffer();
            }
        }

        public void SetCurrentTileIndex(int pTileIndex)
        {
            if (pTileIndex < this.mTileCount)
            {
                int tileColumns = this.mTileColumns;
                this.SetCurrentTileIndex(pTileIndex % tileColumns, pTileIndex / tileColumns);
            }
        }

        public float GetTexturePositionOfCurrentTileX()
        {
            return base.GetTexturePositionX() + this.mCurrentTileColumn * this.GetTileWidth();
        }
        public float TexturePositionOfCurrentTileX { get { return GetTexturePositionOfCurrentTileX(); } }

        public float GetTexturePositionOfCurrentTileY()
        {
            return base.GetTexturePositionY() + this.mCurrentTileRow * this.GetTileHeight();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ virtual TiledTextureRegion Clone()
        {
            TiledTextureRegion clone = new TiledTextureRegion(this.mTexture, this.GetTexturePositionX(), this.GetTexturePositionY(), this.GetWidth(), this.GetHeight(), this.mTileColumns, this.mTileRows);
            clone.SetCurrentTileIndex(this.mCurrentTileColumn, this.mCurrentTileRow);
            return clone;
        }

        //protected override TiledTextureRegionBuffer OnCreateTextureRegionBuffer()
        protected override BaseTextureRegionBuffer OnCreateTextureRegionBufferCore()
        {
            return new TiledTextureRegionBuffer(this, GL11Consts.GlStaticDraw);
        }
        //BaseTextureRegionBuffer BaseTextureRegion.OnCreateTextureRegionBuffer() { return (BaseTextureRegionBuffer)OnCreateTextureRegionBuffer(); }
        //private BaseTextureRegionBuffer OnCreateTextureRegionBuffer() { return this.OnCreateTextureRegionBuffer(); }
        protected new TiledTextureRegionBuffer OnCreateTextureRegionBuffer() { return (TiledTextureRegionBuffer)OnCreateTextureRegionBufferCore(); }

        // ===========================================================
        // Methods
        // ===========================================================

        public void NextTile()
        {
            int tileIndex = (this.GetCurrentTileIndex() + 1) % this.GetTileCount();
            this.SetCurrentTileIndex(tileIndex);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}