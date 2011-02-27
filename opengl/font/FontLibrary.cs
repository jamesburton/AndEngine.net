namespace andengine.opengl.font
{

    //using Library = andengine.util.Library;

    //import android.util.SparseArray;

    using System.Collections.Generic;
    /**
     * @author Nicolas Gramlich
     * @since 11:52:26 - 20.08.2010
     */
    //public class FontLibrary : Library<Font>
    public class FontLibrary : andengine.util.Library<Font>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public FontLibrary()
            : base()
        {
        }

        public FontLibrary(int pInitialCapacity)
            : base(pInitialCapacity)
        {
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

        void loadFonts(FontManager pFontManager)
        {
            SparseArray<Font> items = this.mItems;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                pFontManager.loadFont(items[i]);
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}