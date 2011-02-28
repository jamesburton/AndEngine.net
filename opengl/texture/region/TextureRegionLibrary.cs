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

        public override TextureRegion get(int pID)
        {
            return (TextureRegion)base.get(pID);
        }

        public TiledTextureRegion getTiled(int pID)
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