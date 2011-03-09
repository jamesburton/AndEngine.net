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
            protected override TouchEventRunnablePoolItem OnAllocatePoolItem()
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

        public /* override */ virtual void SetTouchEventCallback(ITouchEventCallback pTouchEventCallback)
        {
            this.mTouchEventCallback = pTouchEventCallback;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ virtual void Reset()
        {
            if (this.mRunOnUpdateThread)
            {
                this.mTouchEventRunnablePoolUpdateHandler.Reset();
            }
        }

        public /* override */ virtual void OnUpdate(float pSecondsElapsed)
        {
            if (this.mRunOnUpdateThread)
            {
                this.mTouchEventRunnablePoolUpdateHandler.OnUpdate(pSecondsElapsed);
            }
        }

        protected bool FireTouchEvent(float pX, float pY, /* int pAction */ Android.Views.MotionEventActions pAction, int pPointerID, MotionEvent pMotionEvent)
        {
            bool handled;

            if (this.mRunOnUpdateThread)
            {
                TouchEvent touchEvent = TouchEvent.Obtain(pX, pY, pAction, pPointerID, MotionEvent.Obtain(pMotionEvent));

                TouchEventRunnablePoolItem touchEventRunnablePoolItem = this.mTouchEventRunnablePoolUpdateHandler.ObtainPoolItem();
                touchEventRunnablePoolItem.Set(touchEvent);
                this.mTouchEventRunnablePoolUpdateHandler.PostPoolItem(touchEventRunnablePoolItem);

                handled = true;
            }
            else
            {
                TouchEvent touchEvent = TouchEvent.Obtain(pX, pY, pAction, pPointerID, pMotionEvent);
                handled = this.mTouchEventCallback.OnTouchEvent(touchEvent);
                touchEvent.recycle();
            }

            return handled;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void ApplyTouchOptions(TouchOptions pTouchOptions)
        {
            this.mRunOnUpdateThread = pTouchOptions.IsRunOnUpdateThread();
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

            public void Set(TouchEvent pTouchEvent)
            {
                this.mTouchEvent = pTouchEvent;
            }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            public void Run() {
			   // BaseTouchController.this.mTouchEventCallback.onTouchEvent(this.mTouchEvent);
                BaseTouchController.Instance.mTouchEventCallback.OnTouchEvent(this.mTouchEvent);
		    }

            protected override void OnRecycle()
            {
                base.OnRecycle();
                TouchEvent touchEvent = this.mTouchEvent;
                touchEvent.GetMotionEvent().Recycle();
                touchEvent.Recycle();
            }
        }
    }
}