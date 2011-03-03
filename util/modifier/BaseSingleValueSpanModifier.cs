namespace andengine.util.modifier
{

    using IEaseFunction = andengine.util.modifier.ease.IEaseFunction;

    /**
     * @author Nicolas Gramlich
     * @since 23:29:22 - 19.03.2010
     */
    public abstract class BaseSingleValueSpanModifier<T> : BaseDurationModifier<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly float mFromValue;
        private readonly float mValueSpan;

        protected readonly IEaseFunction mEaseFunction;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseSingleValueSpanModifier(float pDuration, float pFromValue, float pToValue)
            : this(pDuration, pFromValue, pToValue, null, IEaseFunction.DEFAULT)
        {
        }

        public BaseSingleValueSpanModifier(float pDuration, float pFromValue, float pToValue, IEaseFunction pEaseFunction)
            : this(pDuration, pFromValue, pToValue, null, pEaseFunction)
        {
        }

        public BaseSingleValueSpanModifier(float pDuration, float pFromValue, float pToValue, IModifierListener<T> pModifierListener)
            : this(pDuration, pFromValue, pToValue, IEaseFunction.DEFAULT)
        {
        }

        public BaseSingleValueSpanModifier(float pDuration, float pFromValue, float pToValue, IModifierListener<T> pModifierListener, IEaseFunction pEaseFunction)
            : base(pDuration, pModifierListener)
        {
            this.mFromValue = pFromValue;
            this.mValueSpan = pToValue - pFromValue;
            this.mEaseFunction = pEaseFunction;
        }

        protected BaseSingleValueSpanModifier(BaseSingleValueSpanModifier<T> pBaseSingleValueSpanModifier)
            : base(pBaseSingleValueSpanModifier)
        {
            this.mFromValue = pBaseSingleValueSpanModifier.mFromValue;
            this.mValueSpan = pBaseSingleValueSpanModifier.mValueSpan;
            this.mEaseFunction = pBaseSingleValueSpanModifier.mEaseFunction;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract void OnSetInitialValue(T pItem, float pValue);
        protected abstract void OnSetValue(T pItem, float pPercentageDone, float pValue);

        protected override void OnManagedInitialize(T pItem)
        {
            this.onSetInitialValue(pItem, this.mFromValue);
        }

        protected override void OnManagedUpdate(float pSecondsElapsed, T pItem)
        {
            //float percentageDone = this.mEaseFunction.getPercentageDone(this.getTotalSecondsElapsed(), this.mDuration, 0, 1);
            float percentageDone = this.mEaseFunction.GetPercentageDone(this.TotalSecondsElapsed, this.mDuration, 0, 1);

            this.OnSetValue(pItem, percentageDone, this.mFromValue + percentageDone * this.mValueSpan);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}