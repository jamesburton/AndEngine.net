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
        public IPool mParent;
        public bool mRecycled = true;

        // ===========================================================
        // Constructors
        // ===========================================================

        //public Pool<? extends PoolItem> getParent() {
        public IPool GetParent()
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
        public bool IsFromPool(/* final */ IPool pPool)
        {
            return pPool == this.mParent;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public virtual void OnRecycle()
        {

        }

        public virtual void OnObtain()
        {

        }

        public virtual void Recycle()
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