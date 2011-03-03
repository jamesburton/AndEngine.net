namespace andengine.engine
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    //using andengine.engine.camera.Camera;
    using Camera = andengine.engine.camera.Camera;
    //using andengine.engine.options.EngineOptions;
    using EngineOptions = andengine.engine.options.EngineOptions;
    //using andengine.input.touch.TouchEvent;
    using TouchEvent = andengine.input.touch.TouchEvent;

    /**
     * @author Nicolas Gramlich
     * @since 22:28:34 - 27.03.2010
     */
    public class SingleSceneSplitScreenEngine : Engine
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ readonly Camera mSecondCamera;

        // ===========================================================
        // Constructors
        // ===========================================================

        public SingleSceneSplitScreenEngine(/* final */ EngineOptions pEngineOptions, /* final */ Camera pSecondCamera)
            : base(pEngineOptions)
        {
            //super(pEngineOptions);
            this.mSecondCamera = pSecondCamera;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        //@Deprecated
        public override Camera GetCamera()
        {
            //return super.mCamera;
            return base.mCamera;
        }

        public Camera GetFirstCamera()
        {
            return base.mCamera;
        }

        public Camera GetSecondCamera()
        {
            return this.mSecondCamera;
        }

        public Camera FirstCamera { get { return GetFirstCamera(); } }
        public Camera SecondCamera { get { return GetSecondCamera(); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override void OnDrawScene(/* final */ GL10 pGL)
        {
            /* final */
            //Camera firstCamera = this.GetFirstCamera();
            Camera firstCamera = this.FirstCamera;
            /* final */
            Camera secondCamera = this.SecondCamera;

            /* final */
            int surfaceWidth = this.mSurfaceWidth;
            /* final */
            int surfaceWidthHalf = surfaceWidth >> 1;

            /* final */
            int surfaceHeight = this.mSurfaceHeight;

            //pGL.glEnable(GL10.GL_SCISSOR_TEST); // TODO --> GLHelper
            pGL.GlEnable(GL10Consts.GlScissorTest); // TODO --> GLHelper

            /* First Screen. With first camera, on the left half of the screens width. */
            {
                pGL.GlScissor(0, 0, surfaceWidthHalf, surfaceHeight);
                pGL.GlViewport(0, 0, surfaceWidthHalf, surfaceHeight);

                //super.mScene.onDraw(pGL, firstCamera);
                base.mScene.OnDraw(pGL, firstCamera);
                firstCamera.OnDrawHUD(pGL);
            }

            /* Second Screen. With second camera, on the right half of the screens width. */
            {
                pGL.GlScissor(surfaceWidthHalf, 0, surfaceWidthHalf, surfaceHeight);
                pGL.GlViewport(surfaceWidthHalf, 0, surfaceWidthHalf, surfaceHeight);

                //super.mScene.onDraw(pGL, secondCamera);
                base.mScene.OnDraw(pGL, secondCamera);
                secondCamera.OnDrawHUD(pGL);
            }

            pGL.GlDisable(GL10Consts.GlScissorTest);
        }

        protected override Camera GetCameraFromSurfaceTouchEvent(/* final */ TouchEvent pTouchEvent)
        {
            //if (pTouchEvent.getX() <= this.mSurfaceWidth >> 1)
            if (pTouchEvent.X <= this.mSurfaceWidth >> 1)
            {
                //return this.getFirstCamera();
                return this.FirstCamera;
            }
            else
            {
                //return this.getSecondCamera();
                return this.SecondCamera;
            }
        }

        protected override void ConvertSurfaceToSceneTouchEvent(/* final */ Camera pCamera, /* final */ TouchEvent pSurfaceTouchEvent)
        {
            /* final */
            int surfaceWidthHalf = this.mSurfaceWidth >> 1;

            //if (pCamera == this.getFirstCamera())
            if (pCamera == this.FirstCamera)
            {
                pCamera.ConvertSurfaceToSceneTouchEvent(pSurfaceTouchEvent, surfaceWidthHalf, this.mSurfaceHeight);
            }
            else
            {
                pSurfaceTouchEvent.Offset(-surfaceWidthHalf, 0);
                pCamera.ConvertSurfaceToSceneTouchEvent(pSurfaceTouchEvent, surfaceWidthHalf, this.mSurfaceHeight);
            }
        }

        protected override void UpdateUpdateHandlers(/* final */ float pSecondsElapsed)
        {
            //super.updateUpdateHandlers(pSecondsElapsed);
            base.UpdateUpdateHandlers(pSecondsElapsed);
            //this.getSecondCamera().onUpdate(pSecondsElapsed);
            this.SecondCamera.OnUpdate(pSecondsElapsed);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}