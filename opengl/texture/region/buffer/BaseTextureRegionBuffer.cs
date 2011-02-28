namespace andengine.opengl.texture.region.buffer
{

    //import static org.anddev.andengine.opengl.vertex.RectangleVertexBuffer.VERTICES_PER_RECTANGLE;
    using RectangleVertexBuffer = andengine.opengl.vertex.RectangleVertexBuffer;

    using BufferObject = andengine.opengl.buffer.BufferObject;
    using Texture = andengine.opengl.texture.Texture;
    using BaseTextureRegion = andengine.opengl.texture.region.BaseTextureRegion;
    using FastFloatBuffer = andengine.opengl.util.FastFloatBuffer;
    using System.Runtime.CompilerServices;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 19:05:50 - 09.03.2010
     */
    public abstract class BaseTextureRegionBuffer : BufferObject
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected readonly BaseTextureRegion mTextureRegion;
        private bool mFlippedVertical;
        private bool mFlippedHorizontal;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseTextureRegionBuffer(BaseTextureRegion pBaseTextureRegion, int pDrawType)
            : base(2 * VERTICES_PER_RECTANGLE, pDrawType)
        {
            this.mTextureRegion = pBaseTextureRegion;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public BaseTextureRegion getTextureRegion()
        {
            return this.mTextureRegion;
        }

        public bool FlippedHoriontal { get { return isFlippedHorizontal(); } set { setFlippedHorizontal(value); } }

        public bool isFlippedHorizontal()
        {
            return this.mFlippedHorizontal;
        }

        public void setFlippedHorizontal(bool pFlippedHorizontal)
        {
            if (this.mFlippedHorizontal != pFlippedHorizontal)
            {
                this.mFlippedHorizontal = pFlippedHorizontal;
                this.update();
            }
        }

        public bool FlippedVertical { get { return isFlippedVertical(); } set { setFlippedVertical(value); } }

        public bool isFlippedVertical()
        {
            return this.mFlippedVertical;
        }

        public void setFlippedVertical(bool pFlippedVertical)
        {
            if (this.mFlippedVertical != pFlippedVertical)
            {
                this.mFlippedVertical = pFlippedVertical;
                this.update();
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract float getX1();

        protected abstract float getY1();

        protected abstract float getX2();

        protected abstract float getY2();

        // ===========================================================
        // Methods
        // ===========================================================

        //public synchronized void update() {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void update()
        {
            BaseTextureRegion textureRegion = this.mTextureRegion;
            Texture texture = textureRegion.getTexture();

            if (texture == null)
            {
                return;
            }

            int x1 = Float.FloatToRawIntBits(this.getX1());
            int y1 = Float.FloatToRawIntBits(this.getY1());
            int x2 = Float.FloatToRawIntBits(this.getX2());
            int y2 = Float.FloatToRawIntBits(this.getY2());

            int[] bufferData = this.mBufferData;

            if (this.mFlippedVertical)
            {
                if (this.mFlippedHorizontal)
                {
                    bufferData[0] = x2;
                    bufferData[1] = y2;

                    bufferData[2] = x2;
                    bufferData[3] = y1;

                    bufferData[4] = x1;
                    bufferData[5] = y2;

                    bufferData[6] = x1;
                    bufferData[7] = y1;
                }
                else
                {
                    bufferData[0] = x1;
                    bufferData[1] = y2;

                    bufferData[2] = x1;
                    bufferData[3] = y1;

                    bufferData[4] = x2;
                    bufferData[5] = y2;

                    bufferData[6] = x2;
                    bufferData[7] = y1;
                }
            }
            else
            {
                if (this.mFlippedHorizontal)
                {
                    bufferData[0] = x2;
                    bufferData[1] = y1;

                    bufferData[2] = x2;
                    bufferData[3] = y2;

                    bufferData[4] = x1;
                    bufferData[5] = y1;

                    bufferData[6] = x1;
                    bufferData[7] = y2;
                }
                else
                {
                    bufferData[0] = x1;
                    bufferData[1] = y1;

                    bufferData[2] = x1;
                    bufferData[3] = y2;

                    bufferData[4] = x2;
                    bufferData[5] = y1;

                    bufferData[6] = x2;
                    bufferData[7] = y2;
                }
            }

            FastFloatBuffer buffer = this.getFloatBuffer();
            buffer.position(0);
            buffer.put(bufferData);
            buffer.position(0);

            base.setHardwareBufferNeedsUpdate();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}