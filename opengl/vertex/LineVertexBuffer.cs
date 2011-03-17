using Java.Lang;

namespace andengine.opengl.vertex
{

    using FastFloatBuffer = andengine.opengl.util.FastFloatBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 13:07:25 - 13.03.2010
     */
    public class LineVertexBuffer : VertexBuffer
    {
        // ===========================================================
        // Constants
        // ===========================================================

        public static int VERTICES_PER_LINE = 2;

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public LineVertexBuffer(int pDrawType) :
            base(2 * VERTICES_PER_LINE, pDrawType)
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

        //TODO: This was synchronized in Java.  Do we need to add any locking code in C#?
        public /*synchronized*/ void Update(float pX1, float pY1, float pX2, float pY2)
        {
            int[] bufferData = this.mBufferData;

            bufferData[0] = Float.FloatToRawIntBits(pX1);
            bufferData[1] = Float.FloatToRawIntBits(pY1);

            bufferData[2] = Float.FloatToRawIntBits(pX2);
            bufferData[3] = Float.FloatToRawIntBits(pY2);

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