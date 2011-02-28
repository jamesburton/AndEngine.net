namespace andengine.opengl.texture.region
{

    using BuildableTexture = andengine.opengl.texture.BuildableTexture;
    using Texture = andengine.opengl.texture.Texture;
    using TextureSourceWithLocation = andengine.opengl.texture.Texture.TextureSourceWithLocation;
    using AssetTextureSource = andengine.opengl.texture.source.AssetTextureSource;
    using ITextureSource = andengine.opengl.texture.source.ITextureSource;
    using ResourceTextureSource = andengine.opengl.texture.source.ResourceTextureSource;
    //using Callback = andengine.util.Callback;

    using Context = Android.Content.Context;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 18:15:14 - 09.03.2010
     */
    public class TextureRegionFactory
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static String sAssetBasePath = new String("");

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
                TextureRegionFactory.sAssetBasePath = pAssetBasePath;
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

        public static TextureRegion extractFromTexture(Texture pTexture, int pTexturePositionX, int pTexturePositionY, int pWidth, int pHeight)
        {
            return new TextureRegion(pTexture, pTexturePositionX, pTexturePositionY, pWidth, pHeight);
        }

        // ===========================================================
        // Methods using Texture
        // ===========================================================

        public static TextureRegion createFromAsset(Texture pTexture, Context pContext, String pAssetPath, int pTexturePositionX, int pTexturePositionY)
        {
            ITextureSource textureSource = new AssetTextureSource(pContext, TextureRegionFactory.sAssetBasePath + pAssetPath);
            return TextureRegionFactory.createFromSource(pTexture, textureSource, pTexturePositionX, pTexturePositionY);
        }

        public static TiledTextureRegion createTiledFromAsset(Texture pTexture, Context pContext, String pAssetPath, int pTexturePositionX, int pTexturePositionY, int pTileColumns, int pTileRows)
        {
            ITextureSource textureSource = new AssetTextureSource(pContext, TextureRegionFactory.sAssetBasePath + pAssetPath);
            return TextureRegionFactory.createTiledFromSource(pTexture, textureSource, pTexturePositionX, pTexturePositionY, pTileColumns, pTileRows);
        }


        public static TextureRegion createFromResource(Texture pTexture, Context pContext, int pDrawableResourceID, int pTexturePositionX, int pTexturePositionY)
        {
            ITextureSource textureSource = new ResourceTextureSource(pContext, pDrawableResourceID);
            return TextureRegionFactory.createFromSource(pTexture, textureSource, pTexturePositionX, pTexturePositionY);
        }

        public static TiledTextureRegion createTiledFromResource(Texture pTexture, Context pContext, int pDrawableResourceID, int pTexturePositionX, int pTexturePositionY, int pTileColumns, int pTileRows)
        {
            ITextureSource textureSource = new ResourceTextureSource(pContext, pDrawableResourceID);
            return TextureRegionFactory.createTiledFromSource(pTexture, textureSource, pTexturePositionX, pTexturePositionY, pTileColumns, pTileRows);
        }


        public static TextureRegion createFromSource(Texture pTexture, ITextureSource pTextureSource, int pTexturePositionX, int pTexturePositionY)
        {
            TextureRegion textureRegion = new TextureRegion(pTexture, pTexturePositionX, pTexturePositionY, pTextureSource.getWidth(), pTextureSource.getHeight());
            pTexture.addTextureSource(pTextureSource, textureRegion.getTexturePositionX(), textureRegion.getTexturePositionY());
            return textureRegion;
        }

        public static TiledTextureRegion createTiledFromSource(Texture pTexture, ITextureSource pTextureSource, int pTexturePositionX, int pTexturePositionY, int pTileColumns, int pTileRows)
        {
            TiledTextureRegion tiledTextureRegion = new TiledTextureRegion(pTexture, pTexturePositionX, pTexturePositionY, pTextureSource.getWidth(), pTextureSource.getHeight(), pTileColumns, pTileRows);
            pTexture.addTextureSource(pTextureSource, tiledTextureRegion.getTexturePositionX(), tiledTextureRegion.getTexturePositionY());
            return tiledTextureRegion;
        }

        // ===========================================================
        // Methods using BuildableTexture
        // ===========================================================

        public static TextureRegion createFromAsset(BuildableTexture pBuildableTexture, Context pContext, String pAssetPath)
        {
            ITextureSource textureSource = new AssetTextureSource(pContext, TextureRegionFactory.sAssetBasePath + pAssetPath);
            return TextureRegionFactory.createFromSource(pBuildableTexture, textureSource);
        }

        public static TiledTextureRegion createTiledFromAsset(BuildableTexture pBuildableTexture, Context pContext, String pAssetPath, int pTileColumns, int pTileRows)
        {
            ITextureSource textureSource = new AssetTextureSource(pContext, TextureRegionFactory.sAssetBasePath + pAssetPath);
            return TextureRegionFactory.createTiledFromSource(pBuildableTexture, textureSource, pTileColumns, pTileRows);
        }


        public static TextureRegion createFromResource(BuildableTexture pBuildableTexture, Context pContext, int pDrawableResourceID)
        {
            ITextureSource textureSource = new ResourceTextureSource(pContext, pDrawableResourceID);
            return TextureRegionFactory.createFromSource(pBuildableTexture, textureSource);
        }

        public static TiledTextureRegion createTiledFromResource(BuildableTexture pBuildableTexture, Context pContext, int pDrawableResourceID, int pTileColumns, int pTileRows)
        {
            ITextureSource textureSource = new ResourceTextureSource(pContext, pDrawableResourceID);
            return TextureRegionFactory.createTiledFromSource(pBuildableTexture, textureSource, pTileColumns, pTileRows);
        }

        /*
        public static TextureRegion createFromSource(BuildableTexture pBuildableTexture, ITextureSource pTextureSource) {
            TextureRegion textureRegion = new TextureRegion(pBuildableTexture, 0, 0, pTextureSource.getWidth(), pTextureSource.getHeight());
            pBuildableTexture.addTextureSource(pTextureSource, new Callback<TextureSourceWithLocation>() {
                //@Override
                public override void onCallback(final TextureSourceWithLocation pCallbackValue) {
                    textureRegion.setTexturePosition(pCallbackValue.getTexturePositionX(), pCallbackValue.getTexturePositionY());
                }
            });
            return textureRegion;
        }
        */
        /*
        public class TextureRegionCallback : Callback {
            protected TextureRegion _TextureRegion;
            public TextureRegionCallback(TextureRegion textureRegion) { _TextureRegion = textureRegion; }
            public override void onCallback(TextureSourceWithLocation pCallbackValue) {
                _TextureRegion.setTexturePosition(pCallbackValue.getTexturePositionX(), pCallbackValue.getTexturePositionY());
            }
        }
        */
        public class TextureSourceWithLocationCallback : andengine.util.Callback<TextureSourceWithLocation>
        {
            protected TextureSourceWithLocation _TextureSourceWithLocation;
            public TextureSourceWithLocationCallback(TextureSourceWithLocation textureSourceWithLocation) { _TextureSourceWithLocation = textureSourceWithLocation; }
            public override void onCallback(TextureSourceWithLocation pCallbackValue)
            {
                _TextureSourceWithLocation.setTexturePosition(pCallbackValue.getTexturePositionX(), pCallbackValue.getTexturePositionY());
            }
        }
        public static TextureRegion createFromSource(BuildableTexture pBuildableTexture, ITextureSource pTextureSource)
        {
            TextureRegion textureRegion = new TextureRegion(pBuildableTexture, 0, 0, pTextureSource.getWidth(), pTextureSource.getHeight());
            TextureSourceWithLocationCallback callback = new TextureSourceWithLocationCallback((TextureSourceWithLocation)textureRegion);
            pBuildableTexture.addTextureSource(pTextureSource, callback);
            return textureRegion;
        }

        /*
        public static TiledTextureRegion createTiledFromSource(final BuildableTexture pBuildableTexture, final ITextureSource pTextureSource, final int pTileColumns, final int pTileRows) {
            final TiledTextureRegion tiledTextureRegion = new TiledTextureRegion(pBuildableTexture, 0, 0, pTextureSource.getWidth(), pTextureSource.getHeight(), pTileColumns, pTileRows);
            pBuildableTexture.addTextureSource(pTextureSource, new Callback<TextureSourceWithLocation>() {
                @Override
                public void onCallback(final TextureSourceWithLocation pCallbackValue) {
                    tiledTextureRegion.setTexturePosition(pCallbackValue.getTexturePositionX(), pCallbackValue.getTexturePositionY());
                }
            });
            return tiledTextureRegion;
        }
        */
        public static TiledTextureRegion createTiledFromSource(BuildableTexture pBuildableTexture, ITextureSource pTextureSource, int pTileColumns, int pTileRows)
        {
            TiledTextureRegion tiledTextureRegion = new TiledTextureRegion(pBuildableTexture, 0, 0, pTextureSource.getWidth(), pTextureSource.getHeight(), pTileColumns, pTileRows);
            pBuildableTexture.addTextureSource(pTextureSource, new TextureSourceWithLocationCallback((TextureSourceWithLocation)tiledTextureRegion));
            return tiledTextureRegion;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}