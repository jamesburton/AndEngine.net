namespace andengine.opengl.view
{

    using EGL10 = Javax.Microedition.Khronos.Egl.IEGL10;
    using EGL10Consts = Javax.Microedition.Khronos.Egl.EGL10Consts;
    using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;
    using EGLDisplay = Javax.Microedition.Khronos.Egl.EGLDisplay;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 20:54:06 - 28.06.2010
     */
    public class ComponentSizeChooser : BaseConfigChooser
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly int[] mValue;
        // Subclasses can adjust these values:
        protected int mRedSize;
        protected int mGreenSize;
        protected int mBlueSize;
        protected int mAlphaSize;
        protected int mDepthSize;
        protected int mStencilSize;

        // ===========================================================
        // Constructors
        // ===========================================================

        public ComponentSizeChooser(int pRedSize, int pGreenSize, int pBlueSize, int pAlphaSize, int pDepthSize, int pStencilSize)
            : base(new int[] { EGL10Consts.EglRedSize, pRedSize, EGL10Consts.EglGreenSize, pGreenSize, EGL10Consts.EglBlueSize, pBlueSize, EGL10Consts.EglAlphaSize, pAlphaSize, EGL10Consts.EglDepthSize, pDepthSize, EGL10Consts.EglStencilSize, pStencilSize, EGL10Consts.EglNone })
        {
            this.mValue = new int[1];
            this.mRedSize = pRedSize;
            this.mGreenSize = pGreenSize;
            this.mBlueSize = pBlueSize;
            this.mAlphaSize = pAlphaSize;
            this.mDepthSize = pDepthSize;
            this.mStencilSize = pStencilSize;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override EGLConfig chooseConfig(EGL10 pEGL, EGLDisplay pEGLDisplay, EGLConfig[] pEGLConfigs)
        {
            EGLConfig closestConfig = null;
            int closestDistance = 1000;
            //for(final EGLConfig config : pEGLConfigs) {
            foreach (EGLConfig config in pEGLConfigs)
            {
                int r = this.findConfigAttrib(pEGL, pEGLDisplay, config, EGL10Consts.EglRedSize, 0);
                int g = this.findConfigAttrib(pEGL, pEGLDisplay, config, EGL10Consts.EglGreenSize, 0);
                int b = this.findConfigAttrib(pEGL, pEGLDisplay, config, EGL10Consts.EglBlueSize, 0);
                int a = this.findConfigAttrib(pEGL, pEGLDisplay, config, EGL10Consts.EglAlphaSize, 0);
                int d = this.findConfigAttrib(pEGL, pEGLDisplay, config, EGL10Consts.EglDepthSize, 0);
                int s = this.findConfigAttrib(pEGL, pEGLDisplay, config, EGL10Consts.EglStencilSize, 0);
                int distance = Math.Abs(r - this.mRedSize) + Math.Abs(g - this.mGreenSize) + Math.Abs(b - this.mBlueSize) + Math.Abs(a - this.mAlphaSize) + Math.Abs(d - this.mDepthSize) + Math.Abs(s - this.mStencilSize);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestConfig = config;
                }
            }
            return closestConfig;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        private int findConfigAttrib(EGL10 pEGL, EGLDisplay pEGLDisplay, EGLConfig pEGLConfig, int pAttribute, int pDefaultValue)
        {
            if (pEGL.EglGetConfigAttrib(pEGLDisplay, pEGLConfig, pAttribute, this.mValue))
            {
                return this.mValue[0];
            }
            return pDefaultValue;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}