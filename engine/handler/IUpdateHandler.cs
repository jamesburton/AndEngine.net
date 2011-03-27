using System.Collections.Generic;
using andengine.util;

namespace andengine.engine.handler
{

    /**
     * @author Nicolas Gramlich
     * @since 12:24:09 - 11.03.2010
     */
    public interface IUpdateHandler
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */
        void OnUpdate(/* final */ float pSecondsElapsed);
        /* public */
        void Reset();
    }

    public interface IUpdateHandlerMatcher : IMatcher<IUpdateHandler> {
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================
	}
}