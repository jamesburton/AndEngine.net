namespace andengine.opengl.texture
{

    //import java.util.ArrayList;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using ITextureSource = andengine.opengl.texture.source.ITextureSource;
    using GLHelper = andengine.opengl.util.GLHelper;
    using Debug = andengine.util.Debug;
    using MathUtils = andengine.util.MathUtils;

    using Bitmap = Android.Graphics.Bitmap;
    using GLUtils = Android.Opengl.GLUtils;
    using System.Collections.Generic;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 14:55:02 - 08.03.2010
     */
    public class Texture
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static readonly int[] HARDWARETEXTUREID_FETCHER = new int[1];

        // ===========================================================
        // Fields
        // ===========================================================

        private /* readonly */ int mWidth;
        private /* readonly */ int mHeight;

        private bool mLoadedToHardware;
        private int mHardwareTextureID = -1;
        private readonly TextureOptions mTextureOptions;

        //private final ArrayList<TextureSourceWithLocation> mTextureSources = new ArrayList<TextureSourceWithLocation>();
        private readonly List<TextureSourceWithLocation> mTextureSources = new List<TextureSourceWithLocation>();

        private readonly ITextureStateListener mTextureStateListener;

        protected bool mUpdateOnHardwareNeeded = false;

        // ===========================================================
        // Constructors
        // ===========================================================

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         */
        public Texture(int pWidth, int pHeight)
        {
            Init(pWidth, pHeight, TextureOptions.DEFAULT, null);
        }

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pTextureStateListener to be informed when this {@link Texture} is loaded, unloaded or a {@link ITextureSource} failed to load.
         */
        public Texture(int pWidth, int pHeight, ITextureStateListener pTextureStateListener)
        {
            Init(pWidth, pHeight, TextureOptions.DEFAULT, pTextureStateListener);
        }

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pTextureOptions the (quality) settings of the Texture.
         */
        public Texture(int pWidth, int pHeight, TextureOptions pTextureOptions) /* throws IllegalArgumentException */ {
            Init(pWidth, pHeight, pTextureOptions, null);
        }

        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pTextureOptions the (quality) settings of the Texture.
         * @param pTextureStateListener to be informed when this {@link Texture} is loaded, unloaded or a {@link ITextureSource} failed to load.
         */
        public Texture(int pWidth, int pHeight, TextureOptions pTextureOptions, ITextureStateListener pTextureStateListener) /* throws IllegalArgumentException */ {
            Init(pWidth, pHeight, pTextureOptions, pTextureStateListener);
        }
        /**
         * @param pWidth must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pHeight must be a power of 2 (i.e. 32, 64, 128, 256, 512, 1024).
         * @param pTextureOptions the (quality) settings of the Texture.
         * @param pTextureStateListener to be informed when this {@link Texture} is loaded, unloaded or a {@link ITextureSource} failed to load.
         */
        protected void Init(int pWidth, int pHeight, TextureOptions pTextureOptions, ITextureStateListener pTextureStateListener) /* throws IllegalArgumentException */ {
            if (!MathUtils.isPowerOfTwo(pWidth) || !MathUtils.isPowerOfTwo(pHeight))
            {
                throw new IllegalArgumentException("Width and Height of a Texture must be a power of 2!");
            }

            this.mWidth = pWidth;
            this.mHeight = pHeight;
            this.mTextureOptions = pTextureOptions;
            this.mTextureStateListener = pTextureStateListener;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public int getHardwareTextureID()
        {
            return this.mHardwareTextureID;
        }

        public bool isLoadedToHardware()
        {
            return this.mLoadedToHardware;
        }

        public bool isUpdateOnHardwareNeeded()
        {
            return this.mUpdateOnHardwareNeeded;
        }

        void setLoadedToHardware(bool pLoadedToHardware)
        {
            this.mLoadedToHardware = pLoadedToHardware;
        }

        public int Width { get { return getWidth(); } }

        public int getWidth()
        {
            return this.mWidth;
        }

        public int Height { get { return getHeight(); } }

        public int getHeight()
        {
            return this.mHeight;
        }

        public TextureOptions getTextureOptions()
        {
            return this.mTextureOptions;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public TextureSourceWithLocation addTextureSource(ITextureSource pTextureSource, int pTexturePositionX, int pTexturePositionY) /* throws IllegalArgumentException */ {
            this.checkTextureSourcePosition(pTextureSource, pTexturePositionX, pTexturePositionY);

            TextureSourceWithLocation textureSourceWithLocation = new TextureSourceWithLocation(pTextureSource, pTexturePositionX, pTexturePositionY);
            this.mTextureSources.Add(textureSourceWithLocation);
            this.mUpdateOnHardwareNeeded = true;
            return textureSourceWithLocation;
        }

        private void checkTextureSourcePosition(ITextureSource pTextureSource, int pTexturePositionX, int pTexturePositionY) /* throws IllegalArgumentException */ {
            if (pTexturePositionX < 0)
            {
                throw new IllegalArgumentException("Illegal negative pTexturePositionX supplied: '" + pTexturePositionX + "'");
            }
            else if (pTexturePositionY < 0)
            {
                throw new IllegalArgumentException("Illegal negative pTexturePositionY supplied: '" + pTexturePositionY + "'");
            }
            else if (pTexturePositionX + pTextureSource.getWidth() > this.mWidth || pTexturePositionY + pTextureSource.getHeight() > this.mHeight)
            {
                throw new IllegalArgumentException("Supplied TextureSource must not exceed bounds of Texture.");
            }
        }

        public void removeTextureSource(ITextureSource pTextureSource, int pTexturePositionX, int pTexturePositionY)
        {
            //final ArrayList<TextureSourceWithLocation> textureSources = this.mTextureSources;
            List<TextureSourceWithLocation> textureSources = this.mTextureSources;
            for (int i = textureSources.Count - 1; i >= 0; i--)
            {
                TextureSourceWithLocation textureSourceWithLocation = textureSources[i];
                if (textureSourceWithLocation.mTextureSource == pTextureSource && textureSourceWithLocation.mTexturePositionX == pTexturePositionX && textureSourceWithLocation.mTexturePositionY == pTexturePositionY)
                {
                    textureSources.RemoveAt(i);
                    this.mUpdateOnHardwareNeeded = true;
                    return;
                }
            }
        }

        public void clearTextureSources()
        {
            this.mTextureSources.Clear();
            this.mUpdateOnHardwareNeeded = true;
        }

        public void loadToHardware(GL10 pGL)
        {
            GLHelper.enableTextures(pGL);

            this.mHardwareTextureID = Texture.generateHardwareTextureID(pGL);

            this.allocateAndBindTextureOnHardware(pGL);

            this.applyTextureOptions(pGL);

            this.writeTextureToHardware(pGL);

            this.mUpdateOnHardwareNeeded = false;
            this.mLoadedToHardware = true;

            if (this.mTextureStateListener != null)
            {
                this.mTextureStateListener.onLoadedToHardware(this);
            }
        }

        public void unloadFromHardware(GL10 pGL)
        {
            GLHelper.enableTextures(pGL);

            this.deleteTextureOnHardware(pGL);

            this.mHardwareTextureID = -1;

            this.mLoadedToHardware = false;

            if (this.mTextureStateListener != null)
            {
                this.mTextureStateListener.onUnloadedFromHardware(this);
            }
        }

        private void writeTextureToHardware(GL10 pGL)
        {
            bool preMultipyAlpha = this.mTextureOptions.mPreMultipyAlpha;

            //final ArrayList<TextureSourceWithLocation> textureSources = this.mTextureSources;
            List<TextureSourceWithLocation> textureSources = this.mTextureSources;
            int textureSourceCount = textureSources.Count;

            for (int j = 0; j < textureSourceCount; j++)
            {
                TextureSourceWithLocation textureSourceWithLocation = textureSources[j];
                if (textureSourceWithLocation != null)
                {
                    Bitmap bmp = textureSourceWithLocation.onLoadBitmap();
                    try
                    {
                        if (bmp == null)
                        {
                            throw new IllegalArgumentException("TextureSource: " + textureSourceWithLocation.ToString() + " returned a null Bitmap.");
                        }
                        if (preMultipyAlpha)
                        {
                            GLUtils.TexSubImage2D(GL10Consts.GlTexture2d, 0, textureSourceWithLocation.getTexturePositionX(), textureSourceWithLocation.getTexturePositionY(), bmp, GL10Consts.GlRgba, GL10Consts.GlUnsignedByte);
                        }
                        else
                        {
                            GLHelper.glTexSubImage2D(pGL, GL10Consts.GlTexture2d, 0, textureSourceWithLocation.getTexturePositionX(), textureSourceWithLocation.getTexturePositionY(), bmp, GL10Consts.GlRgba, GL10Consts.GlUnsignedByte);
                        }

                        bmp.Recycle();
                    }
                    catch (IllegalArgumentException iae)
                    {
                        // TODO Load some static checkerboard or so to visualize that loading the texture has failed.
                        Debug.e("Error loading: " + textureSourceWithLocation.ToString(), iae);
                        if (this.mTextureStateListener != null)
                        {
                            this.mTextureStateListener.onTextureSourceLoadExeption(this, textureSourceWithLocation.mTextureSource, iae);
                        }
                        else
                        {
                            throw iae;
                        }
                    }
                }
            }
        }

        private void applyTextureOptions(GL10 pGL)
        {
            TextureOptions textureOptions = this.mTextureOptions;
            pGL.GlTexParameterf(GL10Consts.GlTexture2d, GL10Consts.GlTextureMinFilter, textureOptions.mMinFilter);
            pGL.GlTexParameterf(GL10Consts.GlTexture2d, GL10Consts.GlTextureMagFilter, textureOptions.mMagFilter);
            pGL.GlTexParameterf(GL10Consts.GlTexture2d, GL10Consts.GlTextureWrapS, textureOptions.mWrapS);
            pGL.GlTexParameterf(GL10Consts.GlTexture2d, GL10Consts.GlTextureWrapT, textureOptions.mWrapT);
            pGL.GlTexEnvf(GL10Consts.GlTextureEnv, GL10Consts.GlTextureEnvMode, textureOptions.mTextureEnvironment);
        }

        private void allocateAndBindTextureOnHardware(GL10 pGL)
        {
            GLHelper.bindTexture(pGL, this.mHardwareTextureID);

            Texture.sendPlaceholderBitmapToHardware(this.mWidth, this.mHeight);
        }

        private void deleteTextureOnHardware(GL10 pGL)
        {
            GLHelper.deleteTexture(pGL, this.mHardwareTextureID);
        }

        private static int generateHardwareTextureID(GL10 pGL)
        {
            pGL.glGenTextures(1, Texture.HARDWARETEXTUREID_FETCHER, 0);

            return Texture.HARDWARETEXTUREID_FETCHER[0];
        }

        private static void sendPlaceholderBitmapToHardware(int pWidth, int pHeight)
        {
            Bitmap textureBitmap = Bitmap.CreateBitmap(pWidth, pHeight, Bitmap.Config.Argb8888);

            GLUtils.TexImage2D(GL10Consts.GlTexture2d, 0, textureBitmap, 0);

            textureBitmap.Recycle();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        public /* static */ interface ITextureStateListener
        {
            // ===========================================================
            // Final Fields
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            void onLoadedToHardware(Texture pTexture);
            void onTextureSourceLoadExeption(Texture pTexture, ITextureSource pTextureSource, Throwable pThrowable);
            void onUnloadedFromHardware(Texture pTexture);

            // ===========================================================
            // Inner and Anonymous Classes
            // ===========================================================

            /* NB: Moved outside this interface
            public static class TextureStateAdapter : ITextureStateListener {
                public override void onLoadedToHardware(Texture pTexture) { }

                public override void onTextureSourceLoadExeption(Texture pTexture, ITextureSource pTextureSource, Throwable pThrowable) { }

                public override void onUnloadedFromHardware(Texture pTexture) { }
            }

            public static class DebugTextureStateListener : ITextureStateListener {
                public override void onLoadedToHardware(Texture pTexture) {
                    Debug.d("Texture loaded: " + pTexture.ToString());
                }

                public override void onTextureSourceLoadExeption(Texture pTexture, ITextureSource pTextureSource, Throwable pThrowable) {
                    Debug.e("Exception loading TextureSource. Texture: " + pTexture.ToString() + " TextureSource: " + pTextureSource.toString(), pThrowable);
                }

                public override void onUnloadedFromHardware(Texture pTexture) {
                    Debug.d("Texture unloaded: " + pTexture.ToString());
                }
            }
            */
        }

        public /* static */ class TextureStateAdapter : ITextureStateListener
        {
            public override void onLoadedToHardware(Texture pTexture) { }

            public override void onTextureSourceLoadExeption(Texture pTexture, ITextureSource pTextureSource, Throwable pThrowable) { }

            public override void onUnloadedFromHardware(Texture pTexture) { }
        }

        public /* static */ class DebugTextureStateListener : ITextureStateListener
        {
            public override void onLoadedToHardware(Texture pTexture)
            {
                Debug.d("Texture loaded: " + pTexture.ToString());
            }

            public override void onTextureSourceLoadExeption(Texture pTexture, ITextureSource pTextureSource, Throwable pThrowable)
            {
                Debug.e("Exception loading TextureSource. Texture: " + pTexture.ToString() + " TextureSource: " + pTextureSource.toString(), pThrowable);
            }

            public override void onUnloadedFromHardware(Texture pTexture)
            {
                Debug.d("Texture unloaded: " + pTexture.ToString());
            }
        }

        public /* static */ class TextureSourceWithLocation
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            public readonly ITextureSource mTextureSource;
            public /* final */ int mTexturePositionX;
            public /* final */ int mTexturePositionY;

            // ===========================================================
            // Constructors
            // ===========================================================

            public TextureSourceWithLocation(ITextureSource pTextureSource, int pTexturePositionX, int pTexturePositionY)
            {
                this.mTextureSource = pTextureSource;
                this.mTexturePositionX = pTexturePositionX;
                this.mTexturePositionY = pTexturePositionY;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            public int TexturePositionX { get { return getTexturePositionX(); } }
            public int TexturePositionY { get { return getTexturePositionY(); } }

            public int getTexturePositionX()
            {
                return this.mTexturePositionX;
            }

            public int getTexturePositionY()
            {
                return this.mTexturePositionY;
            }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            public int Width { get { return getWidth(); } }
            public int Height { get { return getHeight(); } }

            public int getWidth()
            {
                return this.mTextureSource.getWidth();
            }

            public int getHeight()
            {
                return this.mTextureSource.getHeight();
            }

            public Bitmap onLoadBitmap()
            {
                return this.mTextureSource.onLoadBitmap();
            }

            public override String ToString()
            {
                return this.mTextureSource.ToString();
            }

            public void setTexturePosition(int TexturePositionX, int TexturePositionY)
            {
                this.mTexturePositionX = TexturePositionX;
                this.mTexturePositionY = TexturePositionY;
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