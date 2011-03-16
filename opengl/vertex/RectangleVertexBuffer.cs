namespace andengine.opengl.vertex
{

    using FastFloatBuffer = andengine.opengl.util.FastFloatBuffer;
    using Java.Lang;
    using System.Runtime.CompilerServices;

    /**
     * @author Nicolas Gramlich
     * @since 13:07:25 - 13.03.2010
     */
    public class RectangleVertexBuffer : VertexBuffer
    {
        // ===========================================================
        // Constants
        // ===========================================================

        public static readonly int VERTICES_PER_RECTANGLE = 4;

        private static readonly int FLOAT_TO_RAW_INT_BITS_ZERO = Float.FloatToRawIntBits(0);

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public RectangleVertexBuffer(int pDrawType)
            : base(2 * VERTICES_PER_RECTANGLE, pDrawType)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        //public synchronized void update(final float pWidth, final float pHeight) {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(float pWidth, float pHeight)
        {
            int x = FLOAT_TO_RAW_INT_BITS_ZERO;
            int y = FLOAT_TO_RAW_INT_BITS_ZERO;
            int x2 = Float.FloatToRawIntBits(pWidth);
            int y2 = Float.FloatToRawIntBits(pHeight);

            int[] bufferData = this.mBufferData;
            bufferData[0] = x;
            bufferData[1] = y;

            bufferData[2] = x;
            bufferData[3] = y2;

            bufferData[4] = x2;
            bufferData[5] = y;

            bufferData[6] = x2;
            bufferData[7] = y2;

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