using System.Runtime.CompilerServices;
using Java.Lang;
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

        protected /* override */ new T OnHandleAllocatePoolItem()
        {
            /* final */
            T poolItem = /*super*/base.onHandleAllocatePoolItem();
            poolItem.mParent = this;
            return poolItem;
        }

        protected /* override */ new void OnHandleObtainItem(/* final */ T pPoolItem)
        {
            pPoolItem.mRecycled = false;
            pPoolItem.onObtain();
        }

        protected /* override */ new void OnHandleRecycleItem(/* final */ T pPoolItem)
        {
            pPoolItem.onRecycle();
            pPoolItem.mRecycled = true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* override */ new /*synchronized*/ void RecyclePoolItem(/* final */ T pPoolItem)
        {
            if (pPoolItem.mParent == null)
            {
                throw new IllegalArgumentException("PoolItem not assigned to a pool!");
            }
            else if (!pPoolItem.IsFromPool(this))
            {
                throw new IllegalArgumentException("PoolItem from another pool!");
            }
            else if (pPoolItem.IsRecycled())
            {
                throw new IllegalArgumentException("PoolItem already recycled!");
            }

            //super.recyclePoolItem(pPoolItem);
            base.RecyclePoolItem(pPoolItem);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* synchronized */ bool OwnsPoolItem(/* final */ T pPoolItem)
        {
            return pPoolItem.mParent == this;
        }

        // TODO: Check if anything should be added in place of this:- @SuppressWarnings("unchecked")
        /*protected*/public void Recycle(/* final */ PoolItem pPoolItem)
        {
            this.recyclePoolItem((T)pPoolItem);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}