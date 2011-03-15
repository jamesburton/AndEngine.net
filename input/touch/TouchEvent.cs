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
        public static /* final */ readonly MotionEventActions ACTION_DOWN = MotionEventActions.Down;
        //public static /* final */ readonly int ACTION_MOVE = MotionEvent.ACTION_MOVE;
        public static /* final */ readonly MotionEventActions ACTION_MOVE = MotionEventActions.Move;
        //public static /* final */ readonly int ACTION_OUTSIDE = MotionEvent.ACTION_OUTSIDE;
        public static /* final */ readonly MotionEventActions ACTION_OUTSIDE = MotionEventActions.Outside;
        //public static /* final */ readonly int ACTION_UP = MotionEvent.ACTION_UP;
        public static /* final */ readonly MotionEventActions ACTION_UP = MotionEventActions.Up;

        private static /* final */ readonly TouchEventPool TOUCHEVENT_POOL = new TouchEventPool();

        // ===========================================================
        // Fields
        // ===========================================================

        protected int mPointerID;

        protected float mX;
        protected float mY;

        protected /* int */ MotionEventActions mAction;

        protected MotionEvent mMotionEvent;

        // ===========================================================
        // Constructors
        // ===========================================================

        public static TouchEvent Obtain(/* final */ float pX, /* final */ float pY, /* final int */ MotionEventActions pAction, /* final */ int pPointerID, /* final */ MotionEvent pMotionEvent)
        {
            /* final */
            TouchEvent touchEvent = TOUCHEVENT_POOL.ObtainPoolItem();
            touchEvent.Set(pX, pY, pAction, pPointerID, pMotionEvent);
            return touchEvent;
        }

        private void Set(/* final */ float pX, /* final */ float pY, /* final int */ MotionEventActions pAction, /* final */ int pPointerID, /* final */ MotionEvent pMotionEvent)
        {
            this.mX = pX;
            this.mY = pY;
            this.mAction = pAction;
            this.mPointerID = pPointerID;
            this.mMotionEvent = pMotionEvent;
        }

        public void Recycle()
        {
            TOUCHEVENT_POOL.RecyclePoolItem(this);
        }

        public static void recycle(/* final */ TouchEvent pTouchEvent)
        {
            TOUCHEVENT_POOL.RecyclePoolItem(pTouchEvent);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float GetX()
        {
            return this.mX;
        }
        public float X { get { return GetX(); } }

        public float GetY()
        {
            return this.mY;
        }
        public float Y { get { return GetY(); } }

        public void Set(/* final */ float pX, /* final */ float pY)
        {
            this.mX = pX;
            this.mY = pY;
        }

        public void Offset(/* final */ float pDeltaX, /* final */ float pDeltaY)
        {
            this.mX += pDeltaX;
            this.mY += pDeltaY;
        }

        public int GetPointerID()
        {
            return this.mPointerID;
        }
        public int PointerID { get { return GetPointerID(); } }

        public /* int */ MotionEventActions GetAction()
        {
            return this.mAction;
        }
        public MotionEventActions Action { get { return GetAction(); } }

        /**
         * Provides the raw {@link MotionEvent} that originally caused this {@link TouchEvent}.
         * The coordinates of this {@link MotionEvent} are in surface-coordinates!
         * @return
         */
        public MotionEvent GetMotionEvent()
        {
            return this.mMotionEvent;
        }
        public MotionEvent MotionEvent { get { return GetMotionEvent(); } }

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

            protected override TouchEvent OnAllocatePoolItem()
            {
                return new TouchEvent();
            }
        }
    }
}