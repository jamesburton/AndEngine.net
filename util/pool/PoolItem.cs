using Java.Lang;
namespace andengine.util.pool
{

    /**
     * @author Valentin Milea
     * @author Nicolas Gramlich
     * 
     * @since 23:02:47 - 21.08.2010
     */
    public abstract class PoolItem
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //Pool<? extends PoolItem> mParent;
        public Pool<PoolItem> mParent;
        public bool mRecycled = true;

        // ===========================================================
        // Constructors
        // ===========================================================

        //public Pool<? extends PoolItem> getParent() {
        public Pool<PoolItem> GetParent()
        {
            return this.mParent;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool IsRecycled()
        {
            return this.mRecycled;
        }

        //public bool isFromPool(final Pool<? extends PoolItem> pPool) {
        public bool IsFromPool(/* final */ Pool<PoolItem> pPool)
        {
            return pPool == this.mParent;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public void OnRecycle()
        {

        }

        public void OnObtain()
        {

        }

        public void Recycle()
        {
            if (this.mParent == null)
            {
                throw new IllegalStateException("Item already recycled!");
            }

            this.mParent.Recycle(this);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}