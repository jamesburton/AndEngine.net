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
    using AttributeSet = Android.Util.AttributeSet;
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

        public void setRenderer(Engine pEngine)
        {
            this.setOnTouchListener(pEngine);
            this.mRenderer = new Renderer(pEngine);
            this.setRenderer(this.mRenderer);
        }

        /**
         * @see android.view.View#measure(int, int)
         */
        protected override void onMeasure(int pWidthMeasureSpec, int pHeightMeasureSpec)
        {
            this.mRenderer.mEngine.getEngineOptions().getResolutionPolicy().onMeasure(this, pWidthMeasureSpec, pHeightMeasureSpec);
        }

        public void setMeasuredDimensionProxy(int pMeasuredWidth, int pMeasuredHeight)
        {
            this.setMeasuredDimension(pMeasuredWidth, pMeasuredHeight);
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
        public static class Renderer : GLSurfaceView.Renderer
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            private readonly Engine mEngine;

            // ===========================================================
            // Constructors
            // ===========================================================

            public Renderer(Engine pEngine)
            {
                this.mEngine = pEngine;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            public override void onSurfaceChanged(GL10 pGL, int pWidth, int pHeight)
            {
                Debug.d("onSurfaceChanged: pWidth=" + pWidth + "  pHeight=" + pHeight);
                this.mEngine.setSurfaceSize(pWidth, pHeight);
                pGL.GlViewport(0, 0, pWidth, pHeight);
                pGL.GlLoadIdentity();
            }

            public override void onSurfaceCreated(GL10 pGL, EGLConfig pConfig)
            {
                Debug.d("onSurfaceCreated");
                GLHelper.reset(pGL);

                GLHelper.setPerspectiveCorrectionHintFastest(pGL);
                //			pGL.glEnable(GL10.GL_POLYGON_SMOOTH);
                //			pGL.glHint(GL10.GL_POLYGON_SMOOTH_HINT, GL10.GL_NICEST);
                //			pGL.glEnable(GL10.GL_LINE_SMOOTH);
                //			pGL.glHint(GL10.GL_LINE_SMOOTH_HINT, GL10.GL_NICEST);
                //			pGL.glEnable(GL10.GL_POINT_SMOOTH);
                //			pGL.glHint(GL10.GL_POINT_SMOOTH_HINT, GL10.GL_NICEST);

                GLHelper.setShadeModelFlat(pGL);

                GLHelper.disableLightning(pGL);
                GLHelper.disableDither(pGL);
                GLHelper.disableDepthTest(pGL);
                GLHelper.disableMultisample(pGL);

                GLHelper.enableBlend(pGL);
                GLHelper.enableTextures(pGL);
                GLHelper.enableTexCoordArray(pGL);
                GLHelper.enableVertexArray(pGL);

                GLHelper.enableCulling(pGL);
                pGL.GlFrontFace(GL10Consts.GlCcw);
                pGL.GlCullFace(GL10Consts.GlBack);

                GLHelper.enableExtensions(pGL, this.mEngine.getEngineOptions().getRenderOptions());
            }

            public override void onDrawFrame(GL10 pGL)
            {
                try
                {
                    this.mEngine.onDrawFrame(pGL);
                }
                catch (InterruptedException e)
                {
                    Debug.e("GLThread interrupted!", e);
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