namespace andengine.opengl.texture.builder
{

    //import java.util.ArrayList;
    using System.Collections.Generic;

    using BuildableTexture = andengine.opengl.texture.BuildableTexture;
    using TextureSourceWithLocationCallback = andengine.opengl.texture.BuildableTexture.TextureSourceWithWithLocationCallback;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 15:59:14 - 12.08.2010
     */
    public interface ITextureBuilder
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        //public void pack(final BuildableTexture pBuildableTexture, final ArrayList<TextureSourceWithWithLocationCallback> pTextureSourcesWithLocationCallback) throws TextureSourcePackingException;
        void Pack(BuildableTexture pBuildableTexture, List<TextureSourceWithLocationCallback> pTextureSourcesWithLocationCallback);

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        /*
        public static class TextureSourcePackingException extends Exception {
            // ===========================================================
            // Constants
            // ===========================================================

            private static final long serialVersionUID = 4700734424214372671L;

            // ===========================================================
            // Fields
            // ===========================================================

            // ===========================================================
            // Constructors
            // ===========================================================

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
        }
        */
    }

    // NB: Moved from inside interface
    public /* static */ class TextureSourcePackingException : Exception
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ readonly long serialVersionUID = 4700734424214372671L;

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

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
    }

}