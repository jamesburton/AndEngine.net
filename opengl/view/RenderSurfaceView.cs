namespace andengine.opengl.view
{

    //import javax.microedition.khronos.egl.EGLConfig;
    using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;
    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using Engine = andengine.engine.Engine;
    using GLHelper = andengine.opengl.util.GLHelper;
    using Debug = andengine.util.Debug;

    using Context = Android.Content.Context;
    using AttributeSet = Android.Util.IAttributeSet;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 11:57:29 - 08.03.2010
     */
    public class RenderSurfaceView : GLSurfaceView
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private Renderer mRenderer;

        // ===========================================================
        // Constructors
        // ===========================================================

        public RenderSurfaceView(Context pContext)
            : base(pContext)
        {
        }

        public RenderSurfaceView(Context pContext, AttributeSet pAttrs)
            : base(pContext, pAttrs)
        {
        }

        public void SetRenderer(Engine pEngine)
        {
            this.SetOnTouchListener(pEngine);
            this.mRenderer = new Renderer(pEngine);
            this.SetRenderer(this.mRenderer);
        }

        /**         
         * @see android.view.View#measure(int, int)
         */
        protected override void OnMeasure(int pWidthMeasureSpec, int pHeightMeasureSpec)
        {
            //this.mRenderer.mEngine.getEngineOptions().getResolutionPolicy().onMeasure(this, pWidthMeasureSpec, pHeightMeasureSpec);
            this.mRenderer.mEngine.EngineOptions.ResolutionPolicy.OnMeasure(this, pWidthMeasureSpec, pHeightMeasureSpec);
        }

        public void SetMeasuredDimensionProxy(int pMeasuredWidth, int pMeasuredHeight)
        {
            this.SetMeasuredDimension(pMeasuredWidth, pMeasuredHeight);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        /**
         * @author Nicolas Gramlich
         * @since 11:45:59 - 08.03.2010
         */
        public /* static */ new class Renderer : GLSurfaceView.Renderer
        {
            public static Renderer Instance;
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            //private readonly Engine mEngine;
            public readonly Engine mEngine;

            // ===========================================================
            // Constructors
            // ===========================================================

            public Renderer(Engine pEngine)
            {
                Renderer.Instance = this;

                this.mEngine = pEngine;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            public /* override */ void OnSurfaceChanged(GL10 pGL, int pWidth, int pHeight)
            {
                Debug.D("onSurfaceChanged: pWidth=" + pWidth + "  pHeight=" + pHeight);
                this.mEngine.SetSurfaceSize(pWidth, pHeight);
                pGL.GlViewport(0, 0, pWidth, pHeight);
                pGL.GlLoadIdentity();
            }

            public /* override */ void OnSurfaceCreated(GL10 pGL, EGLConfig pConfig)
            {
                Debug.D("onSurfaceCreated");
                GLHelper.Reset(pGL);

                GLHelper.SetPerspectiveCorrectionHintFastest(pGL);
                //			pGL.glEnable(GL10.GL_POLYGON_SMOOTH);
                //			pGL.glHint(GL10.GL_POLYGON_SMOOTH_HINT, GL10.GL_NICEST);
                //			pGL.glEnable(GL10.GL_LINE_SMOOTH);
                //			pGL.glHint(GL10.GL_LINE_SMOOTH_HINT, GL10.GL_NICEST);
                //			pGL.glEnable(GL10.GL_POINT_SMOOTH);
                //			pGL.glHint(GL10.GL_POINT_SMOOTH_HINT, GL10.GL_NICEST);

                GLHelper.SetShadeModelFlat(pGL);

                GLHelper.DisableLightning(pGL);
                GLHelper.DisableDither(pGL);
                GLHelper.DisableDepthTest(pGL);
                GLHelper.DisableMultisample(pGL);

                GLHelper.EnableBlend(pGL);
                GLHelper.EnableTextures(pGL);
                GLHelper.EnableTexCoordArray(pGL);
                GLHelper.EnableVertexArray(pGL);

                GLHelper.EnableCulling(pGL);
                pGL.GlFrontFace(GL10Consts.GlCcw);
                pGL.GlCullFace(GL10Consts.GlBack);

               // GLHelper.EnableExtensions(pGL, this.mEngine.getEngineOptions().getRenderOptions());
                GLHelper.EnableExtensions(pGL, this.mEngine.EngineOptions.RenderOptions);
            }

            public /* override */ void OnDrawFrame(GL10 pGL)
            {
                try
                {
                    this.mEngine.OnDrawFrame(pGL);
                }
                catch (InterruptedException e)
                {
                    Debug.E("GLThread interrupted!", e);
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
}