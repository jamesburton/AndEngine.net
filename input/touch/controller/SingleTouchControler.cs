namespace andengine.input.touch.controller
{

    using MotionEvent = Android.Views.MotionEvent;

    /**
     * @author Nicolas Gramlich
     * @since 20:23:33 - 13.07.2010
     */
    public class SingleTouchControler : BaseTouchController
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

        public SingleTouchControler()
        {

        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override bool onHandleMotionEvent(MotionEvent pMotionEvent)
        {
            //return this.fireTouchEvent(pMotionEvent.getX(), pMotionEvent.getY(), pMotionEvent.getAction(), 0, pMotionEvent);
            return this.fireTouchEvent(pMotionEvent.GetX(), pMotionEvent.GetY(), pMotionEvent.Action, 0, pMotionEvent);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}