namespace andengine.engine.options
{

    /**
     * @author Nicolas Gramlich
     * @since 13:01:40 - 02.07.2010
     */
    public class RenderOptions
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private bool mDisableExtensionVertexBufferObjects = false;

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public RenderOptions DnableExtensionVertexBufferObjects()
        {
            return this.SetDisableExtensionVertexBufferObjects(false);
        }

        public RenderOptions DisableExtensionVertexBufferObjects()
        {
            return this.SetDisableExtensionVertexBufferObjects(true);
        }

        public RenderOptions SetDisableExtensionVertexBufferObjects(/* final */ bool pDisableExtensionVertexBufferObjects)
        {
            this.mDisableExtensionVertexBufferObjects = pDisableExtensionVertexBufferObjects;
            return this;
        }

        public bool DisableExtensionVertexBuffer { get { return IsDisableExtensionVertexBufferObjects(); } set { SetDisableExtensionVertexBufferObjects(value); } }

        /**
         * <u><b>Default:</b></u> <code>false</code>
         */
        public bool IsDisableExtensionVertexBufferObjects()
        {
            return this.mDisableExtensionVertexBufferObjects;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}