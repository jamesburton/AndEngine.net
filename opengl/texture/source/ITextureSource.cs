namespace andengine.opengl.texture.source
{

    using Bitmap = Android.Graphics.Bitmap;

    /**
     * @author Nicolas Gramlich
     * @since 12:08:52 - 09.03.2010
     */
    // TODO: Check about Clonable stub usage or suitable replacement
    public interface ITextureSource : Cloneable /* Have created a stub for this in the root */
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        int GetWidth();
        int GetHeight();

        ITextureSource CloneCore();
        ITextureSource Clone();

        Bitmap OnLoadBitmap();
    }
}