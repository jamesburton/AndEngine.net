namespace andengine.entity.primitive
{

    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11Consts = Javax.Microedition.Khronos.Opengles.GL11Consts;

    using RectangularShape = andengine.entity.shape.RectangularShape;
    using RectangleVertexBuffer = andengine.opengl.vertex.RectangleVertexBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 19:05:49 - 11.04.2010
     */
    public abstract class BaseRectangle : RectangularShape
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

        public BaseRectangle(float pX, float pY, float pWidth, float pHeight)
            : base(pX, pY, pWidth, pHeight, new RectangleVertexBuffer(GL11Consts.GlStaticDraw))
        {
            this.updateVertexBuffer();
        }

        public BaseRectangle(float pX, float pY, float pWidth, float pHeight, RectangleVertexBuffer pRectangleVertexBuffer)
            : base(pX, pY, pWidth, pHeight, pRectangleVertexBuffer)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override RectangleVertexBuffer getVertexBuffer()
        {
            return (RectangleVertexBuffer)base.getVertexBuffer();
        }

        protected override void onUpdateVertexBuffer()
        {
            this.getVertexBuffer().update(this.mWidth, this.mHeight);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}