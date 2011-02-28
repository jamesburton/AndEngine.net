namespace andengine.opengl.texture.source
{

    using IOException = Java.IO.IOException;
    using InputStream = Java.IO.InputStream;

    using Debug = andengine.util.Debug;
    using StreamUtils = andengine.util.StreamUtils;

    using Context = Android.Content.Context;
    using Bitmap = Android.Graphics.Bitmap;
    using BitmapFactory = Android.Graphics.BitmapFactory;
    using Config = Android.Graphics.Bitmap.Config;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 12:07:52 - 09.03.2010
     */
    public class AssetTextureSource : ITextureSource
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly int mWidth;
        private readonly int mHeight;

        private readonly String mAssetPath;
        private readonly Context mContext;

        // ===========================================================
        // Constructors
        // ===========================================================

        public AssetTextureSource(Context pContext, String pAssetPath)
        {
            this.mContext = pContext;
            this.mAssetPath = pAssetPath;

            BitmapFactory.Options decodeOptions = new BitmapFactory.Options();
            decodeOptions.inJustDecodeBounds = true;

            InputStream input = null;
            try
            {
                input = pContext.getAssets().open(pAssetPath);
                BitmapFactory.decodeStream(input, null, decodeOptions);
            }
            catch (IOException e)
            {
                Debug.e("Failed loading Bitmap in AssetTextureSource. AssetPath: " + pAssetPath, e);
            }
            finally
            {
                StreamUtils.closeStream(input);
            }

            this.mWidth = decodeOptions.outWidth;
            this.mHeight = decodeOptions.outHeight;
        }

        AssetTextureSource(Context pContext, String pAssetPath, int pWidth, int pHeight)
        {
            this.mContext = pContext;
            this.mAssetPath = pAssetPath;
            this.mWidth = pWidth;
            this.mHeight = pHeight;
        }

        public override AssetTextureSource clone()
        {
            return new AssetTextureSource(this.mContext, this.mAssetPath, this.mWidth, this.mHeight);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public int Height { get { return getHeight(); } }
        public int Width { get { return getWidth(); } }

        public override int getHeight()
        {
            return this.mHeight;
        }

        public override int getWidth()
        {
            return this.mWidth;
        }

        public override Bitmap onLoadBitmap()
        {
            InputStream input = null;
            try
            {
                BitmapFactory.Options decodeOptions = new BitmapFactory.Options();
                decodeOptions.inPreferredConfig = Config.Argb8888;

                input = this.mContext.getAssets().open(this.mAssetPath);
                return BitmapFactory.decodeStream(input, null, decodeOptions);
            }
            catch (IOException e)
            {
                Debug.e("Failed loading Bitmap in AssetTextureSource. AssetPath: " + this.mAssetPath, e);
                return null;
            }
            finally
            {
                StreamUtils.closeStream(input);
            }
        }

        public String toString()
        {
            return this.GetType().Name + "(" + this.mAssetPath + ")";
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}