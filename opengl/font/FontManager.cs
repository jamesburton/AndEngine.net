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

        public void Clear()
        {
            this.mFontsManaged.Clear();
        }

        public void LoadFont(Font pFont)
        {
            this.mFontsManaged.Add(pFont);
        }

        public void LoadFonts(FontLibrary pFontLibrary)
        {
            pFontLibrary.LoadFonts(this);
        }

        //public void loadFonts(final Font ... pFonts) {
        public void LoadFonts(params Font[] pFonts)
        {
            for (int i = pFonts.Length - 1; i >= 0; i--)
            {
                this.LoadFont(pFonts[i]);
            }
        }

        public void UpdateFonts(GL10 pGL)
        {
            //final ArrayList<Font> fonts = this.mFontsManaged;
            List<Font> fonts = this.mFontsManaged;
            int fontCount = fonts.Count;
            if (fontCount > 0)
            {
                for (int i = fontCount - 1; i >= 0; i--)
                {
                    fonts[i].Update(pGL);
                }
            }
        }

        public void ReloadFonts()
        {
            //final ArrayList<Font> managedFonts = this.mFontsManaged;
            List<Font> managedFonts = this.mFontsManaged;
            for (int i = managedFonts.Count - 1; i >= 0; i--)
            {
                managedFonts[i].Reload();
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}