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
        void run();
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
        private readonly List<Runnable> mRunnables = new List<Runnable>();

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* override */ void OnUpdate(/* final */ float pSecondsElapsed) {
		//final ArrayList<Runnable> runnables = this.mRunnables;
        List<Runnable> runnables = this.mRunnables;
		/* final */ int runnableCount = runnables.Count;
		for(int i = runnableCount - 1; i >= 0; i--) {
			//runnables[i].run();
            Thread thread = new Thread(new ThreadStart(runnables[i].run));
		}
		runnables.Clear();
	}

        public /* override */ void Reset()
        {
            this.mRunnables.Clear();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PostRunnable(/* final */ Runnable pRunnable)
        {
            this.mRunnables.Add(pRunnable);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}