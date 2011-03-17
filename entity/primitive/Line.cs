using System;
using andengine.opengl.vertex;

namespace andengine.entity.primitive
{
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11Consts = Javax.Microedition.Khronos.Opengles.GL11Consts;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;


    using LineCollisionChecker = andengine.collision.LineCollisionChecker;
    using ShapeCollisionChecker = andengine.collision.ShapeCollisionChecker;
    using Camera = andengine.engine.camera.Camera;
    using GLShape = andengine.entity.shape.GLShape;
    using IShape = andengine.entity.shape.IShape;
    using BufferObjectManager = andengine.opengl.buffer.BufferObjectManager;
    using GLHelper = andengine.opengl.util.GLHelper;
    using LineVertexBuffer = andengine.opengl.vertex.LineVertexBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 09:50:36 - 04.04.2010
     */
    public class Line : GLShape
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /**/float LINEWIDTH_DEFAULT = 1.0f;

        // ===========================================================
        // Fields
        // ===========================================================

        protected float mX2;
        protected float mY2;

        private float mLineWidth;

        private /*final*/ LineVertexBuffer mLineVertexBuffer;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Line(float pX1, float pY1, float pX2, float pY2) :
            this(pX1, pY1, pX2, pY2, LINEWIDTH_DEFAULT)
        {
        }

        public Line(float pX1, float pY1, float pX2, float pY2, float pLineWidth) :
            base(pX1, pY1)
        {
            this.mX2 = pX2;
            this.mY2 = pY2;

            this.mLineWidth = pLineWidth;

            this.mLineVertexBuffer = new LineVertexBuffer(GL11Consts.GlStaticDraw);
            BufferObjectManager.GetActiveInstance().LoadBufferObject(this.mLineVertexBuffer);
            this.UpdateVertexBuffer();

            float width = this.GetWidth();
            float height = this.GetHeight();

            this.mRotationCenterX = width * 0.5f;
            this.mRotationCenterY = height * 0.5f;

            this.mScaleCenterX = this.mRotationCenterX;
            this.mScaleCenterY = this.mRotationCenterY;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float GetX1()
        {
            return base.GetX();
        }

        public float GetY1()
        {
            return base.GetY();
        }

        public float GetX2()
        {
            return this.mX2;
        }

        public float GetY2()
        {
            return this.mY2;
        }

        public float GetLineWidth()
        {
            return this.mLineWidth;
        }

        public void SetLineWidth(float pLineWidth)
        {
            this.mLineWidth = pLineWidth;
        }

        public override float GetBaseHeight()
        {
            return this.mY2 - this.mY;
        }

        public override float GetBaseWidth()
        {
            return this.mX2 - this.mX;
        }

        public override float GetHeight()
        {
            return this.mY2 - this.mY;
        }

        public override float GetWidth()
        {
            return this.mX2 - this.mX;
        }

        /**
         * Instead use {@link Line#setPosition(float, float, float, float)}.
         */
        public override void SetPosition(float pX, float pY)
        {
            float dX = this.mX - pX;
            float dY = this.mY - pY;

            base.SetPosition(pX, pY);

            this.mX2 += dX;
            this.mY2 += dY;
        }

        public void SetPosition(float pX1, float pY1, float pX2, float pY2)
        {
            this.mX2 = pX2;
            this.mY2 = pY2;

            base.SetPosition(pX1, pY1);

            this.UpdateVertexBuffer();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override bool IsCulled(Camera pCamera)
        {
            return false; // TODO
        }

        protected override void OnInitDraw(GL10 pGL)
        {
            base.OnInitDraw(pGL);
            GLHelper.DisableTextures(pGL);
            GLHelper.DisableTexCoordArray(pGL);
            GLHelper.LineWidth(pGL, this.mLineWidth);
        }

        public override VertexBuffer GetVertexBuffer()
        {
            return this.mLineVertexBuffer;
        }

        protected override void OnUpdateVertexBuffer()
        {
            this.mLineVertexBuffer.Update(0, 0, this.mX2 - this.mX, this.mY2 - this.mY);
        }

        protected override void DrawVertices(GL10 pGL, Camera pCamera)
        {
            pGL.GlDrawArrays(GL10Consts.GlLines, 0, LineVertexBuffer.VERTICES_PER_LINE);
        }

        public override float[] GetSceneCenterCoordinates()
        {
            return ShapeCollisionChecker.ConvertLocalToSceneCoordinates(this, (this.mX + this.mX2) * 0.5f, (this.mY + this.mY2) * 0.5f);
        }

        public override bool Contains(float pX, float pY)
        {
            return false;
        }

        public override float[] ConvertSceneToLocalCoordinates(float pX, float pY)
        {
            return null;
        }

        public override float[] ConvertLocalToSceneCoordinates(float pX, float pY)
        {
            return null;
        }

        public override bool CollidesWith(IShape pOtherShape)
        {
            if (pOtherShape is Line)
            {
                Line otherLine = (Line)pOtherShape;
                return LineCollisionChecker.CheckLineCollision(this.mX, this.mY, this.mX2, this.mY2, otherLine.mX, otherLine.mY, otherLine.mX2, otherLine.mY2);
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

