namespace andengine.util.pool
{

    //import java.util.Stack;
    using System.Collections;
    using System.Collections.Generic;

    using andengine.util/*.Debug*/;
    using System.Runtime.CompilerServices;
    using Java.Lang;

    /**
     * @author Valentin Milea
     * @author Nicolas Gramlich
     * 
     * @since 22:19:55 - 31.08.2010
     * @param <T>
     */
    public abstract class GenericPool<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ readonly Stack<T> mAvailableItems = new Stack<T>();
        private int mUnrecycledCount;
        private /* final */ readonly int mGrowth;

        // ===========================================================
        // Constructors
        // ===========================================================

        public GenericPool():this(0)
        {
        }

        public GenericPool(/* final */ int pInitialSize):this(pInitialSize, 1)
        {
        }

        public GenericPool(/* final */ int pInitialSize, /* final */ int pGrowth)
        {
            if (pGrowth < 0)
            {
                throw new IllegalArgumentException("pGrowth must be at least 0!");
            }

            this.mGrowth = pGrowth;

            if (pInitialSize > 0)
            {
                this.BatchAllocatePoolItems(pInitialSize);
            }
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* synchronized */ int GetUnrecycledCount()
        {
            return this.mUnrecycledCount;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract T OnAllocatePoolItem();

        // ===========================================================
        // Methods
        // ===========================================================

        protected void OnHandleRecycleItem(/* final */ T pItem)
        {

        }

        protected T OnHandleAllocatePoolItem()
        {
            return this.OnAllocatePoolItem();
        }

        protected void OnHandleObtainItem(/* final */ T pItem)
        {

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* synchronized */ void BatchAllocatePoolItems(/* final */ int pCount)
        {
            /* final */
            Stack<T> availableItems = this.mAvailableItems;
            for (int i = pCount - 1; i >= 0; i--)
            {
                availableItems.Push(onHandleAllocatePoolItem());
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* synchronized */ T ObtainPoolItem()
        {
            /* final */
            T item;

            if (this.mAvailableItems.Count > 0)
            {
                item = this.mAvailableItems.Pop();
            }
            else
            {
                if (this.mGrowth == 1)
                {
                    item = OnHandleAllocatePoolItem();
                }
                else
                {
                    this.BatchAllocatePoolItems(this.mGrowth);
                    item = this.mAvailableItems.Pop();
                }
                //Debug.i(this.getClass().getName() + "<" + item.getClass().getSimpleName() + "> was exhausted, with " + this.mUnrecycledCount + " item not yet recycled. Allocated " + this.mGrowth + " more.");
                Debug.i(this.GetType().FullName + "<" + item.GetType().Name + "> was exhausted, with " + this.mUnrecycledCount.ToString() + " item(s) not yet recycled. Allocated " + this.mGrowth.ToString() + " more.");
            }
            this.OnHandleObtainItem(item);

            this.mUnrecycledCount++;
            return item;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* synchronized */ void RecyclePoolItem(/* final */ T pItem)
        {
            if (pItem == null)
            {
                throw new IllegalArgumentException("Cannot recycle null item!");
            }

            this.OnHandleRecycleItem(pItem);

            this.mAvailableItems.Push(pItem);

            this.mUnrecycledCount--;

            if (this.mUnrecycledCount < 0)
            {
                Debug.e(new Java.Lang.String("More items recycled than obtained!"));
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}