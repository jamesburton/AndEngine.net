namespace andengine.util.pool
{

    //using ArrayList = Java.Util.ArrayList;

    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;
    using System.Collections.Generic;
    using Java.Lang;

    /**
     * @author Valentin Milea
     * @author Nicolas Gramlich
     * 
     * @since 23:02:58 - 21.08.2010
     * @param <T>
     */
    public abstract class PoolUpdateHandler<T> : IUpdateHandler where T : PoolItem
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly Pool<T> mPool;
        //private final ArrayList<T> mScheduledPoolItems = new ArrayList<T>();
        private readonly List<T> mScheduledPoolItems = new List<T>();

        // ===========================================================
        // Constructors
        // ===========================================================

        /*
        public PoolUpdateHandler() {
            this.mPool = new Pool<T>() {
                @Override
                protected T onAllocatePoolItem() {
                    return PoolUpdateHandler.this.onAllocatePoolItem();
                }
            };
        }
        */
        public static PoolUpdateHandler<T> Instance;
        public class PoolUpdateHandlerPool : Pool<T>
        {
            // TODO: Consider if this class should also be a generic class (using a where T2 : T clause)
            protected override T OnAllocatePoolItem()
            {
                //return PoolUpdateHandler<T>.this.onAllocatePoolItem();
                return PoolUpdateHandler<T>.Instance.onAllocatePoolItem();
            }
            public PoolUpdateHandlerPool() : base() { }
            public PoolUpdateHandlerPool(int pInitialPoolSize) : base(pInitialPoolSize) { }
            public PoolUpdateHandlerPool(int pInitialPoolSize, int pGrowth) : base(pInitialPoolSize, pGrowth) { }
        }
        public PoolUpdateHandler() { Instance = this; this.mPool = new PoolUpdateHandlerPool(); }

        /*
        public PoolUpdateHandler(int pInitialPoolSize) {
            this.mPool = new Pool<T>(pInitialPoolSize) {
                @Override
                protected T onAllocatePoolItem() {
                    return PoolUpdateHandler.this.onAllocatePoolItem();
                }
            };
        }
        */
        public PoolUpdateHandler(int pInitialPoolSize) { this.mPool = new PoolUpdateHandlerPool(pInitialPoolSize); }

        /*
        public PoolUpdateHandler(final int pInitialPoolSize, final int pGrowth) {
            this.mPool = new Pool<T>(pInitialPoolSize, pGrowth) {
                @Override
                protected T onAllocatePoolItem() {
                    return PoolUpdateHandler.this.onAllocatePoolItem();
                }
            };
        }
        */
        public PoolUpdateHandler(int pInitialPoolSize, int pGrowth) { this.mPool = new PoolUpdateHandlerPool(pInitialPoolSize, pGrowth); }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract T onAllocatePoolItem();

        protected abstract void onHandlePoolItem(T pPoolItem);

        public /* override */ void OnUpdate(float pSecondsElapsed)
        {
            //final ArrayList<T> scheduledPoolItems = this.mScheduledPoolItems;
            List<T> scheduledPoolItems = this.mScheduledPoolItems;

            //synchronized (scheduledPoolItems) {
            lock (scheduledPoolItems)
            {
                int count = scheduledPoolItems.Count;

                if (count > 0)
                {
                    Pool<T> pool = this.mPool;
                    T item;

                    for (int i = 0; i < count; i++)
                    {
                        item = scheduledPoolItems[i];
                        this.onHandlePoolItem(item);
                        pool.recyclePoolItem(item);
                    }

                    scheduledPoolItems.Clear();
                }
            }
        }

        public /* override */ void Reset()
        {
            //final ArrayList<T> scheduledPoolItems = this.mScheduledPoolItems;
            List<T> scheduledPoolItems = this.mScheduledPoolItems;
            //synchronized (scheduledPoolItems) {
            lock (scheduledPoolItems)
            {
                int count = scheduledPoolItems.Count;

                Pool<T> pool = this.mPool;
                for (int i = count - 1; i >= 0; i--)
                {
                    pool.recyclePoolItem(scheduledPoolItems[i]);
                }

                scheduledPoolItems.Clear();
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public T ObtainPoolItem()
        {
            return this.mPool.ObtainPoolItem();
        }

        public void PostPoolItem(T pPoolItem)
        {
            //synchronized (this.mScheduledPoolItems) {
            lock (this.mScheduledPoolItems)
            {
                if (pPoolItem == null)
                {
                    throw new IllegalArgumentException("PoolItem already recycled!");
                }
                else if (!this.mPool.OwnsPoolItem(pPoolItem))
                {
                    throw new IllegalArgumentException("PoolItem from another pool!");
                }

                this.mScheduledPoolItems.Add(pPoolItem);
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}