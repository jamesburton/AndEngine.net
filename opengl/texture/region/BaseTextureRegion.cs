namespace andengine.opengl.texture.region
{

    //import javax.microedition.khronos.opengles.GL10;
    //import javax.microedition.khronos.opengles.GL11;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11Consts = Javax.Microedition.Khronos.Opengles.GL11Consts;

    using BufferObjectManager = andengine.opengl.buffer.BufferObjectManager;
    using Texture = andengine.opengl.texture.Texture;
    using BaseTextureRegionBuffer = andengine.opengl.texture.region.buffer.BaseTextureRegionBuffer;
    using GLHelper = andengine.opengl.util.GLHelper;

    /**
     * @author Nicolas Gramlich
     * @since 14:29:59 - 08.03.2010
     */
    public abstract class BaseTextureRegion
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected readonly Texture mTexture;

        protected readonly BaseTextureRegionBuffer mTextureRegionBuffer;

        protected int mWidth;
        protected int mHeight;

        protected int mTexturePositionX;
        protected int mTexturePositionY;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseTextureRegion(Texture pTexture, int pTexturePositionX, int pTexturePositionY, int pWidth, int pHeight)
        {
            this.mTexture = pTexture;
            this.mTexturePositionX = pTexturePositionX;
            this.mTexturePositionY = pTexturePositionY;
            this.mWidth = pWidth;
            this.mHeight = pHeight;

            this.mTextureRegionBuffer = this.OnCreateTextureRegionBuffer();

            BufferObjectManager.GetActiveInstance().LoadBufferObject(this.mTextureRegionBuffer);

            this.InitTextureBuffer();
        }

        protected virtual void InitTextureBuffer()
        {
            this.UpdateTextureRegionBuffer();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public virtual int Width { get { return GetWidth(); } set { SetWidth(value); } }
        public virtual int Height { get { return GetHeight(); } set { SetHeight(value); } }

        public int GetWidth()
        {
            return this.mWidth;
        }

        public int GetHeight()
        {
            return this.mHeight;
        }

        public void SetWidth(int pWidth)
        {
            this.mWidth = pWidth;
            this.UpdateTextureRegionBuffer();
        }

        public void SetHeight(int pHeight)
        {
            this.mHeight = pHeight;
            this.UpdateTextureRegionBuffer();
        }

        public void SetTexturePosition(int pX, int pY)
        {
            this.mTexturePositionX = pX;
            this.mTexturePositionY = pY;
            this.UpdateTextureRegionBuffer();
        }

        public int TexturePositionX { get { return GetTexturePositionX(); } }
        public int TexturePositionY { get { return GetTexturePositionY(); } }

        public int GetTexturePositionX()
        {
            return this.mTexturePositionX;
        }

        public int GetTexturePositionY()
        {
            return this.mTexturePositionY;
        }

        public Texture GetTexture()
        {
            return this.mTexture;
        }

        public virtual BaseTextureRegionBuffer GetTextureBuffer()
        {
            return this.mTextureRegionBuffer;
        }

        public bool FlippedHoriontal { get { return IsFlippedHorizontal(); } set { SetFlippedHorizontal(value); } }

        public bool IsFlippedHorizontal()
        {
            return this.mTextureRegionBuffer.IsFlippedHorizontal();
        }

        public void SetFlippedHorizontal(bool pFlippedHorizontal)
        {
            this.mTextureRegionBuffer.SetFlippedHorizontal(pFlippedHorizontal);
        }

        public bool FlippedVertical { get { return IsFlippedVertical(); } set { SetFlippedVertical(value); } }

        public bool IsFlippedVertical()
        {
            return this.mTextureRegionBuffer.IsFlippedVertical();
        }

        public void SetFlippedVertical(bool pFlippedVertical)
        {
            this.mTextureRegionBuffer.SetFlippedVertical(pFlippedVertical);
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        /* Covariance is not supported, see these for discussion and work arounds:
         * 
         * http://www.simple-talk.com/community/blogs/simonc/archive/2010/07/14/93495.aspx
         * http://stackoverflow.com/questions/421851/how-to-return-subtype-in-overridden-method-of-subclass-in-c
        protected abstract BaseTextureRegionBuffer OnCreateTextureRegionBuffer();
        //*/
        protected abstract BaseTextureRegionBuffer OnCreateTextureRegionBufferCore();
        protected virtual BaseTextureRegionBuffer OnCreateTextureRegionBuffer() { return OnCreateTextureRegionBufferCore(); }

        // ===========================================================
        // Methods
        // ===========================================================

        protected void UpdateTextureRegionBuffer()
        {
            this.mTextureRegionBuffer.Update();
        }

        public void OnApply(GL10 pGL)
        {
            if (GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS)
            {
                GL11 gl11 = (GL11)pGL;

                this.mTextureRegionBuffer.SelectOnHardware(gl11);

                GLHelper.BindTexture(pGL, this.mTexture.GetHardwareTextureID());
                GLHelper.TexCoordZeroPointer(gl11);
            }
            else
            {
                GLHelper.BindTexture(pGL, this.mTexture.GetHardwareTextureID());
                GLHelper.TexCoordPointer(pGL, this.mTextureRegionBuffer.GetFloatBuffer());
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}