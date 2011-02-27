namespace andengine.engine.handler.timer
{

    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;

    /**
     * @author Nicolas Gramlich
     * @since 16:23:58 - 12.03.2010
     */
    public class TimerHandler : IUpdateHandler
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private float mTimerSeconds;
        private float mTimerSecondsElapsed;
        private bool mCallbackTriggered = false;
        private readonly ITimerCallback mTimerCallback;
        private bool mAutoReset;

        // ===========================================================
        // Constructors
        // ===========================================================

        public TimerHandler(float pTimerSeconds, ITimerCallback pTimerCallback)
        {
            this(pTimerSeconds, false, pTimerCallback);
        }

        public TimerHandler(float pTimerSeconds, boolean pAutoReset, ITimerCallback pTimerCallback)
        {
            this.mTimerSeconds = pTimerSeconds;
            this.mAutoReset = pAutoReset;
            this.mTimerCallback = pTimerCallback;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool AutoReset { get { return isAutoReset(); } set { setAutoReset(value); } }

        public bool isAutoReset()
        {
            return this.mAutoReset;
        }

        public void setAutoReset(bool pAutoReset)
        {
            this.mAutoReset = pAutoReset;
        }

        public float TimeSeconds { get { return getTimerSeconds(); } set { setTimerSeconds(value); } }

        /**
         * You probably want to use this in conjunction with {@link TimerHandler#reset()}.
         * @param pTimerSeconds
         */
        public void setTimerSeconds(float pTimerSeconds)
        {
            this.mTimerSeconds = pTimerSeconds;
        }

        public float getTimerSeconds()
        {
            return this.mTimerSeconds;
        }

        public float TimerSecondsElapsed { get { return getTimerSecondsElapsed(); } }

        public float getTimerSecondsElapsed()
        {
            return this.mTimerSecondsElapsed;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void onUpdate(float pSecondsElapsed)
        {
            if (this.mAutoReset)
            {
                this.mTimerSecondsElapsed += pSecondsElapsed;
                while (this.mTimerSecondsElapsed >= this.mTimerSeconds)
                {
                    this.mTimerSecondsElapsed -= this.mTimerSeconds;
                    this.mTimerCallback.onTimePassed(this);
                }
            }
            else
            {
                if (!this.mCallbackTriggered)
                {
                    this.mTimerSecondsElapsed += pSecondsElapsed;
                    if (this.mTimerSecondsElapsed >= this.mTimerSeconds)
                    {
                        this.mCallbackTriggered = true;
                        this.mTimerCallback.onTimePassed(this);
                    }
                }
            }
        }

        public override void reset()
        {
            this.mCallbackTriggered = false;
            this.mTimerSecondsElapsed = 0;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}