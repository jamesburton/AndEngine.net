using andengine.engine.handler.runnable;
using System.Threading;
namespace andengine.util.pool
{

    /**
     * @author Valentin Milea
     * @author Nicolas Gramlich
     * 
     * @since 23:03:58 - 21.08.2010
     * @param <T>
     */
    //public abstract class RunnablePoolUpdateHandler<T extends RunnablePoolItem> extends PoolUpdateHandler<T> {
    public abstract class RunnablePoolUpdateHandler<T> : PoolUpdateHandler<T> where T : RunnablePoolItem
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

        public RunnablePoolUpdateHandler()
        {

        }

        /*
        public RunnablePoolUpdateHandler(final int pInitialPoolSize) {
            super(pInitialPoolSize);
        }
        */
        public RunnablePoolUpdateHandler(int pInitialPoolSize) : base(pInitialPoolSize) { }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override abstract T OnAllocatePoolItem();

        protected override void OnHandlePoolItem(T pRunnablePoolItem)
        {
            // TODO: Check if the ThreadStart bit is needed
            //((Runnable)pRunnablePoolItem).run();
            new Thread(new ThreadStart(((Runnable)pRunnablePoolItem).Run)).Start();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}