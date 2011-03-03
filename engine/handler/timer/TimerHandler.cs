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
            Init(pTimerSeconds, false);
            this.mTimerCallback = pTimerCallback;
        }

        public TimerHandler(float pTimerSeconds, bool pAutoReset, ITimerCallback pTimerCallback)
        {
            Init(pTimerSeconds, pAutoReset/*, pTimerCallback*/);
        }

        protected void Init(float pTimerSeconds, bool pAutoReset/*, ITimerCallback pTimerCallback*/)
        {
            this.mTimerSeconds = pTimerSeconds;
            this.mAutoReset = pAutoReset;
            //this.mTimerCallback = pTimerCallback;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool AutoReset { get { return IsAutoReset(); } set { SetAutoReset(value); } }

        public bool IsAutoReset()
        {
            return this.mAutoReset;
        }

        public void SetAutoReset(bool pAutoReset)
        {
            this.mAutoReset = pAutoReset;
        }

        public float TimeSeconds { get { return GetTimerSeconds(); } set { SetTimerSeconds(value); } }

        /**
         * You probably want to use this in conjunction with {@link TimerHandler#reset()}.
         * @param pTimerSeconds
         */
        public void SetTimerSeconds(float pTimerSeconds)
        {
            this.mTimerSeconds = pTimerSeconds;
        }

        public float GetTimerSeconds()
        {
            return this.mTimerSeconds;
        }

        public float TimerSecondsElapsed { get { return GetTimerSecondsElapsed(); } }

        public float GetTimerSecondsElapsed()
        {
            return this.mTimerSecondsElapsed;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public virtual void OnUpdate(float pSecondsElapsed)
        {
            if (this.mAutoReset)
            {
                this.mTimerSecondsElapsed += pSecondsElapsed;
                while (this.mTimerSecondsElapsed >= this.mTimerSeconds)
                {
                    this.mTimerSecondsElapsed -= this.mTimerSeconds;
                    this.mTimerCallback.OnTimePassed(this);
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
                        this.mTimerCallback.OnTimePassed(this);
                    }
                }
            }
        }

        public /* override */ virtual void Reset()
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