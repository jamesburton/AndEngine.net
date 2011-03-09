namespace andengine.entity.shape
{

    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;
    using Constants = andengine.util.constants.Constants;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

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
            BufferObjectManager.GetActiveInstance().LoadBufferObject(this.mVertexBuffer);

            this.mRotationCenterX = pWidth * 0.5f;
            this.mRotationCenterY = pHeight * 0.5f;

            this.mScaleCenterX = this.mRotationCenterX;
            this.mScaleCenterY = this.mRotationCenterY;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public VertexBuffer VertexBuffer { get { return GetVertexBuffer(); } }

        public override VertexBuffer GetVertexBuffer()
        {
            return this.mVertexBuffer;
        }

        public float Width { get { return GetWidth(); } set { SetWidth(value); } }
        public float Height { get { return GetHeight(); } set { SetHeight(value); } }

        public override float GetWidth()
        {
            return this.mWidth;
        }

        public override float GetHeight()
        {
            return this.mHeight;
        }

        public float BaseWidth { get { return GetBaseWidth(); } }
        public float BaseHeight { get { return GetBaseHeight(); } }

        public override float GetBaseWidth()
        {
            return this.mBaseWidth;
        }

        public override float GetBaseHeight()
        {
            return this.mBaseHeight;
        }

        public void SetWidth(/* final */ float pWidth)
        {
            this.mWidth = pWidth;
            this.UpdateVertexBuffer();
        }

        public void SetHeight(/* final */ float pHeight)
        {
            this.mHeight = pHeight;
            this.UpdateVertexBuffer();
        }

        public void SetSize(/* final */ float pWidth, /* final */ float pHeight)
        {
            this.mWidth = pWidth;
            this.mHeight = pHeight;
            this.UpdateVertexBuffer();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public void SetBaseSize()
        {
            if (this.mWidth != this.mBaseWidth || this.mHeight != this.mBaseHeight)
            {
                this.mWidth = this.mBaseWidth;
                this.mHeight = this.mBaseHeight;
                this.OnPositionChanged();
                this.UpdateVertexBuffer();
            }
        }

        protected override bool IsCulled(/* final */ Camera pCamera)
        {
            /* final */
            float x = this.mX;
            /* final */
            float y = this.mY;
            return x > pCamera.GetMaxX()
                || y > pCamera.GetMaxY()
                || x + this.GetWidth() < pCamera.GetMinX()
                || y + this.GetHeight() < pCamera.GetMinY();
        }

        protected override void DrawVertices(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            pGL.GlDrawArrays(GL10Consts.GlTriangleStrip, 0, 4);
        }

        public override void Reset()
        {
            base.Reset();
            this.SetBaseSize();

            /* final */
            float baseWidth = this.GetBaseWidth();
            /* final */
            float baseHeight = this.GetBaseHeight();

            this.mRotationCenterX = baseWidth * 0.5f;
            this.mRotationCenterY = baseHeight * 0.5f;

            this.mScaleCenterX = this.mRotationCenterX;
            this.mScaleCenterY = this.mRotationCenterY;
        }

        public override bool Contains(/* final */ float pX, /* final */ float pY)
        {
            return RectangularShapeCollisionChecker.CheckContains(this, pX, pY);
        }

        public override float[] GetSceneCenterCoordinates()
        {
            return this.ConvertLocalToSceneCoordinates(this.mWidth * 0.5f, this.mHeight * 0.5f);
        }

        public override float[] ConvertLocalToSceneCoordinates(/* final */ float pX, /* final */ float pY)
        {
            /* final */
            float[] sceneCoordinates = ShapeCollisionChecker.ConvertLocalToSceneCoordinates(this, pX, pY);
            sceneCoordinates[Constants.VERTEX_INDEX_X] += this.mX;
            sceneCoordinates[Constants.VERTEX_INDEX_Y] += this.mY;
            return sceneCoordinates;
        }

        public override float[] ConvertSceneToLocalCoordinates(/* final */ float pX, /* final */ float pY)
        {
            /* final */
            float[] localCoordinates = ShapeCollisionChecker.ConvertSceneToLocalCoordinates(this, pX, pY);
            localCoordinates[Constants.VERTEX_INDEX_X] -= this.mX;
            localCoordinates[Constants.VERTEX_INDEX_Y] -= this.mY;
            return localCoordinates;
        }

        public override bool CollidesWith(/* final */ IShape pOtherShape)
        {
            if (pOtherShape is RectangularShape)
            {
                /* final */
                RectangularShape pOtherRectangularShape = (RectangularShape)pOtherShape;

                return RectangularShapeCollisionChecker.CheckCollision(this, pOtherRectangularShape);
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