namespace andengine.opengl.view
{

    using EGL10 = Javax.Microedition.Khronos.Egl.IEGL10;
    using EGL10Consts = Javax.Microedition.Khronos.Egl.EGL10Consts;
    using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;
    using EGLDisplay = Javax.Microedition.Khronos.Egl.EGLDisplay;

    /**
     * An interface for choosing an EGLConfig configuration from a list of
     * potential configurations.
     * <p>
     * This interface must be implemented by clients wishing to call
     * {@link GLSurfaceView#setEGLConfigChooser(EGLConfigChooser)}
     *
     * @author Nicolas Gramlich
     * @since 20:53:49 - 28.06.2010
     */
    public interface EGLConfigChooser
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /**
         * Choose a configuration from the list. Implementors typically
         * implement this method by calling {@link EGL10#eglChooseConfig} and
         * iterating through the results. Please consult the EGL specification
         * available from The Khronos Group to learn how to call
         * eglChooseConfig.
         * 
         * @param pEGL the EGL10 for the current display.
         * @param pEGLDisplay the current display.
         * @return the chosen configuration.
         */
        EGLConfig chooseConfig(EGL10 pEGL, EGLDisplay pEGLDisplay);
    }
}