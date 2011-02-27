namespace andengine.opengl.vertex
{

    using BufferObject = andengine.opengl.buffer.BufferObject;

    /**
     * @author Nicolas Gramlich
     * @since 12:16:18 - 09.03.2010
     */
    public abstract class VertexBuffer : BufferObject
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

        public VertexBuffer(int pCapacity, int pDrawType)
            : base(pCapacity, pDrawType)
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

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}