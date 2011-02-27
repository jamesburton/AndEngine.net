namespace andengine.entity.shape
{

    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;
    using Constants = andengine.util.constants.Constants;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;

    using RectangularShapeCollisionChecker = andengine.collision.RectangularShapeCollisionChecker;
    using ShapeCollisionChecker = andengine.collision.ShapeCollisionChecker;
    using Camera = andengine.engine.camera.Camera;
    using BufferObjectManager = andengine.opengl.buffer.BufferObjectManager;
    using VertexBuffer = andengine.opengl.vertex.VertexBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 11:37:50 - 04.04.2010
     */
    public abstract class RectangularShape : GLShape
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected float mBaseWidth;
        protected float mBaseHeight;

        protected float mWidth;
        protected float mHeight;

        private /* final */ readonly VertexBuffer mVertexBuffer;

        // ===========================================================
        // Constructors
        // ===========================================================

        public RectangularShape(/* final */ float pX, /* final */ float pY, /* final */ float pWidth, /* final */ float pHeight, /* final */ VertexBuffer pVertexBuffer)
            : base(pX, pY)
        {

            this.mBaseWidth = pWidth;
            this.mBaseHeight = pHeight;

            this.mWidth = pWidth;
            this.mHeight = pHeight;

            this.mVertexBuffer = pVertexBuffer;
            BufferObjectManager.getActiveInstance().loadBufferObject(this.mVertexBuffer);

            this.mRotationCenterX = pWidth * 0.5f;
            this.mRotationCenterY = pHeight * 0.5f;

            this.mScaleCenterX = this.mRotationCenterX;
            this.mScaleCenterY = this.mRotationCenterY;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public VertexBuffer VertexBuffer { get { return getVertexBuffer(); } }

        public override VertexBuffer getVertexBuffer()
        {
            return this.mVertexBuffer;
        }

        public float Width { get { return getWidth(); } set { setWidth(value); } }
        public float Height { get { return getHeight(); } set { setHeight(value); } }

        public override float getWidth()
        {
            return this.mWidth;
        }

        public override float getHeight()
        {
            return this.mHeight;
        }

        public float BaseWidth { get { return getBaseWidth(); } }
        public float BaseHeight { get { return getBaseHeight(); } }

        public override float getBaseWidth()
        {
            return this.mBaseWidth;
        }

        public override float getBaseHeight()
        {
            return this.mBaseHeight;
        }

        public void setWidth(/* final */ float pWidth)
        {
            this.mWidth = pWidth;
            this.updateVertexBuffer();
        }

        public void setHeight(/* final */ float pHeight)
        {
            this.mHeight = pHeight;
            this.updateVertexBuffer();
        }

        public void setSize(/* final */ float pWidth, /* final */ float pHeight)
        {
            this.mWidth = pWidth;
            this.mHeight = pHeight;
            this.updateVertexBuffer();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public void setBaseSize()
        {
            if (this.mWidth != this.mBaseWidth || this.mHeight != this.mBaseHeight)
            {
                this.mWidth = this.mBaseWidth;
                this.mHeight = this.mBaseHeight;
                this.onPositionChanged();
                this.updateVertexBuffer();
            }
        }

        protected override bool isCulled(/* final */ Camera pCamera)
        {
            /* final */
            float x = this.mX;
            /* final */
            float y = this.mY;
            return x > pCamera.getMaxX()
                || y > pCamera.getMaxY()
                || x + this.getWidth() < pCamera.getMinX()
                || y + this.getHeight() < pCamera.getMinY();
        }

        protected override void drawVertices(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            pGL.glDrawArrays(GL10.GlTriangleStrip, 0, 4);
        }

        public override void reset()
        {
            base.reset();
            this.setBaseSize();

            /* final */
            float baseWidth = this.getBaseWidth();
            /* final */
            float baseHeight = this.getBaseHeight();

            this.mRotationCenterX = baseWidth * 0.5f;
            this.mRotationCenterY = baseHeight * 0.5f;

            this.mScaleCenterX = this.mRotationCenterX;
            this.mScaleCenterY = this.mRotationCenterY;
        }

        public override bool contains(/* final */ float pX, /* final */ float pY)
        {
            return RectangularShapeCollisionChecker.checkContains(this, pX, pY);
        }

        public override float[] getSceneCenterCoordinates()
        {
            return this.convertLocalToSceneCoordinates(this.mWidth * 0.5f, this.mHeight * 0.5f);
        }

        public override float[] convertLocalToSceneCoordinates(/* final */ float pX, /* final */ float pY)
        {
            /* final */
            float[] sceneCoordinates = ShapeCollisionChecker.convertLocalToSceneCoordinates(this, pX, pY);
            sceneCoordinates[Constants.VERTEX_INDEX_X] += this.mX;
            sceneCoordinates[Constants.VERTEX_INDEX_Y] += this.mY;
            return sceneCoordinates;
        }

        public override float[] convertSceneToLocalCoordinates(/* final */ float pX, /* final */ float pY)
        {
            /* final */
            float[] localCoordinates = ShapeCollisionChecker.convertSceneToLocalCoordinates(this, pX, pY);
            localCoordinates[Constants.VERTEX_INDEX_X] -= this.mX;
            localCoordinates[Constants.VERTEX_INDEX_Y] -= this.mY;
            return localCoordinates;
        }

        public override bool collidesWith(/* final */ IShape pOtherShape)
        {
            if (pOtherShape is RectangularShape)
            {
                /* final */
                RectangularShape pOtherRectangularShape = (RectangularShape)pOtherShape;

                return RectangularShapeCollisionChecker.checkCollision(this, pOtherRectangularShape);
            }
            else
            {
                return false;
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}