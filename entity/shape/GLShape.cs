namespace andengine.entity.shape
{

    //import javax.microedition.khronos.opengles.GL10;
    //using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    //import javax.microedition.khronos.opengles.GL11;
    //using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;

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

        protected abstract void OnUpdateVertexBuffer();
        /*protected*/ public abstract VertexBuffer GetVertexBuffer();

        protected override void OnApplyVertices(/* final */ GL10 pGL)
        {
            if (GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS)
            {
                // TODO: Figure what the required conversion here is
                /* final */
                GL11 gl11 = (GL11)pGL;

                this.GetVertexBuffer().SelectOnHardware(gl11);
                GLHelper.VertexZeroPointer(gl11);
            }
            else
            {
                GLHelper.VertexPointer(pGL, this.GetVertexBuffer().GetFloatBuffer());
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        protected void UpdateVertexBuffer()
        {
            this.OnUpdateVertexBuffer();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}