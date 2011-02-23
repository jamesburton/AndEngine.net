namespace andengine.input.touch
{

    using andengine.util.pool/*.GenericPool*/;

    using Android.Views;

    /**
     * @author Nicolas Gramlich
     * @since 10:17:42 - 13.07.2010
     */
    public class TouchEvent
    {
        // ===========================================================
        // Constants
        // ===========================================================

        //public static /* final */ readonly int ACTION_CANCEL = MotionEvent.ACTION_CANCEL;
        public static readonly MotionEventActions ACTION_CANCEL = MotionEventActions.Cancel;
        //public static /* final */ readonly int ACTION_DOWN = MotionEvent.ACTION_DOWN;
        public static /* final */ readonly MotionEventActions ACTION_DOWN = MotionEventActions.ACTION_DOWN;
        //public static /* final */ readonly int ACTION_MOVE = MotionEvent.ACTION_MOVE;
        public static /* final */ readonly MotionEventActions ACTION_MOVE = MotionEventActions.ACTION_MOVE;
        //public static /* final */ readonly int ACTION_OUTSIDE = MotionEvent.ACTION_OUTSIDE;
        public static /* final */ readonly MotionEventActions ACTION_OUTSIDE = MotionEventActions.ACTION_OUTSIDE;
        //public static /* final */ readonly int ACTION_UP = MotionEvent.ACTION_UP;
        public static /* final */ readonly MotionEventActions ACTION_UP = MotionEventActions.ACTION_UP;

        private static /* final */ readonly TouchEventPool TOUCHEVENT_POOL = new TouchEventPool();

        // ===========================================================
        // Fields
        // ===========================================================

        protected int mPointerID;

        protected float mX;
        protected float mY;

        protected int mAction;

        protected MotionEvent mMotionEvent;

        // ===========================================================
        // Constructors
        // ===========================================================

        public static TouchEvent obtain(/* final */ float pX, /* final */ float pY, /* final */ int pAction, /* final */ int pPointerID, /* final */ MotionEvent pMotionEvent)
        {
            /* final */
            TouchEvent touchEvent = TOUCHEVENT_POOL.obtainPoolItem();
            touchEvent.set(pX, pY, pAction, pPointerID, pMotionEvent);
            return touchEvent;
        }

        private void set(/* final */ float pX, /* final */ float pY, /* final */ int pAction, /* final */ int pPointerID, /* final */ MotionEvent pMotionEvent)
        {
            this.mX = pX;
            this.mY = pY;
            this.mAction = pAction;
            this.mPointerID = pPointerID;
            this.mMotionEvent = pMotionEvent;
        }

        public void recycle()
        {
            TOUCHEVENT_POOL.recyclePoolItem(this);
        }

        public static void recycle(/* final */ TouchEvent pTouchEvent)
        {
            TOUCHEVENT_POOL.recyclePoolItem(pTouchEvent);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float getX()
        {
            return this.mX;
        }

        public float getY()
        {
            return this.mY;
        }

        public void set(/* final */ float pX, /* final */ float pY)
        {
            this.mX = pX;
            this.mY = pY;
        }

        public void offset(/* final */ float pDeltaX, /* final */ float pDeltaY)
        {
            this.mX += pDeltaX;
            this.mY += pDeltaY;
        }

        public int getPointerID()
        {
            return this.mPointerID;
        }

        public int getAction()
        {
            return this.mAction;
        }

        /**
         * Provides the raw {@link MotionEvent} that originally caused this {@link TouchEvent}.
         * The coordinates of this {@link MotionEvent} are in surface-coordinates!
         * @return
         */
        public MotionEvent getMotionEvent()
        {
            return this.mMotionEvent;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        private /* static */ /* final */ class TouchEventPool : GenericPool<TouchEvent>
        {
            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            protected override TouchEvent onAllocatePoolItem()
            {
                return new TouchEvent();
            }
        }
    }
}