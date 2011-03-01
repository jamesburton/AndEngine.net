/**
 * 
 */
namespace andengine.opengl.view
{

    using EGL10 = Javax.Microedition.Khronos.Egl.IEGL10;
    using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;
    using EGLDisplay = Javax.Microedition.Khronos.Egl.EGLDisplay;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 20:42:29 - 28.06.2010
     */
    public abstract class BaseConfigChooser : EGLConfigChooser
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected readonly int[] mConfigSpec;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseConfigChooser(int[] pConfigSpec)
        {
            this.mConfigSpec = pConfigSpec;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public abstract EGLConfig chooseConfig(EGL10 pEGL, EGLDisplay pEGLDisplay, EGLConfig[] pEGLConfigs);

        public EGLConfig chooseConfig(EGL10 pEGL, EGLDisplay pEGLDisplay)
        {
            int[] num_config = new int[1];
            pEGL.EglChooseConfig(pEGLDisplay, this.mConfigSpec, null, 0, num_config);

            int numConfigs = num_config[0];

            if (numConfigs <= 0)
            {
                throw new IllegalArgumentException("No configs match configSpec");
            }

            EGLConfig[] configs = new EGLConfig[numConfigs];
            pEGL.EglChooseConfig(pEGLDisplay, this.mConfigSpec, configs, numConfigs, num_config);
            EGLConfig config = this.chooseConfig(pEGL, pEGLDisplay, configs);
            if (config == null)
            {
                throw new IllegalArgumentException("No config chosen");
            }
            return config;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}