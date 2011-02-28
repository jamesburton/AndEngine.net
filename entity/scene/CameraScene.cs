namespace andengine.entity.scene
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using Camera = andengine.engine.camera.Camera;
    using Shape = andengine.entity.shape.Shape;
    using TouchEvent = andengine.input.touch.TouchEvent;

    /**
     * @author Nicolas Gramlich
     * @since 15:35:53 - 29.03.2010
     */
    public class CameraScene : Scene
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected Camera mCamera;

        // ===========================================================
        // Constructors
        // ===========================================================

        /**
         * {@link CameraScene#setCamera(Camera)} needs to be called manually. Otherwise nothing will be drawn.
         */
        public CameraScene(int pLayerCount)
            : base(pLayerCount)
        {
            this.mCamera = null;
        }

        public CameraScene(int pLayerCount, Camera pCamera)
            : base(pLayerCount)
        {
            this.mCamera = pCamera;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public Camera getCamera()
        {
            return this.mCamera;
        }

        public void setCamera(Camera pCamera)
        {
            this.mCamera = pCamera;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override bool onSceneTouchEvent(TouchEvent pSceneTouchEvent)
        {
            if (this.mCamera == null)
            {
                return false;
            }
            else
            {
                this.mCamera.convertSceneToCameraSceneTouchEvent(pSceneTouchEvent);

                bool handled = base.onSceneTouchEvent(pSceneTouchEvent);

                if (handled)
                {
                    return true;
                }
                else
                {
                    this.mCamera.convertCameraSceneToSceneTouchEvent(pSceneTouchEvent);
                    return false;
                }
            }
        }

        protected override bool onChildSceneTouchEvent(TouchEvent pSceneTouchEvent)
        {
            bool childIsCameraScene = this.mChildScene is CameraScene;
            if (childIsCameraScene)
            {
                this.mCamera.convertCameraSceneToSceneTouchEvent(pSceneTouchEvent);
                bool result = base.onChildSceneTouchEvent(pSceneTouchEvent);
                this.mCamera.convertSceneToCameraSceneTouchEvent(pSceneTouchEvent);
                return result;
            }
            else
            {
                return base.onChildSceneTouchEvent(pSceneTouchEvent);
            }
        }

        protected override void onManagedDraw(GL10 pGL, Camera pCamera)
        {
            if (this.mCamera != null)
            {
                pGL.GlMatrixMode(GL10Consts.GlProjection);
                this.mCamera.onApplyCameraSceneMatrix(pGL);
                {
                    pGL.GlMatrixMode(GL10Consts.GlModelview);
                    pGL.GlPushMatrix();
                    pGL.GlLoadIdentity();

                    base.onManagedDraw(pGL, pCamera);

                    pGL.GlPopMatrix();
                }
                pGL.GlMatrixMode(GL10Consts.GlProjection);
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void centerShapeInCamera(Shape pShape)
        {
            Camera camera = this.mCamera;
            pShape.setPosition((camera.getWidth() - pShape.getWidth()) * 0.5f, (camera.getHeight() - pShape.getHeight()) * 0.5f);
        }

        public void centerShapeInCameraHorizontally(Shape pShape)
        {
            pShape.setPosition((this.mCamera.getWidth() - pShape.getWidth()) * 0.5f, pShape.getY());
        }

        public void centerShapeInCameraVertically(Shape pShape)
        {
            pShape.setPosition(pShape.getX(), (this.mCamera.getHeight() - pShape.getHeight()) * 0.5f);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}