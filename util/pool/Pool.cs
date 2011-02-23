using System.Runtime.CompilerServices;
namespace andengine.util.pool
{


    /**
     * @author Valentin Milea
     * @author Nicolas Gramlich
     * 
     * @since 23:00:21 - 21.08.2010
     * @param <T>
     */
    public abstract class Pool<T> : GenericPool<T> where T : PoolItem
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

        public Pool() : base() {}

        public Pool(/* final */ int pInitialSize) : base(pInitialSize) {}

        public Pool(/* final */ int pInitialSize, /* final */ int pGrowth) : base(pInitialSize, pGrowth) {}

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override T onHandleAllocatePoolItem()
        {
            /* final */
            T poolItem = /*super*/base.onHandleAllocatePoolItem();
            poolItem.mParent = this;
            return poolItem;
        }

        protected override void onHandleObtainItem(/* final */ T pPoolItem)
        {
            pPoolItem.mRecycled = false;
            pPoolItem.onObtain();
        }

        protected override void onHandleRecycleItem(/* final */ T pPoolItem)
        {
            pPoolItem.onRecycle();
            pPoolItem.mRecycled = true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public override /*synchronized*/ void recyclePoolItem(/* final */ T pPoolItem)
        {
            if (pPoolItem.mParent == null)
            {
                throw new IllegalArgumentException("PoolItem not assigned to a pool!");
            }
            else if (!pPoolItem.isFromPool(this))
            {
                throw new IllegalArgumentException("PoolItem from another pool!");
            }
            else if (pPoolItem.isRecycled())
            {
                throw new IllegalArgumentException("PoolItem already recycled!");
            }

            //super.recyclePoolItem(pPoolItem);
            base.recyclePoolItem(pPoolItem);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* synchronized */ bool ownsPoolItem(/* final */ T pPoolItem)
        {
            return pPoolItem.mParent == this;
        }

        // TODO: Check if anything should be added in place of this:- @SuppressWarnings("unchecked")
        /*protected*/public void recycle(/* final */ PoolItem pPoolItem)
        {
            this.recyclePoolItem((T)pPoolItem);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}