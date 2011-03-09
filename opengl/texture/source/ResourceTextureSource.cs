namespace andengine.opengl.texture.source
{

    using Context = Android.Content.Context;
    using Bitmap = Android.Graphics.Bitmap;
    using BitmapFactory = Android.Graphics.BitmapFactory;
    using Config = Android.Graphics.Bitmap.Config;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 15:07:23 - 09.03.2010
     */
    public class ResourceTextureSource : ITextureSource
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly int mWidth;
        private readonly int mHeight;

        private readonly int mDrawableResourceID;
        private readonly Context mContext;

        // ===========================================================
        // Constructors
        // ===========================================================

        public ResourceTextureSource(Context pContext, int pDrawableResourceID)
        {
            this.mContext = pContext;
            this.mDrawableResourceID = pDrawableResourceID;

            BitmapFactory.Options decodeOptions = new BitmapFactory.Options();
            decodeOptions.InJustDecodeBounds = true;
            //		decodeOptions.inScaled = false; // TODO Check how this behaves with drawable-""/nodpi/ldpi/mdpi/hdpi folders

            BitmapFactory.DecodeResource(pContext.Resources, pDrawableResourceID, decodeOptions);

            this.mWidth = decodeOptions.OutWidth;
            this.mHeight = decodeOptions.OutHeight;
        }

        protected ResourceTextureSource(Context pContext, int pDrawableResourceID, int pWidth, int pHeight)
        {
            this.mContext = pContext;
            this.mDrawableResourceID = pDrawableResourceID;
            this.mWidth = pWidth;
            this.mHeight = pHeight;
        }

        ITextureSource ITextureSource.Clone() { return (ITextureSource)this.Clone(); }
        public /* override */ virtual ResourceTextureSource Clone()
        {
            return new ResourceTextureSource(this.mContext, this.mDrawableResourceID, this.mWidth, this.mHeight);
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
            BitmapFactory.Options decodeOptions = new BitmapFactory.Options();
            decodeOptions.InPreferredConfig = Config.Argb8888;
            //		decodeOptions.InScaled = false; // TODO Check how this behaves with drawable-""/nodpi/ldpi/mdpi/hdpi folders
            return BitmapFactory.DecodeResource(this.mContext.Resources, this.mDrawableResourceID, decodeOptions);
        }

        //public override String toString()
        public override System.String ToString()
        {
            //return this.getClass().getSimpleName() + "(" + this.mDrawableResourceID + ")";
            //return new Java.Lang.String(this.GetType().Name + "(" + this.mDrawableResourceID.ToString() + ")");
            return this.GetType().Name + "(" + this.mDrawableResourceID.ToString() + ")";
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}