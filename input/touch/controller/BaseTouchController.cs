namespace andengine.input.touch.controller
{

    using TouchOptions = andengine.engine.options.TouchOptions;
    using TouchEvent = andengine.input.touch.TouchEvent;
    using RunnablePoolItem = andengine.util.pool.RunnablePoolItem;
    //using RunnablePoolUpdateHandler = andengine.util.pool.RunnablePoolUpdateHandler;

    using MotionEvent = Android.Views.MotionEvent;

    /**
     * @author Nicolas Gramlich
     * @since 21:06:40 - 13.07.2010
     */
    public abstract class BaseTouchController : ITouchController
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private ITouchEventCallback mTouchEventCallback;

        private bool mRunOnUpdateThread;

        /*
        private final RunnablePoolUpdateHandler<TouchEventRunnablePoolItem> mTouchEventRunnablePoolUpdateHandler = new RunnablePoolUpdateHandler<TouchEventRunnablePoolItem>() {
            @Override
            protected TouchEventRunnablePoolItem onAllocatePoolItem() {
                return new TouchEventRunnablePoolItem();
            }
        };
        */
        public sealed class TouchEventRunnablePoolUpdateHandler : andengine.util.pool.RunnablePoolUpdateHandler<TouchEventRunnablePoolItem>
        {
            protected override TouchEventRunnablePoolItem onAllocatePoolItem()
            {
                return new TouchEventRunnablePoolItem();
            }
        }

        // ===========================================================
        // Constructors
        // ===========================================================

        private static BaseTouchController _Instance;
        public static BaseTouchController Instance { get { return _Instance; } }

        public BaseTouchController()
        {
            _Instance = this;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public override void setTouchEventCallback(ITouchEventCallback pTouchEventCallback)
        {
            this.mTouchEventCallback = pTouchEventCallback;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void reset()
        {
            if (this.mRunOnUpdateThread)
            {
                this.mTouchEventRunnablePoolUpdateHandler.reset();
            }
        }

        public override void onUpdate(float pSecondsElapsed)
        {
            if (this.mRunOnUpdateThread)
            {
                this.mTouchEventRunnablePoolUpdateHandler.onUpdate(pSecondsElapsed);
            }
        }

        protected bool fireTouchEvent(float pX, float pY, /* int pAction */ Android.Views.MotionEventActions pAction, int pPointerID, MotionEvent pMotionEvent)
        {
            bool handled;

            if (this.mRunOnUpdateThread)
            {
                TouchEvent touchEvent = TouchEvent.obtain(pX, pY, pAction, pPointerID, MotionEvent.Obtain(pMotionEvent));

                TouchEventRunnablePoolItem touchEventRunnablePoolItem = this.mTouchEventRunnablePoolUpdateHandler.obtainPoolItem();
                touchEventRunnablePoolItem.set(touchEvent);
                this.mTouchEventRunnablePoolUpdateHandler.postPoolItem(touchEventRunnablePoolItem);

                handled = true;
            }
            else
            {
                TouchEvent touchEvent = TouchEvent.obtain(pX, pY, pAction, pPointerID, pMotionEvent);
                handled = this.mTouchEventCallback.onTouchEvent(touchEvent);
                touchEvent.recycle();
            }

            return handled;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void applyTouchOptions(TouchOptions pTouchOptions)
        {
            this.mRunOnUpdateThread = pTouchOptions.isRunOnUpdateThread();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        public class TouchEventRunnablePoolItem : RunnablePoolItem
        {
            // ===========================================================
            // Fields
            // ===========================================================

            private TouchEvent mTouchEvent;

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            public void set(TouchEvent pTouchEvent)
            {
                this.mTouchEvent = pTouchEvent;
            }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            public void run() {
			   // BaseTouchController.this.mTouchEventCallback.onTouchEvent(this.mTouchEvent);
                BaseTouchController.Instance.mTouchEventCallback.onTouchEvent(this.mTouchEvent);
		    }

            protected override void onRecycle()
            {
                base.onRecycle();
                TouchEvent touchEvent = this.mTouchEvent;
                touchEvent.getMotionEvent().Recycle();
                touchEvent.recycle();
            }
        }
    }
}