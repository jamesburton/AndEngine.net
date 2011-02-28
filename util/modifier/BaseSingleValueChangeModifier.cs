namespace andengine.util.modifier
{


    /**
     * @author Nicolas Gramlich
     * @since 10:49:51 - 03.09.2010
     * @param <T>
     */
    public abstract class BaseSingleValueChangeModifier<T> : BaseDurationModifier<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ float mValueChangePerSecond;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseSingleValueChangeModifier(float pDuration, float pValueChange)
            : this(pDuration, pValueChange, null)
        {
        }

        public BaseSingleValueChangeModifier(float pDuration, float pValueChange, IModifierListener<T> pModifierListener)
            : base(pDuration, pModifierListener)
        {
            this.mValueChangePerSecond = pValueChange / pDuration;
        }

        protected BaseSingleValueChangeModifier(BaseSingleValueChangeModifier<T> pBaseSingleValueChangeModifier)
            : base(pBaseSingleValueChangeModifier)
        {
            this.mValueChangePerSecond = pBaseSingleValueChangeModifier.mValueChangePerSecond;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract void onChangeValue(T pItem, float pValue);

        protected override void onManagedInitialize(T pItem)
        {

        }

        protected override void onManagedUpdate(float pSecondsElapsed, T pItem)
        {
            this.onChangeValue(pItem, this.mValueChangePerSecond * pSecondsElapsed);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}