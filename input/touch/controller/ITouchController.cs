namespace andengine.input.touch.controller
{

    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;
    using TouchOptions = andengine.engine.options.TouchOptions;
    using TouchEvent = andengine.input.touch.TouchEvent;

    //import android.view.MotionEvent;
    using MotionEvent = Android.Views.MotionEvent;

    /**
     * @author Nicolas Gramlich
     * @since 20:23:45 - 13.07.2010
     */
    public interface ITouchController : IUpdateHandler
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */
        void SetTouchEventCallback(/* final */ ITouchEventCallback pTouchEventCallback);

        /* public */
        void ApplyTouchOptions(/* final */ TouchOptions pTouchOptions);

        /* public */
        bool OnHandleMotionEvent(/* final */ MotionEvent pMotionEvent);

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        /* 
        static interface ITouchEventCallback {
            public bool onTouchEvent(/* final * / TouchEvent pTouchEvent);
        } */
    }

    // Moved from within interface
    public interface ITouchEventCallback
    {
        /* public */ bool OnTouchEvent(/* final */ TouchEvent pTouchEvent);
    }
}
