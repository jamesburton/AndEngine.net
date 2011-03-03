namespace andengine.engine.handler
{

    //import java.util.ArrayList;
    using System.Collections.Generic;

    /**
     * @author Nicolas Gramlich
     * @since 09:45:22 - 31.03.2010
     */
    //public class UpdateHandlerList extends ArrayList<IUpdateHandler> : IUpdateHandler {
    public class UpdateHandlerList : List<IUpdateHandler>, IUpdateHandler
    {
        // ===========================================================
        // Constants
        // ===========================================================

        //private static /* final */ readonly long serialVersionUID = -8842562717687229277L;
        private static /* final */ readonly long serialVersionUID = -8442662717627239297L;

        // ===========================================================
        // Fields
        // ===========================================================

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
            /* final */
            int handlerCount = this.Count;
            for (int i = handlerCount - 1; i >= 0; i--)
            {
                this[i].OnUpdate(pSecondsElapsed);
            }
        }

        public /* override */ void Reset()
        {
            /* final */
            int handlerCount = this.Count;
            for (int i = handlerCount - 1; i >= 0; i--)
            {
                this[i].Reset();
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}