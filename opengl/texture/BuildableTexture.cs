namespace andengine.opengl.texture
{

    //import java.util.ArrayList;

    using ITextureBuilder = andengine.opengl.texture.builder.ITextureBuilder;
    using TextureSourcePackingException = andengine.opengl.texture.builder./*ITextureBuilder.*/TextureSourcePackingException;
    using ITextureSource = andengine.opengl.texture.source.ITextureSource;
    //using Callback = andengine.util.Callback;

    using Bitmap = Android.Graphics.Bitmap;
    using System.Collections.Generic;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 21:26:38 - 12.08.2010
     */
    public class BuildableTexture : Texture
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //private final ArrayList<TextureSourceWithWithLocationCallback> mTextureSourcesToPlace = new ArrayList<TextureSourceWithWithLocationCallback>();
        private readonly List<TextureSourceWithWithLocationCallback> mTextureSourcesToPlace = new List<TextureSourceWithWithLocationCallback>();

        // ===========================================================
        // Constructors
        // ===========================================================

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         */
        public BuildableTexture(int pWidth, int pHeight)
            : base(pWidth, pHeight, TextureOptions.DEFAULT, null)
        { }

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pTextureStateListener to be informed when this {@link BuildableTexture} is loaded, unloaded or a {@link ITextureSource} failed to load.
         */
        public BuildableTexture(int pWidth, int pHeight, ITextureStateListener pTextureStateListener)
            : base(pWidth, pHeight, TextureOptions.DEFAULT, pTextureStateListener)
        { }

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pTextureOptions the (quality) settings of the Texture.
         */
        public BuildableTexture(int pWidth, int pHeight, TextureOptions pTextureOptions) /* throws IllegalArgumentException */
            : base(pWidth, pHeight, pTextureOptions, null)
        {
        }

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pTextureOptions the (quality) settings of the Texture.
         * @param pTextureStateListener to be informed when this {@link BuildableTexture} is loaded, unloaded or a {@link ITextureSource} failed to load.
         */
        public BuildableTexture(int pWidth, int pHeight, TextureOptions pTextureOptions, ITextureStateListener pTextureStateListener) /* throws IllegalArgumentException */
            : base(pWidth, pHeight, pTextureOptions, pTextureStateListener)
        { }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        /**
         * Most likely this is not the method you'd want to be using, as the {@link ITextureSource} won't get packed through this.
         * Use {@link BuildableTexture#addTextureSource(ITextureSource)} instead.
         */
        /// @Deprecated
        public TextureSourceWithLocation addTextureSource(ITextureSource pTextureSource, int pTexturePositionX, int pTexturePositionY)
        {
            return base.addTextureSource(pTextureSource, pTexturePositionX, pTexturePositionY);
        }

        public override void clearTextureSources()
        {
            base.clearTextureSources();
            this.mTextureSourcesToPlace.clear();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        /**
         * When all {@link ITextureSource}s are added you have to call {@link BuildableTexture#build(ITextureBuilder)}.
         * @param pTextureSource to be added.
         * @param pTextureRegion
         */
        public void addTextureSource(ITextureSource pTextureSource, andengine.util.Callback<TextureSourceWithLocation> pCallback)
        {
            this.mTextureSourcesToPlace.Add(new TextureSourceWithWithLocationCallback(pTextureSource, pCallback));
        }

        /**
         * Removes a {@link ITextureSource} before {@link BuildableTexture#build(ITextureBuilder)} is called.
         * @param pTextureSource to be removed.
         */
        public void removeTextureSource(ITextureSource pTextureSource)
        {
            //final ArrayList<TextureSourceWithWithLocationCallback> textureSources = this.mTextureSourcesToPlace;
            List<TextureSourceWithWithLocationCallback> textureSources = this.mTextureSourcesToPlace;
            for (int i = textureSources.Count - 1; i >= 0; i--)
            {
                TextureSourceWithWithLocationCallback textureSource = textureSources[i];
                if (textureSource.mTextureSource == pTextureSource)
                {
                    textureSources.Remove(i);
                    this.mUpdateOnHardwareNeeded = true;
                    return;
                }
            }
        }

        /**
         * May draw over already added {@link ITextureSource}s.
         * 
         * @param pTextureSourcePackingAlgorithm the {@link ITextureBuilder} to use for packing the {@link ITextureSource} in this {@link BuildableTexture}.
         * @throws TextureSourcePackingException i.e. when the {@link ITextureSource}s didn't fit into this {@link BuildableTexture}.
         */
        public void build(ITextureBuilder pTextureSourcePackingAlgorithm) /* throws TextureSourcePackingException */ {
            pTextureSourcePackingAlgorithm.pack(this, this.mTextureSourcesToPlace);
            this.mTextureSourcesToPlace.Clear();
            this.mUpdateOnHardwareNeeded = true;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        public /* static */ class TextureSourceWithWithLocationCallback : ITextureSource
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            public /* final */ readonly ITextureSource mTextureSource;
            protected /* final */ readonly andengine.util.Callback<TextureSourceWithLocation> mCallback;

            // ===========================================================
            // Constructors
            // ===========================================================

            public TextureSourceWithWithLocationCallback(ITextureSource pTextureSource, andengine.util.Callback<TextureSourceWithLocation> pCallback)
            {
                this.mTextureSource = pTextureSource;
                mCallback = pCallback;
            }

            public override TextureSourceWithWithLocationCallback clone()
            {
                return null;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            public andengine.util.Callback<TextureSourceWithLocation> getCallback()
            {
                return this.mCallback;
            }

            public ITextureSource getTextureSource()
            {
                return this.mTextureSource;
            }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            public int Width { get { return getWidth(); } }

            public override int getWidth()
            {
                return this.mTextureSource.getWidth();
            }

            public int Height { get { return getHeight(); } }

            public override int getHeight()
            {
                return this.mTextureSource.getHeight();
            }

            public override Bitmap onLoadBitmap()
            {
                return this.mTextureSource.onLoadBitmap();
            }

            public override String ToString()
            {
                return this.mTextureSource.ToString();
            }

            // ===========================================================
            // Methods
            // ===========================================================

            // ===========================================================
            // Inner and Anonymous Classes
            // ===========================================================
        }
    }
}