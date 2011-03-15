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

        public Camera Camera { get { return GetCamera(); } set { SetCamera(value); } } 

        public Camera GetCamera()
        {
            return this.mCamera;
        }

        public void SetCamera(Camera pCamera)
        {
            this.mCamera = pCamera;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override bool OnSceneTouchEvent(TouchEvent pSceneTouchEvent)
        {
            if (this.mCamera == null)
            {
                return false;
            }
            else
            {
                this.mCamera.ConvertSceneToCameraSceneTouchEvent(pSceneTouchEvent);

                bool handled = base.OnSceneTouchEvent(pSceneTouchEvent);

                if (handled)
                {
                    return true;
                }
                else
                {
                    this.mCamera.ConvertCameraSceneToSceneTouchEvent(pSceneTouchEvent);
                    return false;
                }
            }
        }

        protected /*override*/ bool OnChildSceneTouchEvent(TouchEvent pSceneTouchEvent)
        {
            bool childIsCameraScene = this.mChildScene is CameraScene;
            if (childIsCameraScene)
            {
                this.mCamera.ConvertCameraSceneToSceneTouchEvent(pSceneTouchEvent);
                bool result = base.OnChildSceneTouchEvent(pSceneTouchEvent);
                this.mCamera.ConvertSceneToCameraSceneTouchEvent(pSceneTouchEvent);
                return result;
            }
            else
            {
                return base.OnChildSceneTouchEvent(pSceneTouchEvent);
            }
        }

        protected override void OnManagedDraw(GL10 pGL, Camera pCamera)
        {
            if (this.mCamera != null)
            {
                pGL.GlMatrixMode(GL10Consts.GlProjection);
                this.mCamera.OnApplyCameraSceneMatrix(pGL);
                {
                    pGL.GlMatrixMode(GL10Consts.GlModelview);
                    pGL.GlPushMatrix();
                    pGL.GlLoadIdentity();

                    base.OnManagedDraw(pGL, pCamera);

                    pGL.GlPopMatrix();
                }
                pGL.GlMatrixMode(GL10Consts.GlProjection);
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void CenterShapeInCamera(Shape pShape)
        {
            Camera camera = this.mCamera;
            //pShape.setPosition((camera.getWidth() - pShape.getWidth()) * 0.5f, (camera.getHeight() - pShape.getHeight()) * 0.5f);
            pShape.SetPosition((camera.Width - pShape.GetWidth()) * 0.5f, (camera.Height - pShape.GetHeight()) * 0.5f);
        }

        public void centerShapeInCameraHorizontally(Shape pShape)
        {
            //pShape.setPosition((this.mCamera.getWidth() - pShape.getWidth()) * 0.5f, pShape.getY());
            pShape.SetPosition((this.mCamera.Width - pShape.GetWidth()) * 0.5f, pShape.Y);
        }

        public void centerShapeInCameraVertically(Shape pShape)
        {
            //pShape.setPosition(pShape.getX(), (this.mCamera.getHeight() - pShape.getHeight()) * 0.5f);
            pShape.SetPosition(pShape.X, (this.mCamera.Height - pShape.GetHeight()) * 0.5f);
            pShape.SetPosition(pShape.X, (this.mCamera.Height - pShape.GetHeight()) * 0.5f);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}