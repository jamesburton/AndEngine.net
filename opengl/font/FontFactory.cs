namespace andengine.opengl.font
{

    using Texture = andengine.opengl.texture.Texture;

    using Context = Android.Content.Context;
    using Typeface = Android.Graphics.Typeface;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 17:17:28 - 16.06.2010
     */
    public class FontFactory
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static String sAssetBasePath = "";

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        /**
         * @param pAssetBasePath must end with '<code>/</code>' or have <code>.length() == 0</code>.
         */
        public static void setAssetBasePath(String pAssetBasePath)
        {
            if (pAssetBasePath.EndsWith("/") || pAssetBasePath.Length() == 0)
            {
                FontFactory.sAssetBasePath = pAssetBasePath;
            }
            else
            {
                throw new IllegalStateException("pAssetBasePath must end with '/' or be lenght zero.");
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static Font create(Texture pTexture, Typeface pTypeface, float pSize, bool pAntiAlias, int pColor)
        {
            return new Font(pTexture, pTypeface, pSize, pAntiAlias, pColor);
        }

        public static StrokeFont createStroke(Texture pTexture, Typeface pTypeface, float pSize, bool pAntiAlias, int pColor, int pStrokeWidth, int pStrokeColor)
        {
            return new StrokeFont(pTexture, pTypeface, pSize, pAntiAlias, pColor, pStrokeWidth, pStrokeColor);
        }

        public static StrokeFont createStroke(Texture pTexture, Typeface pTypeface, float pSize, bool pAntiAlias, int pColor, int pStrokeWidth, int pStrokeColor, bool pStrokeOnly)
        {
            return new StrokeFont(pTexture, pTypeface, pSize, pAntiAlias, pColor, pStrokeWidth, pStrokeColor, pStrokeOnly);
        }

        public static Font createFromAsset(Texture pTexture, Context pContext, String pAssetPath, float pSize, bool pAntiAlias, int pColor)
        {
            return new Font(pTexture, Typeface.CreateFromAsset(pContext.getAssets(), FontFactory.sAssetBasePath + pAssetPath), pSize, pAntiAlias, pColor);
        }

        public static StrokeFont createStrokeFromAsset(Texture pTexture, Context pContext, String pAssetPath, float pSize, bool pAntiAlias, int pColor, int pStrokeWidth, int pStrokeColor)
        {
            return new StrokeFont(pTexture, Typeface.CreateFromAsset(pContext.getAssets(), FontFactory.sAssetBasePath + pAssetPath), pSize, pAntiAlias, pColor, pStrokeWidth, pStrokeColor);
        }

        public static StrokeFont createStrokeFromAsset(Texture pTexture, Context pContext, String pAssetPath, float pSize, bool pAntiAlias, int pColor, int pStrokeWidth, int pStrokeColor, bool pStrokeOnly)
        {
            return new StrokeFont(pTexture, Typeface.createFromAsset(pContext.getAssets(), FontFactory.sAssetBasePath + pAssetPath), pSize, pAntiAlias, pColor, pStrokeWidth, pStrokeColor, pStrokeOnly);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}