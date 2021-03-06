namespace andengine.opengl.view
{

    using GL = Javax.Microedition.Khronos.Opengles.IGL;

    /**
     * An interface used to wrap a GL interface.
     * <p>
     * Typically used for implementing debugging and tracing on top of the default
     * GL interface. You would typically use this by creating your own class that
     * implemented all the GL methods by delegating to another GL instance. Then you
     * could add your own behavior before or after calling the delegate. All the
     * GLWrapper would do was instantiate and return the wrapper GL instance:
     * 
     * <pre class="prettyprint">
     * class MyGLWrapper : GLWrapper {
     *     GL wrap(GL gl) {
     *         return new MyGLImplementation(gl);
     *     }
     *     static class MyGLImplementation : GL,GL10,GL11,... {
     *         ...
     *     }
     * }
     * </pre>
     * 
     * @see #setGLWrapper(GLWrapper)
     *
     * @author Nicolas Gramlich
     * @since 20:53:38 - 28.06.2010
     */
    public interface GLWrapper
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /**
         * Wraps a gl interface in another gl interface.
         * 
         * @param pGL a GL interface that is to be wrapped.
         * @return either the input argument or another GL object that wraps the
         *         input argument.
         */
        GL wrap(GL pGL);
    }
}