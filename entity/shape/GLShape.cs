namespace andengine.entity.shape
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.GL10Consts;
    //import javax.microedition.khronos.opengles.GL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.GL11Consts;

    //using andengine.opengl.util.GLHelper;
    using GLHelper = andengine.opengl.util.GLHelper;
    //using andengine.opengl.vertex.VertexBuffer;
    using VertexBuffer = andengine.opengl.vertex.VertexBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 11:51:27 - 13.03.2010
     */
    public abstract class GLShape : Shape
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

        public GLShape(/* final */ float pX, /* final */ float pY)
            : base(pX, pY)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        protected abstract void onUpdateVertexBuffer();
        protected abstract VertexBuffer getVertexBuffer();

        protected override void onApplyVertices(/* final */ GL10 pGL)
        {
            if (GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS)
            {
                // TODO: Figure what the required conversion here is
                /* final */
                GL11 gl11 = (GL11)pGL;

                this.getVertexBuffer().selectOnHardware(gl11);
                GLHelper.vertexZeroPointer(gl11);
            }
            else
            {
                GLHelper.vertexPointer(pGL, this.getVertexBuffer().getFloatBuffer());
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        protected void updateVertexBuffer()
        {
            this.onUpdateVertexBuffer();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}