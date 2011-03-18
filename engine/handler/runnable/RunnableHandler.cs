using Java.Lang;

namespace andengine.engine.handler.runnable
{
    //import java.util.ArrayList;

    using System.Collections;
    using System.Collections.Generic;
    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// This is to substitute for the Java Runnable interface.
    /// We are using ThreadStart instead
    /// See: http://stackoverflow.com/questions/1923512/threading-does-c-have-an-equivalent-of-the-java-runnable-interface
    /// </summary>
    public interface Runnable
    {
        void Run();
    }

    /**
     * @author Nicolas Gramlich
     * @since 10:24:39 - 18.06.2010
     */
    public class RunnableHandler : IUpdateHandler
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //private final ArrayList<Runnable> mRunnables = new ArrayList<Runnable>();
        private readonly List<IRunnable> mRunnables = new List<IRunnable>();
        private static readonly object _methodLock = new object();

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ void OnUpdate(/* final */ float pSecondsElapsed)
        {
            lock (_methodLock)
            {
                //final ArrayList<Runnable> runnables = this.mRunnables;
                List<IRunnable> runnables = this.mRunnables;
                /* final */
                int runnableCount = runnables.Count;
                for (int i = runnableCount - 1; i >= 0; i--)
                {
                    //runnables[i].run();
                    Thread thread = new Thread(new ThreadStart(runnables[i].Run));
                }
                runnables.Clear();
            }
        }

        public /* override */ void Reset()
        {
            lock (_methodLock)
            {
                this.mRunnables.Clear();
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void PostRunnable(/* final */ IRunnable pRunnable)
        {
            lock (_methodLock)
            {
                this.mRunnables.Add(pRunnable);
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}