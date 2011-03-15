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
    //using Java.Lang;
    using String = System.String;

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
            decodeOptions.InJustDecodeBounds = true;

            //InputStream input = null;
            System.IO.Stream input = null;
            try
            {
                //input = pContext.getAssets().open(pAssetPath);
                input = pContext.Assets.Open(pAssetPath);
                BitmapFactory.DecodeStream(input, null, decodeOptions);
            }
            catch (IOException e)
            {
                Debug.e("Failed loading Bitmap in AssetTextureSource. AssetPath: " + pAssetPath, e);
            }
            finally
            {
                StreamUtils.CloseStream(input);
            }

            this.mWidth = decodeOptions.OutWidth;
            this.mHeight = decodeOptions.OutHeight;
        }

        AssetTextureSource(Context pContext, String pAssetPath, int pWidth, int pHeight)
        {
            this.mContext = pContext;
            this.mAssetPath = pAssetPath;
            this.mWidth = pWidth;
            this.mHeight = pHeight;
        }

        ITextureSource ITextureSource.Clone() { return (ITextureSource)this.Clone(); }
        public virtual ITextureSource CloneCore()
        {
            return (ITextureSource)Clone();
        }
        public new virtual AssetTextureSource Clone()
        {
            return new AssetTextureSource(this.mContext, this.mAssetPath, this.mWidth, this.mHeight);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public int Height { get { return GetHeight(); } }
        public int Width { get { return GetWidth(); } }

        public /* override */ virtual int GetHeight()
        {
            return this.mHeight;
        }

        public /* override */ virtual int GetWidth()
        {
            return this.mWidth;
        }

        public /* override */ virtual Bitmap OnLoadBitmap()
        {
            System.IO.Stream input = null;
            //InputStream input = null;
            try
            {
                BitmapFactory.Options decodeOptions = new BitmapFactory.Options();
                decodeOptions.InPreferredConfig = Config.Argb8888;

                //input = this.mContext.getAssets().open(this.mAssetPath);
                input = this.mContext.Assets.Open(this.mAssetPath);
                return BitmapFactory.DecodeStream(input, null, decodeOptions);
            }
            catch (IOException e)
            {
                Debug.e("Failed loading Bitmap in AssetTextureSource. AssetPath: " + this.mAssetPath, e);
                return null;
            }
            finally
            {
                //StreamUtils.closeStream(input);
            }
        }

        public override System.String ToString()
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