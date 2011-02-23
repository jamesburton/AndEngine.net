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
        public Pool<PoolItem> getParent()
        {
            return this.mParent;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool isRecycled()
        {
            return this.mRecycled;
        }

        //public bool isFromPool(final Pool<? extends PoolItem> pPool) {
        public bool isFromPool(/* final */ Pool<PoolItem> pPool)
        {
            return pPool == this.mParent;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public void onRecycle()
        {

        }

        public void onObtain()
        {

        }

        public void recycle()
        {
            if (this.mParent == null)
            {
                throw new IllegalStateException("Item already recycled!");
            }

            this.mParent.recycle(this);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}