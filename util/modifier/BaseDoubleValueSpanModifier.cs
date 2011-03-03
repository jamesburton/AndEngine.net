namespace andengine.util.modifier
{

    using IEaseFunction = andengine.util.modifier.ease.IEaseFunction;

    /**
     * @author Nicolas Gramlich
     * @since 10:51:46 - 03.09.2010
     * @param <T>
     */
    public abstract class BaseDoubleValueSpanModifier<T> : BaseSingleValueSpanModifier<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ float mFromValueB;
        private /* final */ float mValueSpanB;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseDoubleValueSpanModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB)
            : this(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, null, IEaseFunction.DEFAULT)
        {
        }

        public BaseDoubleValueSpanModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB, IEaseFunction pEaseFunction)
            : this(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, null, pEaseFunction)
        { }

        public BaseDoubleValueSpanModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB, IModifierListener<T> pModifierListener)
            : this(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, pModifierListener, IEaseFunction.DEFAULT)
        { }

        public BaseDoubleValueSpanModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB, IModifierListener<T> pModifierListener, IEaseFunction pEaseFunction)
            : base(pDuration, pFromValueA, pToValueA, pModifierListener, pEaseFunction)
        {
            this.mFromValueB = pFromValueB;
            this.mValueSpanB = pToValueB - pFromValueB;
        }

        protected BaseDoubleValueSpanModifier(BaseDoubleValueSpanModifier<T> pBaseDoubleValueSpanModifier)
            : base(pBaseDoubleValueSpanModifier)
        {
            this.mFromValueB = pBaseDoubleValueSpanModifier.mFromValueB;
            this.mValueSpanB = pBaseDoubleValueSpanModifier.mValueSpanB;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract void OnSetInitialValues(T pItem, float pValueA, float pValueB);
        protected abstract void OnSetValues(T pItem, float pPercentageDone, float pValueA, float pValueB);

        protected override void OnSetInitialValue(T pItem, float pValueA)
        {
            this.OnSetInitialValues(pItem, pValueA, this.mFromValueB);
        }

        protected override void OnSetValue(T pItem, float pPercentageDone, float pValueA)
        {
            this.OnSetValues(pItem, pPercentageDone, pValueA, this.mFromValueB + pPercentageDone * this.mValueSpanB);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}