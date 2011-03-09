namespace andengine.opengl.texture.region
{

    //using Library = andengine.util.Library;

    /**
     * @author Nicolas Gramlich
     * @since 11:52:26 - 20.08.2010
     */
    public class TextureRegionLibrary : andengine.util.Library<BaseTextureRegion>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public TextureRegionLibrary()
            : base()
        {
        }

        public TextureRegionLibrary(int pInitialCapacity)
            : base(pInitialCapacity)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        /*
        public override TextureRegion Get(int pID)
        {
            return (TextureRegion)base.Get(pID);
        }
        */
        public override BaseTextureRegion GetCore(int pID)
        {
            return Get(pID);
        }
        public new TextureRegion Get(int pID)
        {
            return (TextureRegion)base.GetCore(pID);
        }

        public TiledTextureRegion GetTiled(int pID)
        {
            //return (TiledTextureRegion) this.mItems.get(pID);
            return (TiledTextureRegion)this.mItems[pID];
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}