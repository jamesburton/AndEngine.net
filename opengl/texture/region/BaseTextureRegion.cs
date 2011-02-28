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

            this.mTextureRegionBuffer = this.onCreateTextureRegionBuffer();

            BufferObjectManager.getActiveInstance().loadBufferObject(this.mTextureRegionBuffer);

            this.initTextureBuffer();
        }

        protected void initTextureBuffer()
        {
            this.updateTextureRegionBuffer();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public int Width { get { return getWidth(); } set { setWidth(value); } }
        public int Height { get { return getHeight(); } set { setHeight(value); } }

        public int getWidth()
        {
            return this.mWidth;
        }

        public int getHeight()
        {
            return this.mHeight;
        }

        public void setWidth(int pWidth)
        {
            this.mWidth = pWidth;
            this.updateTextureRegionBuffer();
        }

        public void setHeight(int pHeight)
        {
            this.mHeight = pHeight;
            this.updateTextureRegionBuffer();
        }

        public void setTexturePosition(int pX, int pY)
        {
            this.mTexturePositionX = pX;
            this.mTexturePositionY = pY;
            this.updateTextureRegionBuffer();
        }

        public int TexturePositionX { get { return getTexturePositionX(); } }
        public int TexturePositionY { get { return getTexturePositionY(); } }

        public int getTexturePositionX()
        {
            return this.mTexturePositionX;
        }

        public int getTexturePositionY()
        {
            return this.mTexturePositionY;
        }

        public Texture getTexture()
        {
            return this.mTexture;
        }

        public BaseTextureRegionBuffer getTextureBuffer()
        {
            return this.mTextureRegionBuffer;
        }

        public bool FlippedHoriontal { get { return isFlippedHorizontal(); } set { setFlippedHorizontal(value); } }

        public bool isFlippedHorizontal()
        {
            return this.mTextureRegionBuffer.isFlippedHorizontal();
        }

        public void setFlippedHorizontal(bool pFlippedHorizontal)
        {
            this.mTextureRegionBuffer.setFlippedHorizontal(pFlippedHorizontal);
        }

        public bool FlippedVertical { get { return isFlippedVertical(); } set { setFlippedVertical(value); } }

        public bool isFlippedVertical()
        {
            return this.mTextureRegionBuffer.isFlippedVertical();
        }

        public void setFlippedVertical(bool pFlippedVertical)
        {
            this.mTextureRegionBuffer.setFlippedVertical(pFlippedVertical);
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract BaseTextureRegionBuffer onCreateTextureRegionBuffer();

        // ===========================================================
        // Methods
        // ===========================================================

        protected void updateTextureRegionBuffer()
        {
            this.mTextureRegionBuffer.update();
        }

        public void onApply(GL10 pGL)
        {
            if (GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS)
            {
                GL11 gl11 = (GL11)pGL;

                this.mTextureRegionBuffer.selectOnHardware(gl11);

                GLHelper.bindTexture(pGL, this.mTexture.getHardwareTextureID());
                GLHelper.texCoordZeroPointer(gl11);
            }
            else
            {
                GLHelper.bindTexture(pGL, this.mTexture.getHardwareTextureID());
                GLHelper.texCoordPointer(pGL, this.mTextureRegionBuffer.getFloatBuffer());
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}