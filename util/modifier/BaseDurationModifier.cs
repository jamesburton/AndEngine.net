namespace andengine.util.modifier
{


    /**
     * @author Nicolas Gramlich
     * @since 10:48:13 - 03.09.2010
     * @param <T>
     */
    public abstract class BaseDurationModifier<T> : BaseModifier<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private float mTotalSecondsElapsed;
        protected /* final */ readonly float mDuration;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseDurationModifier() :this(-1, null)
        {
        }

        public BaseDurationModifier(float pDuration)
            : this(pDuration, null)
        {
        }

        public BaseDurationModifier(float pDuration, IModifierListener<T> pModifierListener)
            : base(pModifierListener)
        {
            this.mDuration = pDuration;
        }

        protected BaseDurationModifier(BaseDurationModifier<T> pBaseModifier)
            : this(pBaseModifier.mDuration, pBaseModifier.mModifierListener)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        protected float getTotalSecondsElapsed()
        {
            return this.mTotalSecondsElapsed;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public float getDuration()
        {
            return this.mDuration;
        }

        protected abstract void onManagedUpdate(float pSecondsElapsed, T pItem);

        protected abstract void onManagedInitialize(T pItem);

        public override sealed void onUpdate(float pSecondsElapsed, T pItem)
        {
            if (!this.mFinished)
            {
                if (this.mTotalSecondsElapsed == 0)
                {
                    this.onManagedInitialize(pItem);
                }

                float secondsToElapse;
                if (this.mTotalSecondsElapsed + pSecondsElapsed < this.mDuration)
                {
                    secondsToElapse = pSecondsElapsed;
                }
                else
                {
                    secondsToElapse = this.mDuration - this.mTotalSecondsElapsed;
                }

                this.mTotalSecondsElapsed += secondsToElapse;
                this.onManagedUpdate(secondsToElapse, pItem);

                if (this.mDuration != -1 && this.mTotalSecondsElapsed >= this.mDuration)
                {
                    this.mTotalSecondsElapsed = this.mDuration;
                    this.mFinished = true;
                    if (this.mModifierListener != null)
                    {
                        this.mModifierListener.onModifierFinished(this, pItem);
                    }
                }
            }
        }

        public void reset()
        {
            this.mFinished = false;
            this.mTotalSecondsElapsed = 0;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}