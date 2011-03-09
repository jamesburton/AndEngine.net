namespace andengine.engine.camera
{

    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;
    using Constants = andengine.util.constants.Constants;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    //using andengine.collision.BaseCollisionChecker;
    using BaseCollisionChecker = andengine.collision.BaseCollisionChecker;
    //using andengine.engine.camera.hud.HUD;
    using HUD = andengine.engine.camera.hud.HUD;
    //using andengine.engine.handler.IUpdateHandler;
    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;
    //using andengine.entity.shape.IShape;
    using IShape = andengine.entity.shape.IShape;
    //using andengine.entity.shape.RectangularShape;
    using RectangularShape = andengine.entity.shape.RectangularShape;
    //using andengine.input.touch.TouchEvent;
    using TouchEvent = andengine.input.touch.TouchEvent;
    //using andengine.opengl.util.GLHelper;
    using GLHelper = andengine.opengl.util.GLHelper;
    //using andengine.util.MathUtils;
    using MathUtils = andengine.util.MathUtils;

    /**
     * @author Nicolas Gramlich
     * @since 10:24:18 - 25.03.2010
     */
    public class Camera : IUpdateHandler
    {
        // ===========================================================
        // Constants
        // ===========================================================

        protected static /* final */ readonly float[] VERTICES_TOUCH_TMP = new float[2];

        // ===========================================================
        // Fields
        // ===========================================================

        private float mMinX;
        private float mMaxX;
        private float mMinY;
        private float mMaxY;

        private float mNearZ = -1.0f;
        private float mFarZ = 1.0f;

        private HUD mHUD;

        private IShape mChaseShape;

        protected float mRotation = 0;
        protected float mCameraSceneRotation = 0;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Camera(/* final */ float pX, /* final */ float pY, /* final */ float pWidth, /* final */ float pHeight)
        {
            this.mMinX = pX;
            this.mMaxX = pX + pWidth;
            this.mMinY = pY;
            this.mMaxY = pY + pHeight;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float GetMinX()
        {
            return this.mMinX;
        }

        public float MinX { get { return GetMinX(); } }

        public float GetMaxX()
        {
            return this.mMaxX;
        }

        public float MaxX { get { return GetMaxX(); } }

        public float GetMinY()
        {
            return this.mMinY;
        }

        public float MinY { get { return GetMinY(); } }

        public float GetMaxY()
        {
            return this.mMaxY;
        }

        public float MaxY { get { return GetMaxY(); } }

        public float GetNearZClippingPlane()
        {
            return this.mNearZ;
        }

        public float GetFarZClippingPlane()
        {
            return this.mFarZ;
        }

        public void SetNearZClippingPlane(/* final */ float pNearZClippingPlane)
        {
            this.mNearZ = pNearZClippingPlane;
        }

        public void SetFarZClippingPlane(/* final */ float pFarZClippingPlane)
        {
            this.mFarZ = pFarZClippingPlane;
        }

        public float NearZClippingPlane { get { return GetNearZClippingPlane(); } set { SetNearZClippingPlane(value); } }
        public float FarZClippingPlane { get { return GetFarZClippingPlane(); } set { SetFarZClippingPlane(value); } }

        public void SetZClippingPlanes(/* final */ float pNearZClippingPlane, /* final */ float pFarZClippingPlane)
        {
            this.mNearZ = pNearZClippingPlane;
            this.mFarZ = pFarZClippingPlane;
        }

        public float GetWidth()
        {
            return this.mMaxX - this.mMinX;
        }

        public float Width { get { return GetWidth(); } }

        public float GetHeight()
        {
            return this.mMaxY - this.mMinY;
        }

        public float Height { get { return GetHeight(); } }

        public float GetCenterX()
        {
            /* final */
            float minX = this.mMinX;
            return minX + (this.mMaxX - minX) * 0.5f;
        }

        public float CenterX { get { return GetCenterX(); } }

        public float GetCenterY()
        {
            /* final */
            float minY = this.mMinY;
            return minY + (this.mMaxY - minY) * 0.5f;
        }

        public float CenterY { get { return GetCenterY(); } }

        public virtual void SetCenter(/* final */ float pCenterX, /* final */ float pCenterY)
        {
            /* final */
            float dX = pCenterX - this.GetCenterX();
            /* final */
            float dY = pCenterY - this.GetCenterY();

            this.mMinX += dX;
            this.mMaxX += dX;
            this.mMinY += dY;
            this.mMaxY += dY;
        }

        public void OffsetCenter(/* final */ float pX, /* final */ float pY)
        {
            this.SetCenter(this.GetCenterX() + pX, this.GetCenterY() + pY);
        }

        public HUD HUD { get { return GetHUD(); } set { SetHUD(value); } }

        public HUD GetHUD()
        {
            return this.mHUD;
        }

        public void SetHUD(/* final */ HUD pHUD)
        {
            this.mHUD = pHUD;
            pHUD.SetCamera(this);
        }

        public bool HasHUD()
        {
            return this.mHUD != null;
        }

        public IShape ChaseShape { set { SetChaseShape(value); } }

        public void SetChaseShape(/* final */ IShape pChaseShape)
        {
            this.mChaseShape = pChaseShape;
        }

        public float GetRotation()
        {
            return this.mRotation;
        }

        public void SetRotation(/* final */ float pRotation)
        {
            this.mRotation = pRotation;
        }

        public float Rotation { get { return GetRotation(); } set { SetRotation(value); } }

        public float GetCameraSceneRotation()
        {
            return this.mCameraSceneRotation;
        }

        public void SetCameraSceneRotation(/* final */ float pCameraSceneRotation)
        {
            this.mCameraSceneRotation = pCameraSceneRotation;
        }

        public float CameraSceneRotation { get { return GetCameraSceneRotation(); } set { SetCameraSceneRotation(value); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ void OnUpdate(/* final */ float pSecondsElapsed)
        {
            if (this.mHUD != null)
            {
                this.mHUD.OnUpdate(pSecondsElapsed);
            }

            if (this.mChaseShape != null)
            {
                /* final */
                float[] centerCoordinates = this.mChaseShape.GetSceneCenterCoordinates();
                this.SetCenter(centerCoordinates[Constants.VERTEX_INDEX_X], centerCoordinates[Constants.VERTEX_INDEX_Y]);
            }
        }

        public /* override */ void Reset()
        {

        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void OnDrawHUD(/* final */ GL10 pGL)
        {
            if (this.mHUD != null)
            {
                this.mHUD.OnDraw(pGL, this);
            }
        }

        public bool IsRectangularShapeVisible(/* final */ RectangularShape pRectangularShape)
        {
            // final float otherLeft = pRectangularShape.getX();
            float otherLeft = pRectangularShape.X;
            // final float otherTop = pRectangularShape.getY();
            float otherTop = pRectangularShape.Y;
            // final float otherRight = pRectangularShape.getWidthScaled() + otherLeft;
            float otherRight = pRectangularShape.WidthScaled + otherLeft;
            // final float otherBottom = pRectangularShape.getHeightScaled() + otherTop;
            float otherBottom = pRectangularShape.HeightScaled + otherTop;

            // TODO Should also use RectangularShapeCollisionChecker
            //return BaseCollisionChecker.checkAxisAlignedRectangleCollision(this.getMinX(), this.getMinY(), this.getMaxX(), this.getMaxY(), otherLeft, otherTop, otherRight, otherBottom);
            return BaseCollisionChecker.CheckAxisAlignedRectangleCollision(this.MinX, this.MinY, this.MaxX, this.MaxY, otherLeft, otherTop, otherRight, otherBottom);
        }

        public void OnApplyMatrix(/* final */ GL10 pGL)
        {
            GLHelper.SetProjectionIdentityMatrix(pGL);

            //pGL.GlOrthof(this.getMinX(), this.getMaxX(), this.getMaxY(), this.getMinY(), this.mNearZ, this.mFarZ);
            pGL.GlOrthof(this.MinX, this.MaxX, this.MaxY, this.MinY, this.mNearZ, this.mFarZ);

            /* final */
            float rotation = this.mRotation;
            if (rotation != 0)
            {
                //this.ApplyRotation(pGL, this.getCenterX(), this.getCenterY(), rotation);
                this.ApplyRotation(pGL, this.CenterX, this.CenterY, rotation);
            }
        }

        public void OnApplyPositionIndependentMatrix(/* final */ GL10 pGL)
        {
            GLHelper.SetProjectionIdentityMatrix(pGL);

            /* final */
            float width = this.mMaxX - this.mMinX;
            /* final */
            float height = this.mMaxY - this.mMinY;

            pGL.GlOrthof(0, width, height, 0, this.mNearZ, this.mFarZ);

            /* final */
            float rotation = this.mRotation;
            if (rotation != 0)
            {
                this.ApplyRotation(pGL, width * 0.5f, height * 0.5f, rotation);
            }
        }

        public void OnApplyCameraSceneMatrix(/* final */ GL10 pGL)
        {
            GLHelper.SetProjectionIdentityMatrix(pGL);

            /* final */
            float width = this.mMaxX - this.mMinX;
            /* final */
            float height = this.mMaxY - this.mMinY;

            pGL.GlOrthof(0, width, height, 0, this.mNearZ, this.mFarZ);

            /* final */
            float cameraSceneRotation = this.mCameraSceneRotation;
            if (cameraSceneRotation != 0)
            {
                this.ApplyRotation(pGL, width * 0.5f, height * 0.5f, cameraSceneRotation);
            }
        }

        private void ApplyRotation(/* final */ GL10 pGL, /* final */ float pRotationCenterX, /* final */ float pRotationCenterY, /* final */ float pAngle)
        {
            pGL.GlTranslatef(pRotationCenterX, pRotationCenterY, 0);
            pGL.GlRotatef(pAngle, 0, 0, 1);
            pGL.GlTranslatef(-pRotationCenterX, -pRotationCenterY, 0);
        }

        public void ConvertSceneToCameraSceneTouchEvent(/* final */ TouchEvent pSceneTouchEvent)
        {
            this.UnapplySceneRotation(pSceneTouchEvent);

            this.ApplySceneToCameraSceneOffset(pSceneTouchEvent);

            this.ApplyCameraSceneRotation(pSceneTouchEvent);
        }

        public void ConvertCameraSceneToSceneTouchEvent(/* final */ TouchEvent pCameraSceneTouchEvent)
        {
            this.UnapplyCameraSceneRotation(pCameraSceneTouchEvent);

            this.UnapplySceneToCameraSceneOffset(pCameraSceneTouchEvent);

            this.ApplySceneRotation(pCameraSceneTouchEvent);
        }

        protected void ApplySceneToCameraSceneOffset(/* final */ TouchEvent pSceneTouchEvent)
        {
            pSceneTouchEvent.Offset(-this.mMinX, -this.mMinY);
        }

        protected void UnapplySceneToCameraSceneOffset(/* final */ TouchEvent pCameraSceneTouchEvent)
        {
            pCameraSceneTouchEvent.Offset(this.mMinX, this.mMinY);
        }

        private void ApplySceneRotation(/* final */ TouchEvent pCameraSceneTouchEvent)
        {
            /* final */
            float rotation = -this.mRotation;
            if (rotation != 0)
            {
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pCameraSceneTouchEvent.getX();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pCameraSceneTouchEvent.getY();

                //MathUtils.RotateAroundCenter(VERTICES_TOUCH_TMP, rotation, this.getCenterX(), this.getCenterY());
                MathUtils.RotateAroundCenter(VERTICES_TOUCH_TMP, rotation, this.CenterX, this.CenterY);

                pCameraSceneTouchEvent.Set(VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X], VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y]);
            }
        }

        private void UnapplySceneRotation(/* final */ TouchEvent pSceneTouchEvent)
        {
            /* final */
            float rotation = this.mRotation;

            if (rotation != 0)
            {
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pSceneTouchEvent.getX();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pSceneTouchEvent.X;
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pSceneTouchEvent.getY();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pSceneTouchEvent.Y;

                //MathUtils.revertRotateAroundCenter(VERTICES_TOUCH_TMP, rotation, this.getCenterX(), this.getCenterY());
                MathUtils.RevertRotateAroundCenter(VERTICES_TOUCH_TMP, rotation, this.CenterX, this.CenterY);

                pSceneTouchEvent.Set(VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X], VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y]);
            }
        }

        private void ApplyCameraSceneRotation(/* final */ TouchEvent pSceneTouchEvent)
        {
            /* final */
            float cameraSceneRotation = -this.mCameraSceneRotation;

            if (cameraSceneRotation != 0)
            {
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pSceneTouchEvent.getX();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pSceneTouchEvent.X;
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pSceneTouchEvent.getY();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pSceneTouchEvent.Y;

                MathUtils.RotateAroundCenter(VERTICES_TOUCH_TMP, cameraSceneRotation, (this.mMaxX - this.mMinX) * 0.5f, (this.mMaxY - this.mMinY) * 0.5f);

                pSceneTouchEvent.Set(VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X], VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y]);
            }
        }

        private void UnapplyCameraSceneRotation(/* final */ TouchEvent pCameraSceneTouchEvent)
        {
            /* final */
            float cameraSceneRotation = -this.mCameraSceneRotation;

            if (cameraSceneRotation != 0)
            {
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pCameraSceneTouchEvent.getX();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pCameraSceneTouchEvent.X;
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pCameraSceneTouchEvent.getY();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pCameraSceneTouchEvent.Y;

                MathUtils.RevertRotateAroundCenter(VERTICES_TOUCH_TMP, cameraSceneRotation, (this.mMaxX - this.mMinX) * 0.5f, (this.mMaxY - this.mMinY) * 0.5f);

                pCameraSceneTouchEvent.Set(VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X], VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y]);
            }
        }

        public void ConvertSurfaceToSceneTouchEvent(/* final */ TouchEvent pSurfaceTouchEvent, /* final */ int pSurfaceWidth, /* final */ int pSurfaceHeight)
        {
            /* final */
            float relativeX;
            /* final */
            float relativeY;

            /* final */
            float rotation = this.mRotation;
            if (rotation == 0)
            {
                //relativeX = pSurfaceTouchEvent.getX() / pSurfaceWidth;
                relativeX = pSurfaceTouchEvent.X / pSurfaceWidth;
                //relativeY = pSurfaceTouchEvent.getY() / pSurfaceHeight;
                relativeY = pSurfaceTouchEvent.Y / pSurfaceHeight;
            }
            else if (rotation == 180)
            {
                //relativeX = 1 - (pSurfaceTouchEvent.getX() / pSurfaceWidth);
                relativeX = 1 - (pSurfaceTouchEvent.X / pSurfaceWidth);
                //relativeY = 1 - (pSurfaceTouchEvent.getY() / pSurfaceHeight);
                relativeY = 1 - (pSurfaceTouchEvent.Y / pSurfaceHeight);
            }
            else
            {
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pSurfaceTouchEvent.getX();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] = pSurfaceTouchEvent.X;
                //VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pSurfaceTouchEvent.getY();
                VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] = pSurfaceTouchEvent.Y;

                MathUtils.RotateAroundCenter(VERTICES_TOUCH_TMP, rotation, pSurfaceWidth / 2, pSurfaceHeight / 2);

                relativeX = VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_X] / pSurfaceWidth;
                relativeY = VERTICES_TOUCH_TMP[Constants.VERTEX_INDEX_Y] / pSurfaceHeight;
            }

            this.convertAxisAlignedSurfaceToSceneTouchEvent(pSurfaceTouchEvent, relativeX, relativeY);
        }

        private void convertAxisAlignedSurfaceToSceneTouchEvent(/* final */ TouchEvent pSurfaceTouchEvent, /* final */ float pRelativeX, /* final */ float pRelativeY)
        {
            // final float minX = this.getMinX();
            float minX = this.MinX;
            // final float maxX = this.getMaxX();
            float maxX = this.MaxX;
            // final float minY = this.getMinY();
            float minY = this.MinY;
            // final float maxY = this.getMaxY();
            float maxY = this.MaxY;

            /* final */
            float x = minX + pRelativeX * (maxX - minX);
            /* final */
            float y = minY + pRelativeY * (maxY - minY);

            pSurfaceTouchEvent.Set(x, y);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}