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
            //: base(2 * VERTICES_PER_RECTANGLE, pDrawType)
            : base(2 * RectangleVertexBuffer.VERTICES_PER_RECTANGLE, pDrawType)
        {
            this.mTextureRegion = pBaseTextureRegion;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public virtual BaseTextureRegion GetTextureRegionCore()
        {
            return this.mTextureRegion;
        }
        public BaseTextureRegion GetTextureRegion() { return GetTextureRegionCore(); }

        public bool FlippedHoriontal { get { return IsFlippedHorizontal(); } set { SetFlippedHorizontal(value); } }

        public bool IsFlippedHorizontal()
        {
            return this.mFlippedHorizontal;
        }

        public void SetFlippedHorizontal(bool pFlippedHorizontal)
        {
            if (this.mFlippedHorizontal != pFlippedHorizontal)
            {
                this.mFlippedHorizontal = pFlippedHorizontal;
                this.Update();
            }
        }

        public bool FlippedVertical { get { return IsFlippedVertical(); } set { SetFlippedVertical(value); } }

        public bool IsFlippedVertical()
        {
            return this.mFlippedVertical;
        }

        public void SetFlippedVertical(bool pFlippedVertical)
        {
            if (this.mFlippedVertical != pFlippedVertical)
            {
                this.mFlippedVertical = pFlippedVertical;
                this.Update();
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract float GetX1();

        protected abstract float GetY1();

        protected abstract float GetX2();

        protected abstract float GetY2();

        // ===========================================================
        // Methods
        // ===========================================================

        //public synchronized void update() {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update()
        {
            BaseTextureRegion textureRegion = this.mTextureRegion;
            Texture texture = textureRegion.GetTexture();

            if (texture == null)
            {
                return;
            }

            int x1 = Float.FloatToRawIntBits(this.GetX1());
            int y1 = Float.FloatToRawIntBits(this.GetY1());
            int x2 = Float.FloatToRawIntBits(this.GetX2());
            int y2 = Float.FloatToRawIntBits(this.GetY2());

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

            FastFloatBuffer buffer = this.GetFloatBuffer();
            buffer.Position(0);
            buffer.Put(bufferData);
            buffer.Position(0);

            base.SetHardwareBufferNeedsUpdate();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}