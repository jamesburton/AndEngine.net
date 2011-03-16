namespace andengine.opengl.texture
{

    //import java.util.ArrayList;
    //import java.util.HashSet;
    using System.Collections.Generic;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    /**
     * @author Nicolas Gramlich
     * @since 17:48:46 - 08.03.2010
     */
    public class TextureManager
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly HashSet<Texture> mTexturesManaged = new HashSet<Texture>();

        //private final ArrayList<Texture> mTexturesLoaded = new ArrayList<Texture>();
        private readonly List<Texture> mTexturesLoaded = new List<Texture>();

        //private final ArrayList<Texture> mTexturesToBeLoaded = new ArrayList<Texture>();
        //private final ArrayList<Texture> mTexturesToBeUnloaded = new ArrayList<Texture>();
        private readonly List<Texture> mTexturesToBeLoaded = new List<Texture>();
        private readonly List<Texture> mTexturesToBeUnloaded = new List<Texture>();

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

        protected void clear()
        {
            this.mTexturesToBeLoaded.Clear();
            this.mTexturesLoaded.Clear();
            this.mTexturesManaged.Clear();
        }

        /**
         * @param pTexture the {@link Texture} to be loaded before the very next frame is drawn (Or prevent it from being unloaded then).
         * @return <code>true</code> when the {@link Texture} was previously not managed by this {@link TextureManager}, <code>false</code> if it was already managed.
         */
        public bool loadTexture(Texture pTexture)
        {
            if (this.mTexturesManaged.Contains(pTexture))
            {
                /* Just make sure it doesn't get deleted. */
                this.mTexturesToBeUnloaded.Remove(pTexture);
                return false;
            }
            else
            {
                this.mTexturesManaged.Add(pTexture);
                this.mTexturesToBeLoaded.Add(pTexture);
                return true;
            }
        }

        /**
         * @param pTexture the {@link Texture} to be unloaded before the very next frame is drawn (Or prevent it from being loaded then).
         * @return <code>true</code> when the {@link Texture} was already managed by this {@link TextureManager}, <code>false</code> if it was not managed.
         */
        public bool unloadTexture(Texture pTexture)
        {
            if (this.mTexturesManaged.Contains(pTexture))
            {
                /* If the Texture is loaded, unload it.
                 * If the Texture is about to be loaded, stop it from being loaded. */
                if (this.mTexturesLoaded.Contains(pTexture))
                {
                    this.mTexturesToBeUnloaded.Add(pTexture);
                }
                else if (this.mTexturesToBeLoaded.Remove(pTexture))
                {
                    this.mTexturesManaged.Remove(pTexture);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void loadTextures(final Texture ... pTextures) {
        public void loadTextures(params Texture[] pTextures)
        {
            for (int i = pTextures.Length - 1; i >= 0; i--)
            {
                this.loadTexture(pTextures[i]);
            }
        }

        //public void unloadTextures(final Texture ... pTextures) {
        public void uploadTextures(params Texture[] pTextures)
        {
            for (int i = pTextures.Length - 1; i >= 0; i--)
            {
                this.unloadTexture(pTextures[i]);
            }
        }

        public void reloadTextures()
        {
            HashSet<Texture> managedTextures = this.mTexturesManaged;
            //for(final Texture texture : managedTextures) { // TODO Can the use of the iterator be avoided somehow?
            foreach (Texture texture in managedTextures)
            {
                texture.SetLoadedToHardware(false);
            }

            //this.mTexturesToBeLoaded.addAll(this.mTexturesLoaded); // TODO Check if addAll uses iterator internally!
            this.mTexturesToBeLoaded.AddRange(this.mTexturesLoaded); // TODO Check if addAll uses iterator internally!
            this.mTexturesLoaded.Clear();

            //this.mTexturesManaged.removeAll(this.mTexturesToBeUnloaded); // TODO Check if removeAll uses iterator internally!
            this.mTexturesManaged.RemoveWhere(x => this.mTexturesToBeUnloaded.Contains(x)); // TODO Check if removeAll uses iterator internally!
            this.mTexturesToBeUnloaded.Clear();
        }

        public void updateTextures(GL10 pGL)
        {
            HashSet<Texture> texturesManaged = this.mTexturesManaged;
            //final ArrayList<Texture> texturesLoaded = this.mTexturesLoaded;
            //final ArrayList<Texture> texturesToBeLoaded = this.mTexturesToBeLoaded;
            //final ArrayList<Texture> texturesToBeUnloaded = this.mTexturesToBeUnloaded;

            List<Texture> texturesLoaded = this.mTexturesLoaded;
            List<Texture> texturesToBeLoaded = this.mTexturesToBeLoaded;
            List<Texture> texturesToBeUnloaded = this.mTexturesToBeUnloaded;

            /* First reload Textures that need to be updated. */
            int texturesLoadedCount = texturesLoaded.Count;

            if (texturesLoadedCount > 0)
            {
                for (int i = texturesLoadedCount - 1; i >= 0; i--)
                {
                    Texture textureToBeUpdated = texturesLoaded[i];
                    if (textureToBeUpdated.IsUpdateOnHardwareNeeded())
                    {
                        textureToBeUpdated.UnloadFromHardware(pGL);
                        textureToBeUpdated.LoadToHardware(pGL);
                    }
                }
            }

            /* Then load pending Textures. */
            int texturesToBeLoadedCount = texturesToBeLoaded.Count;

            if (texturesToBeLoadedCount > 0)
            {
                for (int i = texturesToBeLoadedCount - 1; i >= 0; i--)
                {
                    Texture textureToBeLoaded = texturesToBeLoaded[i];
                    texturesToBeLoaded.RemoveAt(i);
                    if (!textureToBeLoaded.IsLoadedToHardware())
                    {
                        textureToBeLoaded.LoadToHardware(pGL);
                    }
                    texturesLoaded.Add(textureToBeLoaded);
                }
            }

            /* Then unload pending Textures. */
            int texturesToBeUnloadedCount = texturesToBeUnloaded.Count;

            if (texturesToBeUnloadedCount > 0)
            {
                for (int i = texturesToBeUnloadedCount - 1; i >= 0; i--)
                {
                    Texture textureToBeUnloaded = texturesToBeUnloaded[i];
                    texturesToBeUnloaded.RemoveAt(i);
                    if (textureToBeUnloaded.IsLoadedToHardware())
                    {
                        textureToBeUnloaded.UnloadFromHardware(pGL);
                    }
                    texturesLoaded.Remove(textureToBeUnloaded);
                    texturesManaged.Remove(textureToBeUnloaded);
                }
            }

            /* Finally invoke the GC if anything has changed. */
            if (texturesToBeLoadedCount > 0 || texturesToBeUnloadedCount > 0)
            {
                //System.gc();
                // TODO: See if any equivalent to System.gc(); is required
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}