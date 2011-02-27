namespace andengine.opengl.font
{

    //import java.util.ArrayList;
    using System.Collections.Generic;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    /**
     * @author Nicolas Gramlich
     * @since 17:48:46 - 08.03.2010
     */
    public class FontManager
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //private final ArrayList<Font> mFontsManaged = new ArrayList<Font>();
        private readonly List<Font> mFontsManaged = new List<Font>();

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

        public void clear()
        {
            this.mFontsManaged.Clear();
        }

        public void loadFont(Font pFont)
        {
            this.mFontsManaged.Add(pFont);
        }

        public void loadFonts(FontLibrary pFontLibrary)
        {
            pFontLibrary.loadFonts(this);
        }

        //public void loadFonts(final Font ... pFonts) {
        public void loadFonts(params Font[] pFonts)
        {
            for (int i = pFonts.Length - 1; i >= 0; i--)
            {
                this.loadFont(pFonts[i]);
            }
        }

        public void updateFonts(GL10 pGL)
        {
            //final ArrayList<Font> fonts = this.mFontsManaged;
            List<Font> fonts = this.mFontsManaged;
            int fontCount = fonts.Count;
            if (fontCount > 0)
            {
                for (int i = fontCount - 1; i >= 0; i--)
                {
                    fonts[i].update(pGL);
                }
            }
        }

        public void reloadFonts()
        {
            //final ArrayList<Font> managedFonts = this.mFontsManaged;
            List<Font> managedFonts = this.mFontsManaged;
            for (int i = managedFonts.Count - 1; i >= 0; i--)
            {
                managedFonts[i].reload();
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}